using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Operation : Entity
    {   // Id, name, description

        public string? OperationCode { get; set; }

        public string? OperationDescription { get; set; }

        public ICollection<Machine> Machines { get; set; } = new List<Machine>();

        //public OperationType OperationType { get; set; }

    }
}


//public enum OperationType
//{
//    Machining,
//    Inspection,
//    HoldingOn
//}


