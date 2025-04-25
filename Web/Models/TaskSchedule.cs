using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class TaskSchedule : Entity
    {
        public int TaskId { get; set; }
        public int MachineId { get; set; }
        public int VersionId { get; set; }



        [ForeignKey("TaskId")]
        public Task Task { get; set; } = null!;

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; } = null!;

        [ForeignKey("VersionId")]
        public ScheduleVersion ScheduleVersion { get; set; } = null!;

        public int SubTasks_Total { get; set; }
        public int SubTask_Number { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public DateTime? ScheduleGenerationDate { get; set;}
                
    }
}
