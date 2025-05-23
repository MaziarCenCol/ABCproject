﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ScheduleVersion : Entity
    {
        public DateTime CreatedTime { get; set; }

        public DateTime? TaskStartDateTime { get; set; }

        public string? Modification { get; set; }

        public int Version { get; set; }

        public float? ObjectiveValue { get; set; }

        public float? LowerBound { get; set; }

        public string? SolverStatus { get; set; }

        public float? Runtime { get; set; }

    }

}
