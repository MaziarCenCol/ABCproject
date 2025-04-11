using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    //Type of operation like Core Block / Cavity Block/Slide/Lifter/Insert/Main Insert/Hook Slide 
    //To determine machine priority
    public class OperationCategory : Entity
    {   // Id, name, description

        // Navigation property for the join entity
        public ICollection<MachineOperationPriority> MachineOperationPrioritys { get; set; } = new List<MachineOperationPriority>();


    }
}


