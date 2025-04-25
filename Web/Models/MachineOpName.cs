using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class MachineOpName : Entity
    {   // Id, name, description

        public string? OpName { get; set; }

        // Collection of operations that reference this MachineOpName
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();


        public ICollection<Machine> Machines { get; set; } = new List<Machine>();

    }
}
