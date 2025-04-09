using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Job : Entity
    {
        // Id, Name, Description
        public int JobNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? RequestDate { get; set; }
        
        public double? CompletionPercentage { get; set; }
        public double? RequiredProdHours { get; set; }
        public double? ActualProdHours { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public List<Task>? Tasks { get; set; }

    }
}
