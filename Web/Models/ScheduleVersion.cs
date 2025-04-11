using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ScheduleVersion : Entity
    {
        public DateTime CreatedTime { get; set; }

        public string? Modification { get; set; }

        public int Version { get; set; }

    }

}

