using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class MachineWeeklyUpTime : Entity
    {
  
        public int MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; } = null!;

        public Weekday Weekday { get; set; }
        public int UpHour { get; set; }

        public TimeOnly OperationStartTime { get; set; }
                
    }

    public enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}