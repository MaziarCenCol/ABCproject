using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{

    //To determine machine priority on operation category
    public class MachineOperationPriority
    {   // Id, name, description

        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;

        public int OperationCategoryId { get; set; }
        public OperationCategory OperationCategory{ get; set; } = null!;

        public int Priority { get; set; }


    }
}



