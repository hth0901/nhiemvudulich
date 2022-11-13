using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class SYS_Setting
    {
        public int ID { get; set; }
        public string GiaTri { get; set; }
        public string MoTa { get; set; }
    }

    public class SYS_SettingRequest
    {
        public int RadiusMin { get; set; }
        public int RadiusMax { get; set; }
        public string Unit { get; set; }
        public string SendEmail { get; set; }
        public string SendPassword { get; set; }
        public string SendDisplayName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public int Scheduler { get; set; }

    }
}
