using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class TaskMachine
    {
        // Composite key: TaskId + MachineId
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;

        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;

        // Additional property representing priority
        public int Priority { get; set; }

        //store OperationCategory ID matched with the task
        [ForeignKey("OperationCategory")]
        public int? OperationCategoryId { get; set; }

        // Navigation property to OperationCategory
        public OperationCategory? OperationCategory { get; set; }
    }
}
