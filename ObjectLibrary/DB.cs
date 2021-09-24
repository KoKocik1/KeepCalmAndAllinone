using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PartyMaker
{
    public class DBConnect
    {
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {

        }

        //Initialize values

        private string PrepareConnectionString()
        {
            server = "localhost";
            database = "srrobotics";
            uid = "srrobotics";
            password = "Rysiek47";
            string connectionString;
            connectionString = "server=" + server + "; user id=" + uid + "; " +
                                      "database=" + database + "; Password=" + password;
            return connectionString;
        }
        //open connection to database
        public MySqlConnection OpenConnection()
        {
            try
            {
                var connection = new MySqlConnection(PrepareConnectionString());
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {

                switch (ex.Number)
                {
                    case 0:
                        Console.Out.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.Out.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return null;
            }

        }

    }

    public class DBWriter
    {
        private MySqlConnection connection;

        public DBWriter(MySqlConnection connect)
        {
            this.connection = connect;
        }





        public void AddEntry(dvl_water dvlDataW, dvl_position_water dvlDataPW, dvl_position dvlDataP, dvl_bottom dvlDataB, quat_ahrs ahrsData,
             gps gpsDataB, gps gpsDataR, double satHeading, double poprawkaSat, double poprawkaAhrs)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Insert allinone (stacja,misja," +
                "idAhrs,timeAhrs,temperature,pitch,roll,yaw,quat1,quat2,quat3,quat4,calibyaw," +
                "idGps_R,local_timeGps_R, device_timeGps_R, lat_R, lon_R, alt_R, fix_R,idGps_B, local_timeGps_B, device_timeGps_B, lat_B, lon_B, alt_B, fix_B," +
                "idDvl_bottom, local_timeDvl_bottom, device_timeDvl_bottom,dt1_bottom, dt2_bottom, vx_bottom, vy_bottom, vz_bottom, fom_bottom, d1_bottom, d2_bottom, d3_bottom, d4_bottom, battery_bottom, soundspeed_bottom, pressure_bottom," +
                "idDvl_water, local_timeDvl_water, device_timeDvl_water, dt1_water, dt2_water, vx_water, vy_water, vz_water, fom_water, d1_water, d2_water, d3_water, d4_water," +
                "idDvl_position, local_timeDvl_position, device_timeDvl_position, vx_position, vy_position, vz_position, x_position, y_position, z_position, lat_position, lon_position, alt_position, yawsource_position, lat0_position, lon0_position," +
                "idDvl_positionW, local_timeDvl_positionW, device_timeDvl_positionW, vx_positionW, vy_positionW, vz_positionW, currx_positionW, curry_positionW, currz_positionW, x_positionW, y_positionW, z_positionW, lat_positionW, lon_positionW, alt_positionW," +
                "kursSat, centerTime, poprawkaSat, poprawkaAhrs)" +

                "values (@stacja,@misja," +
                "@idAhrs,@timeAhrs,@temperature,@pitch,@roll,@yaw,@quat1,@quat2,@quat3,@quat4,@calibyaw," +
                "@idGps_R,@local_timeGps_R, @device_timeGps_R, @lat_R, @lon_R, @alt_R, @fix_R, @idGps_B, @local_timeGps_B, @device_timeGps_B, @lat_B, @lon_B, @alt_B, @fix_B," +
                "@idDvl_bottom, @local_timeDvl_bottom, @device_timeDvl_bottom, @dt1_bottom, @dt2_bottom, @vx_bottom, @vy_bottom, @vz_bottom, @fom_bottom, @d1_bottom, @d2_bottom, @d3_bottom, @d4_bottom, @battery_bottom, @soundspeed_bottom, @pressure_bottom," +
                "@idDvl_water,  @local_timeDvl_water, @device_timeDvl_water, @dt1_water, @dt2_water, @vx_water, @vy_water, @vz_water, @fom_water, @d1_water, @d2_water, @d3_water, @d4_water," +
                "@idDvl_position, @local_timeDvl_position, @device_timeDvl_position, @vx_position, @vy_position, @vz_position, @x_position, @y_position, @z_position, @lat_position, @lon_position, @alt_position, @yawsource_position, @lat0_position, @lon0_position," +
                "@idDvl_positionW, @local_timeDvl_positionW, @device_timeDvl_positionW, @vx_positionW, @vy_positionW, @vz_positionW, @currx_positionW, @curry_positionW, @currz_positionW, @x_positionW, @y_positionW, @z_positionW, @lat_positionW, @lon_positionW, @alt_positionW," +
                " @kursSat, @centerTime, @poprawkaSat, @poprawkaAhrs)", connection);

                cmd.Parameters.AddWithValue("@stacja", dvlDataB.stacja);
                cmd.Parameters.AddWithValue("@misja", dvlDataB.misja);

                cmd.Parameters.AddWithValue("@idAhrs", 1);
                cmd.Parameters.AddWithValue("@timeAhrs", ahrsData.time);
                cmd.Parameters.AddWithValue("@temperature", ahrsData.temperature);
                cmd.Parameters.AddWithValue("@pitch", ahrsData.pitch);
                cmd.Parameters.AddWithValue("@roll", ahrsData.roll);
                cmd.Parameters.AddWithValue("@yaw", ahrsData.yaw);
                cmd.Parameters.AddWithValue("@quat1", ahrsData.quat1);
                cmd.Parameters.AddWithValue("@quat2", ahrsData.quat2);
                cmd.Parameters.AddWithValue("@quat3", ahrsData.quat3);
                cmd.Parameters.AddWithValue("@quat4", ahrsData.quat4);
                cmd.Parameters.AddWithValue("@calibyaw", ahrsData.calibyaw);

                cmd.Parameters.AddWithValue("@idGps_R", 1);
                cmd.Parameters.AddWithValue("@local_timeGps_R", gpsDataR.local_time);
                cmd.Parameters.AddWithValue("@device_timeGps_R", gpsDataR.device_time);
                cmd.Parameters.AddWithValue("@lat_R", gpsDataR.lat);
                cmd.Parameters.AddWithValue("@lon_R", gpsDataR.lon);
                cmd.Parameters.AddWithValue("@alt_R", gpsDataR.alt);
                cmd.Parameters.AddWithValue("@fix_R", gpsDataR.fix);

                cmd.Parameters.AddWithValue("@idGps_B", 1);
                cmd.Parameters.AddWithValue("@local_timeGps_B", gpsDataB.local_time);
                cmd.Parameters.AddWithValue("@device_timeGps_B", gpsDataB.device_time);
                cmd.Parameters.AddWithValue("@lat_B", gpsDataB.lat);
                cmd.Parameters.AddWithValue("@lon_B", gpsDataB.lon);
                cmd.Parameters.AddWithValue("@alt_B", gpsDataB.alt);
                cmd.Parameters.AddWithValue("@fix_B", gpsDataB.fix);

                cmd.Parameters.AddWithValue("@idDvl_bottom", 1);
                cmd.Parameters.AddWithValue("@local_timeDvl_bottom", dvlDataB.local_time);
                cmd.Parameters.AddWithValue("@device_timeDvl_bottom", dvlDataB.device_time);
                cmd.Parameters.AddWithValue("@dt1_bottom", dvlDataB.dt1);
                cmd.Parameters.AddWithValue("@dt2_bottom", dvlDataB.dt2);
                cmd.Parameters.AddWithValue("@vx_bottom", dvlDataB.vx);
                cmd.Parameters.AddWithValue("@vy_bottom", dvlDataB.vy);
                cmd.Parameters.AddWithValue("@vz_bottom", dvlDataB.vz);
                cmd.Parameters.AddWithValue("@fom_bottom", dvlDataB.fom);
                cmd.Parameters.AddWithValue("@d1_bottom", dvlDataB.d1);
                cmd.Parameters.AddWithValue("@d2_bottom", dvlDataB.d2);
                cmd.Parameters.AddWithValue("@d3_bottom", dvlDataB.d3);
                cmd.Parameters.AddWithValue("@d4_bottom", dvlDataB.d4);
                cmd.Parameters.AddWithValue("@battery_bottom", dvlDataB.battery);
                cmd.Parameters.AddWithValue("@soundspeed_bottom", dvlDataB.soundspeed);
                cmd.Parameters.AddWithValue("@pressure_bottom", dvlDataB.pressure);

                cmd.Parameters.AddWithValue("@idDvl_water", 1);
                cmd.Parameters.AddWithValue("@local_timeDvl_water", dvlDataW.local_time);
                cmd.Parameters.AddWithValue("@device_timeDvl_water", dvlDataW.device_time);
                cmd.Parameters.AddWithValue("@dt1_water", dvlDataW.dt1);
                cmd.Parameters.AddWithValue("@dt2_water", dvlDataW.dt2);
                cmd.Parameters.AddWithValue("@vx_water", dvlDataW.vx);
                cmd.Parameters.AddWithValue("@vy_water", dvlDataW.vy);
                cmd.Parameters.AddWithValue("@vz_water", dvlDataW.vz);
                cmd.Parameters.AddWithValue("@fom_water", dvlDataW.fom);
                cmd.Parameters.AddWithValue("@d1_water", dvlDataW.d1);
                cmd.Parameters.AddWithValue("@d2_water", dvlDataW.d2);
                cmd.Parameters.AddWithValue("@d3_water", dvlDataW.d3);
                cmd.Parameters.AddWithValue("@d4_water", dvlDataW.d4);

                cmd.Parameters.AddWithValue("@idDvl_position", 1);
                cmd.Parameters.AddWithValue("@local_timeDvl_position", dvlDataP.local_time);
                cmd.Parameters.AddWithValue("@device_timeDvl_position", dvlDataP.device_time);
                cmd.Parameters.AddWithValue("@vx_position", dvlDataP.vx);
                cmd.Parameters.AddWithValue("@vy_position", dvlDataP.vy);
                cmd.Parameters.AddWithValue("@vz_position", dvlDataP.vz);
                cmd.Parameters.AddWithValue("@x_position", dvlDataP.x);
                cmd.Parameters.AddWithValue("@y_position", dvlDataP.y);
                cmd.Parameters.AddWithValue("@z_position", dvlDataP.z);
                cmd.Parameters.AddWithValue("@lat_position", dvlDataP.lat);
                cmd.Parameters.AddWithValue("@lon_position", dvlDataP.lon);
                cmd.Parameters.AddWithValue("@alt_position", dvlDataP.alt);
                cmd.Parameters.AddWithValue("@yawsource_position", dvlDataP.yawsource);
                cmd.Parameters.AddWithValue("@lat0_position", dvlDataP.lat0);
                cmd.Parameters.AddWithValue("@lon0_position", dvlDataP.lon0);

                cmd.Parameters.AddWithValue("@idDvl_positionW", 1);
                cmd.Parameters.AddWithValue("@local_timeDvl_positionW", dvlDataPW.local_time);
                cmd.Parameters.AddWithValue("@device_timeDvl_positionW", dvlDataPW.device_time);
                cmd.Parameters.AddWithValue("@vx_positionW", dvlDataPW.vx);
                cmd.Parameters.AddWithValue("@vy_positionW", dvlDataPW.vy);
                cmd.Parameters.AddWithValue("@vz_positionW", dvlDataPW.vz);
                cmd.Parameters.AddWithValue("@currx_positionW", dvlDataPW.currx);
                cmd.Parameters.AddWithValue("@curry_positionW", dvlDataPW.curry);
                cmd.Parameters.AddWithValue("@currz_positionW", dvlDataPW.currz);
                cmd.Parameters.AddWithValue("@x_positionW", dvlDataPW.x);
                cmd.Parameters.AddWithValue("@y_positionW", dvlDataPW.y);
                cmd.Parameters.AddWithValue("@z_positionW", dvlDataPW.z);
                cmd.Parameters.AddWithValue("@lat_positionW", dvlDataPW.lat);
                cmd.Parameters.AddWithValue("@lon_positionW", dvlDataPW.lon);
                cmd.Parameters.AddWithValue("@alt_positionW", dvlDataPW.alt);

                cmd.Parameters.AddWithValue("@kursSat", satHeading);

                DateTime centerTime = (DateTime)dvlDataB.local_time;
                centerTime=centerTime.AddHours(2);
                cmd.Parameters.AddWithValue("@centerTime", centerTime);

                cmd.Parameters.AddWithValue("@poprawkaSat", poprawkaSat);
                cmd.Parameters.AddWithValue("@poprawkaAhrs", poprawkaAhrs);

               // StartAddNewEntry(cmd);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
               MessageBox.Show("dupa " + ex);
            }

        }

        //***********************************************************

    }
    public static class ReadDB
    {

        //Odczyty z bazy danych
        static public List<gps> readGPS(DateTime poczatek, DateTime koniec, String rola,MySqlConnection conn)
        {
            List<gps> results = new List<gps>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM gps_measurement WHERE local_time BETWEEN @val1 AND @val2 AND role = @val3", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Parameters.AddWithValue("@val3", rola);
                cmd.Prepare();
                gps currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new gps();

                        currentRow.idgps = reader.GetInt32(2);
                        currentRow.local_time = reader.GetDateTime(5);
                        currentRow.device_time= reader.GetDateTime(6);
                        currentRow.lat = reader.GetDouble(7);
                        currentRow.lon = reader.GetDouble(8);
                        currentRow.alt = reader.GetDouble(9);
                        currentRow.fix = reader.GetInt32(10);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy gps");
            }
            return results;
        }
        static public List<quat_ahrs> readAhrs(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<quat_ahrs> results = new List<quat_ahrs>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM quat_ahrs WHERE time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                quat_ahrs currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new quat_ahrs();
                        currentRow.idahrs = reader.GetInt32(3);
                        currentRow.time = reader.GetDateTime(4);
                        currentRow.temperature = reader.GetFloat(5);
                        currentRow.pitch = reader.GetFloat(6);
                        currentRow.roll = reader.GetFloat(7);
                        currentRow.yaw = reader.GetFloat(8);
                        currentRow.calibyaw = (float)reader.GetDouble(13);
                        if (reader.IsDBNull(9))
                            currentRow.quat1 = null;
                        else
                        currentRow.quat1 = reader.GetFloat(9);

                        if (reader.IsDBNull(10))
                            currentRow.quat1 = null;
                        else
                            currentRow.quat2= reader.GetFloat(10);

                        if (reader.IsDBNull(11))
                            currentRow.quat1 = null;
                        else
                            currentRow.quat3 = reader.GetFloat(11);

                        if (reader.IsDBNull(12))
                            currentRow.quat1 = null;
                        else
                            currentRow.quat4 =reader.GetFloat(12);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy quat_ahrs");
            }
            return results;
        }

        static public List<dvl_bottom> readDVLbottom(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_bottom> results = new List<dvl_bottom>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_bottom WHERE local_time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_bottom currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_bottom();
                        currentRow.stacja = reader.GetInt32(1);
                        currentRow.misja = reader.GetInt32(2);
                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.dt1 = reader.GetDouble(6);
                        currentRow.dt2 = reader.GetDouble(7);
                        currentRow.vx = reader.GetDouble(8);
                        currentRow.vy = reader.GetDouble(9);
                        currentRow.vz = reader.GetDouble(10);
                        currentRow.fom = reader.GetDouble(11);
                        currentRow.d1 = reader.GetDouble(12);
                        currentRow.d2 = reader.GetDouble(13);
                        currentRow.d3 = reader.GetDouble(14);
                        currentRow.d4 = reader.GetDouble(15);
                        currentRow.battery = reader.GetDouble(16);
                        currentRow.soundspeed = reader.GetDouble(16);
                        currentRow.pressure = reader.GetDouble(17);
                        currentRow.temperature = reader.GetDouble(18);

                        results.Add(currentRow);
                    }
                }
            }
            catch( Exception ex)
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_bottom"+ex);
            }
            return results;
        }
        static public List<dvl_position_water> readDVLpositionWaterS(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_position_water> results = new List<dvl_position_water>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_position_water WHERE yawsource='S' AND local_time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_position_water currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_position_water();

                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.vx = reader.GetDouble(6);
                        currentRow.vy = reader.GetDouble(7);
                        currentRow.vz = reader.GetDouble(8);
                        currentRow.currx = reader.GetDouble(9);
                        currentRow.curry = reader.GetDouble(10);
                        currentRow.currz = reader.GetDouble(11);
                        currentRow.x = reader.GetDouble(12);
                        currentRow.y = reader.GetDouble(13);
                        currentRow.z = reader.GetDouble(14);
                        currentRow.lat = reader.GetDouble(15);
                        currentRow.lon = reader.GetDouble(16);
                        currentRow.alt = reader.GetDouble(17);
                        currentRow.yawsource = reader.GetString(18);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_position_water");
            }
            return results;
        }
        static public List<dvl_position_water> readDVLpositionWaterA(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_position_water> results = new List<dvl_position_water>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_position_water WHERE yawsource='A' AND local_time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_position_water currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_position_water();

                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.vx = reader.GetDouble(6);
                        currentRow.vy = reader.GetDouble(7);
                        currentRow.vz = reader.GetDouble(8);
                        currentRow.currx = reader.GetDouble(9);
                        currentRow.curry = reader.GetDouble(10);
                        currentRow.currz = reader.GetDouble(11);
                        currentRow.x = reader.GetDouble(12);
                        currentRow.y = reader.GetDouble(13);
                        currentRow.z = reader.GetDouble(14);
                        currentRow.lat = reader.GetDouble(15);
                        currentRow.lon = reader.GetDouble(16);
                        currentRow.alt = reader.GetDouble(17);
                        currentRow.yawsource = reader.GetString(18);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_position_water");
            }
            return results;
        }

        static public List<dvl_position> readDVLpositionA(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_position> results = new List<dvl_position>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_position WHERE yawsource='A' AND local_time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_position currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_position();

                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.vx = reader.GetDouble(6);
                        currentRow.vy = reader.GetDouble(7);
                        currentRow.vz = reader.GetDouble(8);
                        currentRow.x = reader.GetDouble(9);
                        currentRow.y = reader.GetDouble(10);
                        currentRow.z = reader.GetDouble(11);
                        currentRow.lat = reader.GetDouble(12);
                        currentRow.lon = reader.GetDouble(13);

                        if (reader.IsDBNull(14))
                            currentRow.alt = null;
                                    else
                            currentRow.alt = reader.GetDouble(14);

                        currentRow.yawsource = reader.GetString(15);
                        currentRow.lat0 =reader.GetDouble(16);
                        currentRow.lon0 = reader.GetDouble(17);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_position");
            }
            return results;
        }
        static public List<dvl_position> readDVLpositionS(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_position> results = new List<dvl_position>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_position WHERE yawsource='S' AND local_time BETWEEN @val1 AND @val2", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_position currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_position();

                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.vx = reader.GetDouble(6);
                        currentRow.vy = reader.GetDouble(7);
                        currentRow.vz = reader.GetDouble(8);
                        currentRow.x = reader.GetDouble(9);
                        currentRow.y = reader.GetDouble(10);
                        currentRow.z = reader.GetDouble(11);
                        currentRow.lat = reader.GetDouble(12);
                        currentRow.lon = reader.GetDouble(13);

                        if (reader.IsDBNull(14))
                            currentRow.alt = null;
                        else
                            currentRow.alt = reader.GetDouble(14);

                        currentRow.yawsource = reader.GetString(15);
                        currentRow.lat0 = reader.GetDouble(16);
                        currentRow.lon0 = reader.GetDouble(17);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_position");
            }
            return results;
        }
        static public List<dvl_water> readDVLwater(DateTime poczatek, DateTime koniec, MySqlConnection conn)
        {
            List<dvl_water> results = new List<dvl_water>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM dvl_water WHERE local_time BETWEEN @val1 AND @val2 ", conn);
                cmd.Parameters.AddWithValue("@val1", poczatek);
                cmd.Parameters.AddWithValue("@val2", koniec);
                cmd.Prepare();
                dvl_water currentRow;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Mapowanie na obiekt
                        currentRow = new dvl_water();

                        currentRow.iddvl = reader.GetInt32(3);
                        currentRow.local_time = reader.GetDateTime(4);
                        currentRow.device_time = reader.GetDateTime(5);
                        currentRow.dt1 = reader.GetDouble(6);
                        currentRow.dt2 = reader.GetDouble(7);
                        currentRow.vx = reader.GetDouble(8);
                        currentRow.vy = reader.GetDouble(9);
                        currentRow.vz = reader.GetDouble(10);
                        currentRow.fom = reader.GetDouble(11);
                        currentRow.d1 = reader.GetDouble(12);
                        currentRow.d2 = reader.GetDouble(13);
                        currentRow.d3 = reader.GetDouble(14);
                        currentRow.d4 = reader.GetDouble(15);

                        results.Add(currentRow);
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("Błąd odczytu z tablicy dvl_water");
            }
            return results;
        }
    }
}
