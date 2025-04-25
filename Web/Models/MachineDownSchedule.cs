using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class MachineDownSchedule : Entity
    {
        public int MachineId { get; set; }
        public int VersionId { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; } = null!;


        [ForeignKey("VersionId")]
        public ScheduleVersion ScheduleVersion { get; set; } = null!;

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public String? DownType { get; set; }

    }

    // public enum DownType
    // {
    //     Scheduled_Down,
    //     Unscheduled_Down,
    // }

}