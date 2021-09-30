using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyMaker
{
    public class CsvWriter : IDisposable
    {

        private StreamWriter outputFile;

        public CsvWriter(string path)
        {
            outputFile = new StreamWriter(path);
            outputFile.WriteLine("Czas;gpsB Lat;gpsB Lon; dvlB Vx;dvlB Vy;dvlB Vz;dvlB x;dvlB y;dvlB z;dvlB" +
                "Lat;dvlB Lon;dvlB alt;dvlB Lat0;dvlB Lon0; dvlW Vx;dvlW Vy;dvlW Vz;dvlW x;dvlW y;dvlW z;dvlW" +
                "Lat;dvlW Lon;dvlW alt; dvlW czas; kurs");

        }

        public void addNewLine(RotElements dvl, dvl_position dvlBData, dvl_position_water dvlWData, double kurs)
        {
            outputFile.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};" +
                                                "{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};",
                                    ((DateTime)dvlBData.device_time).ToString("dd MM yyyy HH:mm:ss"),
                                    dvl.lat, dvl.lon,
                                    dvlBData.vx, dvlBData.vy, dvlBData.vz, dvlBData.x, dvlBData.y, dvlBData.z,
                                    dvlBData.lat, dvlBData.lon, dvlBData.alt, dvlBData.lat0, dvlBData.lon0,
                                    dvlWData.vx, dvlWData.vy, dvlWData.vz, dvlWData.x, dvlWData.y, dvlWData.z,
                                    dvlWData.lat, dvlWData.lon, dvlWData.alt, dvlWData.device_time, kurs));
        }

        public void Dispose()
        {
            outputFile.Dispose();
        }
    }
}
