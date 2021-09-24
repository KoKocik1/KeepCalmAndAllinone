using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PartyMaker;

namespace KeepCalmAndAllinone
{
    class Program
    {
        int licznik = 0;
        //Odczyt danych
        MySqlConnection conn;


        //public DateTime (int year, int month, int day, int hour, int minute, int second, int millisecond);
        DateTime poczatek = new DateTime(2021, 9, 21, 8, 03, 0, 0);
        DateTime koniec = new DateTime(2021, 9, 21, 18, 16, 0, 0);

        double poprawkaSat = 4.44, poprawkaAhrs = 0.86;
        double baseX=0.07, baseY=-0.35, rovX=0.09, rovY=0.67;

        private object Lock = new object();

        //flagi
        //czy korzystac z przedzialu poczatek, koniec - w innym przypadku analiza calej bazy
        bool przedzial = false;


       
        static void Main(string[] args)
        {
            Program program = new Program();
        }
        public Program()
        {
            //polaczenie z baza
            DBConnect connDB = new DBConnect();
            conn = connDB.OpenConnection();

            List<gps> odczytyGPS_B = ReadDB.readGPS(poczatek, koniec, "B",conn);
            List<gps> odczytyGPS_R = ReadDB.readGPS(poczatek, koniec, "R", conn);
            List<dvl_bottom> odczytyDVL_bottom = ReadDB.readDVLbottom(poczatek, koniec, conn);
            List<dvl_water> odczytyDVL_water = ReadDB.readDVLwater(poczatek, koniec, conn);

            List<dvl_position_water> odczytyDVL_positionWaterS = ReadDB.readDVLpositionWaterS(poczatek, koniec, conn);
            List<dvl_position> odczytyDVL_positionS = ReadDB.readDVLpositionS(poczatek, koniec, conn);

            List<dvl_position_water> odczytyDVL_positionWaterA = ReadDB.readDVLpositionWaterA(poczatek, koniec, conn);
            List<dvl_position> odczytyDVL_positionA = ReadDB.readDVLpositionA(poczatek, koniec, conn);
            List<quat_ahrs> odczytyAhrs = ReadDB.readAhrs(poczatek, koniec, conn);
            double satHeading = 0;
            double geometricCorrection = GeoCalc.calcGeometricCorrection(baseX, baseY, rovX, rovY);
            
            
            DBWriter dbWriter = new DBWriter(conn);

            foreach (var point in odczytyDVL_bottom)
            {
                for (int powtorzenie = 0; powtorzenie <= 1; powtorzenie++)
                {
                    dvl_position dvlPosition;
                    dvl_position_water dvl_Position_Water;
                    dvl_water dvl_Water;
                    quat_ahrs ahrs;
                    gps gpsB;
                    gps gpsR;

                    if (powtorzenie == 0)
                    {
                        dvlPosition = FindElementsByTime.szukajPozycjiDvlPosition((DateTime)point.local_time, odczytyDVL_positionA);
                        dvl_Position_Water = FindElementsByTime.szukajPozycjiDvlPositionW((DateTime)point.local_time, odczytyDVL_positionWaterA);
                    }
                    else
                    {
                        dvlPosition = FindElementsByTime.szukajPozycjiDvlPosition((DateTime)point.local_time, odczytyDVL_positionS);
                        dvl_Position_Water = FindElementsByTime.szukajPozycjiDvlPositionW((DateTime)point.local_time, odczytyDVL_positionWaterS);
                    }

                    dvl_Water = FindElementsByTime.szukajPozycjiWater((DateTime)point.local_time, odczytyDVL_water);
                    ahrs = FindElementsByTime.szukajPozycjiAhrs((DateTime)point.local_time, odczytyAhrs);
                    gpsB = FindElementsByTime.szukajPozycjiGPS((DateTime)point.local_time, odczytyGPS_B);
                    gpsR = FindElementsByTime.szukajPozycjiGPS((DateTime)point.local_time, odczytyGPS_R);

                    Console.WriteLine(licznik++);

                    if (gpsB.lat != null || gpsR.lat != null)
                    {
                        //Zabezpiecznie na czas
                        if (Math.Abs(((DateTime)gpsB.local_time).Subtract((DateTime)gpsR.local_time).TotalMilliseconds) < 1000)
                        {
                            satHeading = GeoCalc.calcSatHeading(gpsB, gpsR, geometricCorrection);
                            satHeading -= poprawkaSat;
                        }
                    }
                    else
                        satHeading = 0;

                    lock (Lock)
                    {
                        dbWriter.AddEntry(dvl_Water, dvl_Position_Water, dvlPosition, point, ahrs, gpsB, gpsR, satHeading, poprawkaSat, poprawkaAhrs);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
