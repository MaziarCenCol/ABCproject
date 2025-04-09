using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Machine : Entity
    {  // Id, Name, Description
        public string? ResourceCode { get; set; }

        // Navigation property for the association with Task through TaskMachine
        public ICollection<TaskMachine> TaskMachines { get; set; } = new List<TaskMachine>();

        public ICollection<MachineOpName> MachineOpNames { get; set; } = new List<MachineOpName>();

        public ICollection<MachineOperationPriority> MachineOperationPrioritys { get; set; } = new List<MachineOperationPriority>();

        public String? MachineCategory { get; set; }

        public double? OperationCostHourly { get; set; }
        public string? Location { get; set; }
        public int? MaxAvailableHoursPerDay { get; set; }
        public string? MachineManufacturer { get; set; }
        public string? Model { get; set; }
        public string? MachineType { get; set; }
        public string? Orientation { get; set; }
        public string? Controller { get; set; }
        public string? ControllerModel { get; set; }
        public double? Xtravel { get; set; }
        public double? XtravelInches { get; set; }
        public double? Ytravel { get; set; }
        public double? YtravelInches { get; set; }
        public double? ZtravelDaylight { get; set; }
        public double? ZtravelDaylightInches { get; set; }
        public double? Wtravel { get; set; }
        public double? TableCapacity { get; set; }
        public double? Rpm { get; set; }
        public double? RapidMMmin { get; set; }
        public double? MaxFeedMMmin { get; set; }
        public int? Year { get; set; }
        public int? ToolChangerCapacity { get; set; }
        public string? ToolHolder { get; set; }
        public double? CalibratedAccuracyMM { get; set; }
        public double? PlateLength { get; set; }
        public double? PlateLengthInches { get; set; }
        public double? PlateWidth { get; set; }
        public double? PlateWidthInches { get; set; }
        public double? MaxOpen { get; set; }
        public double? MaxOpenInches { get; set; }


    }
}

