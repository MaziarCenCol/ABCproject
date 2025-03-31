using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Project : Entity
    {
        public string? ProjectCode { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime FinishDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime RequestDate { get; set; }

        public List<Job>? Jobs { get; set; }
    }
}
