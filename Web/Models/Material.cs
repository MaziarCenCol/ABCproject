using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Material : Entity
    {   // Id, Name, Description

        [ForeignKey("Task")]

        // Optional navigation property back to the Task
        //public Task Task { get; set; } = null!;
        public int TaskId { get; set; }
        public string? MaterialCode { get; set; }
        public string? Source { get; set; }
        public double? Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsReady { get; set; }
    }
}

