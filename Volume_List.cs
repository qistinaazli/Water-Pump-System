using System.ComponentModel.DataAnnotations;

namespace PumpDashboard
{
    public class Volume_List
    {
        [DataType("volume")]
        public double Volume { get; set; }

        [DataType("total_volume")]
        public double Total_Volume { get; set; }

        [DataType("date_time")]
        public DateTime Date_Time { get; set; }
    }
}
