using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PartyMaker
{
    public static class GeoCalc
    {
        public static double calcSatHeading(gps baza, gps rover, double correction)
        {
            IGeodesicLine line = Geodesic.WGS84.InverseLine((double)baza.lat, (double)baza.lon, (double)rover.lat, (double)rover.lon);
            double heading = line.Azimuth - correction;

            if (heading < -180)
                heading += 360;
            if (heading > 180)
                heading -= 360;
            return heading;
        }
        public static double calcGeometricCorrection(double baseX, double baseY, double rovX, double rovY)
        {
            double dx = (double)(rovX - baseX);
            double dy = (double)(rovY - baseY);
            return ((Math.Atan2(dx, dy)) * (180 / Math.PI));

        }

        //Obrot wektora o zadane katy orientacji przestrzennej. Kurs podawac z wniesionymi poprawkami.
        public static Vector3 rotate(Vector3 vectorForRotation, float yaw, float pitch, float roll)
        {
            //!!! katy sa podejrzane
            //Quaternion quat = Quaternion.CreateFromYawPitchRoll(Convert.ToSingle(yaw * (Math.PI / 180.0)), pitch, roll);
            //Wersja bez kwaternionów - następny krok


            Quaternion quat = Quaternion.CreateFromYawPitchRoll(Convert.ToSingle(pitch * (Math.PI / 180.0)),
                Convert.ToSingle(roll * (Math.PI / 180.0)), Convert.ToSingle(yaw * (Math.PI / 180.0)));
            return Vector3.Transform(vectorForRotation, quat);
        }

        //Kalibracja
        public static double calcHeading(double lat1, double lon1, double lat2, double lon2)
        {
            IGeodesicLine line = Geodesic.WGS84.InverseLine(lat1, lon1, lat2, lon2);
            return line.Azimuth;
        }
        //Calkowanie metoda trapezow
        public static double integrate(double seconds, double? prevValue, double? currValue, double? prevIntegratedValue)
        {
            return ((((double)prevValue + (double)currValue) / 2.0) * seconds) + (double)prevIntegratedValue;
        }
    }
    public static class Calkowanie
    {
        public static bool LiczLocal_flag { set; get; }

        //Calkowanie pozycji DVL wzgledem dna
        public static dvl_position integrationDvlBottom(float yaw, float pitch, float roll, dvl_position dvlPrev, dvl_bottom dvlCurr)
        {
            DateTime? czasPrev, czasCurr;
            if (LiczLocal_flag)
            {
                czasPrev = dvlPrev.local_time;
                czasCurr = dvlCurr.local_time;
            }
            else
            {
                czasCurr = dvlCurr.device_time;
                czasPrev = dvlPrev.device_time;
            }
            //TO DO: sprawdzic czy wraca w NED
            Vector3 xyzCurr = new Vector3(Convert.ToSingle(dvlCurr.vx), Convert.ToSingle(dvlCurr.vy),
                Convert.ToSingle(dvlCurr.vz));
            xyzCurr = GeoCalc.rotate(xyzCurr, yaw, pitch, roll);
            double x = 0;
            double y = 0;
            double z = 0;
            double lat_nn;
            double lon_nn;
            double? lat = null;
            double? lon = null;
            if (dvlPrev.device_time.HasValue)
            {
                double dt = ((TimeSpan)((czasCurr - czasPrev))).TotalSeconds;
                x = GeoCalc.integrate(dt, dvlPrev.vx, xyzCurr.X, dvlPrev.x);
                y = GeoCalc.integrate(dt, dvlPrev.vy, xyzCurr.Y, dvlPrev.y);
                z = GeoCalc.integrate(dt, dvlPrev.vz, xyzCurr.Z, dvlPrev.z);
                if (dvlPrev.lat.HasValue && dvlPrev.lon.HasValue)
                {
                    IGeodesicLine wektor = Geodesic.WGS84.DirectLine((double)dvlPrev.lat, (double)dvlPrev.lon,
                        (Math.Atan2((y - (double)dvlPrev.y), (x - (double)dvlPrev.x)) * (180 / Math.PI)),
                        Math.Sqrt(Math.Pow((x - (double)dvlPrev.x), 2) + Math.Pow((y - (double)dvlPrev.y), 2)));
                    wektor.Position(Math.Sqrt(Math.Pow((x - (double)dvlPrev.x), 2) + Math.Pow((y - (double)dvlPrev.y), 2)),
                        out lat_nn, out lon_nn);
                    lat = lat_nn;
                    lon = lon_nn;
                }
            }
            dvl_position result = new dvl_position
            {
                iddvl = dvlCurr.iddvl,
                local_time = dvlCurr.local_time,
                device_time = dvlCurr.device_time,
                vx = xyzCurr.X,
                vy = xyzCurr.Y,
                vz = xyzCurr.Z,
                x = x,
                y = y,
                z = z,
                lat = lat,
                lon = lon
            };
            return result;
        }



        //Calkowanie pozycji DVL wzgledem wody
        public static dvl_position_water integrationDvlWater(float yaw, float pitch, float roll, dvl_position_water dvlPrev, dvl_water dvlCurr)
        {
            //TO DO: sprawdzic czy wraca w NED
            Vector3 xyzCurr = new Vector3(Convert.ToSingle(dvlCurr.vx), Convert.ToSingle(dvlCurr.vy),
            Convert.ToSingle(dvlCurr.vz));
            xyzCurr = GeoCalc.rotate(xyzCurr, yaw, pitch, roll);
            double x = 0;
            double y = 0;
            double z = 0;
            double lat_nn;
            double lon_nn;
            double? lat = null;
            double? lon = null;

            DateTime? czasPrev, czasCurr;
            if (LiczLocal_flag)
            {
                czasPrev = dvlPrev.local_time;
                czasCurr = dvlCurr.local_time;
            }
            else
            {
                czasCurr = dvlCurr.device_time;
                czasPrev = dvlPrev.device_time;
            }
            if (dvlPrev.device_time.HasValue)
            {
                double dt = ((TimeSpan)((czasCurr - czasPrev))).TotalSeconds;
                x = GeoCalc.integrate(dt, dvlPrev.vx, xyzCurr.X, dvlPrev.x);
                y = GeoCalc.integrate(dt, dvlPrev.vy, xyzCurr.Y, dvlPrev.y);
                z = GeoCalc.integrate(dt, dvlPrev.vz, xyzCurr.Z, dvlPrev.z);
                if (dvlPrev.lat.HasValue && dvlPrev.lon.HasValue)
                {
                    IGeodesicLine wektor = Geodesic.WGS84.DirectLine((double)dvlPrev.lat, (double)dvlPrev.lon,
                        (Math.Atan2((y - (double)dvlPrev.y), (x - (double)dvlPrev.x)) * (180 / Math.PI)),
                        Math.Sqrt(Math.Pow((x - (double)dvlPrev.x), 2) + Math.Pow((y - (double)dvlPrev.y), 2)));
                    wektor.Position(Math.Sqrt(Math.Pow((x - (double)dvlPrev.x), 2) + Math.Pow((y - (double)dvlPrev.y), 2)),
                        out lat_nn, out lon_nn);
                    lat = lat_nn;
                    lon = lon_nn;
                }
            }
            dvl_position_water result = new dvl_position_water
            {
                iddvl = dvlCurr.iddvl,
                local_time = dvlCurr.local_time,
                device_time = dvlCurr.device_time,
                vx = xyzCurr.X,
                vy = xyzCurr.Y,
                vz = xyzCurr.Z,
                x = x,
                y = y,
                z = z,
                lat = lat,
                lon = lon
            };
            return result;
        }
    }
}

