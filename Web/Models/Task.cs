using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Task : Entity
    {   // Id = Task Id
        [ForeignKey("Job")]
        public int JobId { get; set; }

        // Name:        Operation Code, can be modified
        // Description: Operation Description can be modified

        [ForeignKey("Operation")]
        public int OperationId { get; set; }

        public int TaskSeq { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime RequestDate { get; set; }
        public double CompletionPercentage { get; set; }
        public double RequiredProdHours { get; set; }
        public double ActualProdHours { get; set; }
        public string? OperationComment { get; set; }

        public Operation? Operation { get; set; }

        // Navigation property for the association with Machine through TaskMachine
        public ICollection<TaskMachine> TaskMachines { get; set; } = new List<TaskMachine>();
        //public Material? Material { get; set; }

        // New navigation property for one-to-many relationship
        public ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}

