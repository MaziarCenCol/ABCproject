using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class MachineUnscheduledDownTime : Entity
    {

        public int MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; } = null!;

        public DateTime DownStartTime { get; set; }

        public DateTime DownEndTime { get; set; }

    }
}