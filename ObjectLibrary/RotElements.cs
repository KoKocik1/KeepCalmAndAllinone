using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GeographicLib;

namespace PartyMaker
{
    public class RotElements
    {
        private double x_baza { get; set; }                  //Współrzędna x w stosunku do bazy w metrach
        private double y_baza { get; set; }                  //Współrzędna y w stosunku do bazy w metrach
        private double z_baza { get; set; }                  //Współrzędna z w stosunku do bazy w metrach
        private String identyfikator { get; set; }           //Identyfikator
        private double azi { get; set; }                     //Kąt jaki tworzy wektor łączący element z bazą względem osi y
        public double lat { get; set; }                     //Szerokość geograficzna LLH
        public double lon { get; set; }                     //Długość geograficzna LLH
        public Vector3 xyz { get; set; }                    //Wektor współrzędnych w stosunku do bazy [m]



        //Konstruktor - należy podać odległości od gps, który jest bazą
        public RotElements(String identyfikator, double x_baza, double y_baza, double z_baza)
        {
            this.identyfikator = identyfikator;
            this.x_baza = x_baza;
            this.y_baza = y_baza;
            this.z_baza = z_baza;
            this.azi = obliczenieAzimuth(x_baza, y_baza);
            this.xyz = new Vector3(Convert.ToSingle(x_baza), Convert.ToSingle(y_baza), Convert.ToSingle(z_baza));
        }

        //Wyznaczenie azymutu dla wektora łączącego baze z wyznaczanym punktem, kiedy kurs barki wynosi 0
        private double obliczenieAzimuth(double x, double y)
        {
            double azim = 0;
            azim = (Math.Atan2(x, y) * (180 / Math.PI));
            return azim;
        }

        //Obliczenie pozycji geograficznej

        public void rotForPosition(double heading, double lat_base, double lon_base)
        {
            double output_lat;
            double output_lon;
            IGeodesicLine obrocone = Geodesic.WGS84.DirectLine(lat_base, lon_base, heading,
                Math.Sqrt(Math.Pow(x_baza, 2) + Math.Pow(y_baza, 2)));
            obrocone.Position(Math.Sqrt(Math.Pow(x_baza, 2) + Math.Pow(y_baza, 2)), out output_lat, out output_lon);
            this.lat = output_lat;
            this.lon = output_lon;

        }

        //Kopiowanie obiektu
        public RotElements Copy()
        {
            return (RotElements)this.MemberwiseClone();
        }


        /*
        public void obrot(double psi, double lat_base, double lon_base)
        {                 //psi w stopniach

            double kat = psi + azi;
            if (kat >= 360)
            {
                kat = kat - 360;
            }
            if (kat < 0)
            {
                kat = kat + 360;
            }

            GeodesicData obrocone = Geodesic.WGS84.Direct(lat_base, lon_base, kat, Math.sqrt(Math.pow(x_baza, 2) + Math.pow(y_baza, 2)));
            lat = obrocone.lat2;      //Przejscie na szerokość geograficzną LLH
            lon = obrocone.lon2;      //Przejscie na długoość geograficzną LLH
                                      // lat_DM = (int)lat + ((lat-(int)lat)*0.6);                 //Zmiana części dziesiętnej na minuty
                                      // lon_DM = (int)lon + ((lon-(int)lon)*0.6);                 //Zmiana części dziesiętnej na minuty
        }*/




    }
}