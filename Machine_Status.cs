using System.ComponentModel.DataAnnotations;

namespace PumpDashboard
{
	public class Machine_Status
	{
		[DataType("mach_id")]
		public int Machine_Id { get; set; }

		[DataType("is_running")]
		public bool Is_Running { get; set; }
	}
}
