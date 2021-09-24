using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

