using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyMaker
{
    public static class FindElementsByTime
    {
        //Szukanie najbliższej pozycji z gps
        public static gps szukajPozycjiGPS(DateTime czas, List<gps> odczytyGPS)
        {
            double czasDouble = czas.ToOADate();
            gps wynik = new gps();

            if (odczytyGPS.Count != 0)
            wynik = odczytyGPS.ElementAt(0);


            if (odczytyGPS.Count == 0 || Convert.ToDateTime(wynik.local_time).ToOADate() > czasDouble)
            {
                return new gps
                {
                    alt = null,
                    device_time = new DateTime(2000, 1, 1, 12, 0, 0, 11),
                    fix = null,
                    idgps = 1,
                    lat = null,
                    local_time = new DateTime(2000, 1, 1, 12, 0, 0, 11),
                    lon = null
                };
            }


            foreach (gps odczyt in odczytyGPS)
            {
                if (Convert.ToDateTime(odczyt.local_time).ToOADate() > czasDouble)
                    break;

                wynik = odczyt;
            }

            return wynik;
        }
        //szukanie najblizszej pozycji ahrs
         public static quat_ahrs szukajPozycjiAhrs(DateTime czas, List<quat_ahrs> odczytyAhrs)
        {
            double czasDouble = czas.ToOADate();
            quat_ahrs wynik = new quat_ahrs();

            if (odczytyAhrs.Count!=0)
            wynik = odczytyAhrs.ElementAt(0);

            if (odczytyAhrs.Count == 0|| Convert.ToDateTime(wynik.time).ToOADate() > czasDouble)
            {
                return new quat_ahrs
                {
                    idahrs = 1,
                    calibyaw = null,
                    pitch = null,
                    quat1 = null,
                    quat2 = null,
                    quat3 = null,
                    quat4 = null,
                    roll = null,
                    temperature = null,
                    time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    yaw = null
                };
            }
            

            foreach (quat_ahrs odczyt in odczytyAhrs)
            {
                if (Convert.ToDateTime(odczyt.time).ToOADate() > czasDouble)
                    break;

                wynik = odczyt;
            }

            return wynik;
        }
        //Szukanie najbliższej pozycji z DVL water
        public static dvl_water szukajPozycjiWater(DateTime czas, List<dvl_water> wynikiWater)
        {
            double czasDouble = czas.ToOADate();
            dvl_water wynik = new dvl_water();

            if (wynikiWater.Count != 0)   
            wynik = wynikiWater.ElementAt(0);

            if (wynikiWater.Count == 0 || Convert.ToDateTime(wynik.local_time).ToOADate() > czasDouble)
            {
                return new dvl_water
                {
                    iddvl = 1,
                    d1 = null,
                    d2 = null,
                    d3 = null,
                    d4 = null,
                    dt1 = null,
                    dt2 = null,
                    fom = null,
                    device_time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    local_time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    vx = null,
                    vy = null,
                    vz = null
                };
            }
            

            foreach (dvl_water odczyt in wynikiWater)
            {
                if (Convert.ToDateTime(odczyt.local_time).ToOADate() > czasDouble)
                    break;

                wynik = odczyt;
            }

            return wynik;
        }
        public static dvl_position_water szukajPozycjiDvlPositionW(DateTime czas, List<dvl_position_water> wynikiWater)
        {
            double czasDouble = czas.ToOADate();
            dvl_position_water wynik = new dvl_position_water();
            if (wynikiWater.Count!=0)
            wynik = wynikiWater.ElementAt(0);
            if (wynikiWater.Count == 0 || Convert.ToDateTime(wynik.local_time).ToOADate() > czasDouble)
            {
                return new dvl_position_water
                {
                    iddvl = 0,
                    device_time = new DateTime(2000, 1, 1, 12, 0, 0,111),
                    local_time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    vx = null,
                    vy = null,
                    vz = null,
                    alt = null,
                    currx = null,
                    curry = null,
                    currz = null,
                    lat = null,
                    lon = null,
                    x = null,
                    y = null,
                    yawsource= null,
                    z = null
                };
            }
            

            foreach (dvl_position_water odczyt in wynikiWater)
            {
                if (Convert.ToDateTime(odczyt.local_time).ToOADate() > czasDouble)
                    break;

                wynik = odczyt;
            }

            return wynik;
        }
        public static dvl_position szukajPozycjiDvlPosition(DateTime czas, List<dvl_position> wynikiWater)
        {
            double czasDouble = czas.ToOADate();
            dvl_position wynik = new dvl_position();

            if (wynikiWater.Count!=0)
            wynik = wynikiWater.ElementAt(0);

            if (wynikiWater.Count == 0 || Convert.ToDateTime(wynik.local_time).ToOADate() > czasDouble)
            {
                return new dvl_position
                {
                    iddvl = 1,
                    device_time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    local_time = new DateTime(2000, 1, 1, 12, 0, 0, 111),
                    vx = null,
                    vy = null,
                    vz = null,
                    alt = null,
                    lat = null,
                    lon = null,
                    x = null,
                    y = null,
                    yawsource = "X",
                    z = null,
                    lat0 = null,
                    lon0 = null
                };
            }
            

            foreach (dvl_position odczyt in wynikiWater)
            {
                if (Convert.ToDateTime(odczyt.local_time).ToOADate() > czasDouble)
                    break;

                wynik = odczyt;
            }

            return wynik;
        }     
    }
    public static class FindElementsByTimePostProcessing
    {
        public static bool LiczLocal_flag { set; get; }

        //Szukanie najbliższej pozycji z gps
        public static gps szukajPozycjiGPS(DateTime czas, List<gps> odczytyGPS)
        {

            double czasDouble = czas.ToOADate();
            gps wynik = odczytyGPS.ElementAt(0);

            foreach (gps odczyt in odczytyGPS)
            {
                if (LiczLocal_flag)
                {
                    var warunek = Convert.ToDateTime(odczyt.local_time).ToOADate();
                    if (warunek > czasDouble)
                    {
                        if (warunek - czasDouble < czasDouble - Convert.ToDateTime(wynik.local_time).ToOADate())
                        {
                            wynik = odczyt;
                        }

                        break;
                    }
                    wynik = odczyt;
                }
                else
                {
                    var warunek = Convert.ToDateTime(odczyt.device_time).ToOADate();
                    if (warunek > czasDouble)
                    {
                        if (warunek - czasDouble < czasDouble - Convert.ToDateTime(wynik.device_time).ToOADate())
                        {
                            wynik = odczyt;
                        }

                        break;
                    }
                    wynik = odczyt;
                }
            }

            return wynik;

        }
        //szukanie najblizszej pozycji ahrs
        public static quat_ahrs szukajPozycjiAhrs(DateTime czas, List<quat_ahrs> odczytyAhrs)
        {

            double czasDouble = czas.ToOADate();
            quat_ahrs wynik = odczytyAhrs.ElementAt(0);

            foreach (quat_ahrs odczyt in odczytyAhrs)
            {

                var warunek = Convert.ToDateTime(odczyt.time).ToOADate();
                if (warunek > czasDouble)
                {
                    if (warunek - czasDouble < czasDouble - Convert.ToDateTime(wynik.time).ToOADate())
                    {
                        wynik = odczyt;
                    }

                    break;
                }

                wynik = odczyt;
            }

            return wynik;

        }
        //Szukanie najbliższej pozycji z DVL water
        public static dvl_position_water szukajPozycjiWater(DateTime czas, List<dvl_position_water> wynikiWater, double limit)
        {
            dvl_position_water wynik = null;
            if (wynikiWater.Count != 0)
            {
                double czasDouble = czas.ToOADate();
                 wynik= wynikiWater.ElementAt(0);

                foreach (dvl_position_water odczyt in wynikiWater)
                {
                    if (LiczLocal_flag)
                    {
                        var warunek = Convert.ToDateTime(odczyt.local_time).ToOADate();
                        if (warunek > czasDouble)
                        {
                            if (warunek - czasDouble < czasDouble - Convert.ToDateTime(wynik.local_time).ToOADate())
                            {
                                wynik = odczyt;
                            }

                            break;
                        }
                        wynik = odczyt;
                    }
                    else
                    {
                        var warunek = Convert.ToDateTime(odczyt.device_time).ToOADate();
                        if (warunek > czasDouble)
                        {
                            if (warunek - czasDouble < czasDouble - Convert.ToDateTime(wynik.device_time).ToOADate())
                            {
                                wynik = odczyt;
                            }

                            break;
                        }
                        wynik = odczyt;
                    }
                }
            }

            return wynik;

        }
    }
}
