using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyMaker
{
    public class quat_ahrs
    {

        public int idahrs { get; set; }

        public Nullable<System.DateTime> time { get; set; }

        public Nullable<float> temperature { get; set; }

        public Nullable<float> pitch { get; set; }

        public Nullable<float> roll { get; set; }

        public Nullable<float> yaw { get; set; }

        public Nullable<float> quat1 { get; set; }

        public Nullable<float> quat2 { get; set; }

        public Nullable<float> quat3 { get; set; }

        public Nullable<float> quat4 { get; set; }

        public Nullable<float> calibyaw { get; set; }

    }
    public class gps
    {

        public int idgps { get; set; }

        public Nullable<System.DateTime> local_time { get; set; }
        public Nullable<System.DateTime> device_time { get; set; }

        public Nullable<double> lat { get; set; }

        public Nullable<double> lon { get; set; }

        public Nullable<double> alt { get; set; }

        public Nullable<int> fix { get; set; }

    }
    public class dvl_water
    {

        public int iddvl { get; set; }

        public Nullable<System.DateTime> local_time { get; set; }
        public Nullable<System.DateTime> device_time { get; set; }

        public Nullable<double> dt1 { get; set; }

        public Nullable<double> dt2 { get; set; }

        public Nullable<double> vx { get; set; }

        public Nullable<double> vy { get; set; }

        public Nullable<double> vz { get; set; }

        public Nullable<double> fom { get; set; }

        public Nullable<double> d1 { get; set; }

        public Nullable<double> d2 { get; set; }

        public Nullable<double> d3 { get; set; }

        public Nullable<double> d4 { get; set; }

    }
    public class dvl_position_water
    {

        public int iddvl { get; set; }

        public Nullable<System.DateTime> local_time { get; set; }
        public Nullable<System.DateTime> device_time { get; set; }

        public Nullable<double> vx { get; set; }

        public Nullable<double> vy { get; set; }

        public Nullable<double> vz { get; set; }

        public Nullable<double> currx { get; set; }

        public Nullable<double> curry { get; set; }

        public Nullable<double> currz { get; set; }

        public Nullable<double> x { get; set; }

        public Nullable<double> y { get; set; }

        public Nullable<double> z { get; set; }

        public Nullable<double> lat { get; set; }

        public Nullable<double> lon { get; set; }

        public Nullable<double> alt { get; set; }

        public string yawsource { get; set; }


    }
    public class dvl_position
    {

        public int iddvl { get; set; }

        public Nullable<System.DateTime> local_time { get; set; }
        public Nullable<System.DateTime> device_time { get; set; }

        public Nullable<double> vx { get; set; }

        public Nullable<double> vy { get; set; }

        public Nullable<double> vz { get; set; }

        public Nullable<double> x { get; set; }

        public Nullable<double> y { get; set; }

        public Nullable<double> z { get; set; }

        public Nullable<double> lat { get; set; }

        public Nullable<double> lon { get; set; }

        public Nullable<double> alt { get; set; }

        public string yawsource { get; set; }

        public Nullable<double> lat0 { get; set; }

        public Nullable<double> lon0 { get; set; }

    }
    public class dvl_bottom
    {

        public int stacja { get; set; }

        public int misja { get; set; }

        public int iddvl { get; set; }

        public Nullable<System.DateTime> local_time { get; set; }
        public Nullable<System.DateTime> device_time { get; set; }

        public Nullable<double> dt1 { get; set; }

        public Nullable<double> dt2 { get; set; }

        public Nullable<double> vx { get; set; }

        public Nullable<double> vy { get; set; }

        public Nullable<double> vz { get; set; }

        public Nullable<double> fom { get; set; }

        public Nullable<double> d1 { get; set; }

        public Nullable<double> d2 { get; set; }

        public Nullable<double> d3 { get; set; }

        public Nullable<double> d4 { get; set; }

        public Nullable<double> battery { get; set; }

        public Nullable<double> soundspeed { get; set; }

        public Nullable<double> pressure { get; set; }

        public Nullable<double> temperature { get; set; }

    }
}
