using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Operation : Entity
    {   // Id, name, description

        [ForeignKey("MachineOpName")]
        public int? MachineOpNameId { get; set; }
        public MachineOpName? MachineOpName { get; set; }

        public string? OperationCode { get; set; }

        public string? OperationDescription { get; set; }


        //public OperationType OperationType { get; set; }

    }
}


//public enum OperationType
//{
//    Machining,
//    Inspection,
//    HoldingOn
//}
