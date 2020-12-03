namespace KanbanDal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Workstation")]
    public partial class Workstation
    {
        public int WorkstationId { get; set; }

        public int? ExperienceLevel { get; set; }

        public bool IsCurrentlyWorking { get; set; }

        public int? HarnessAmount { get; set; }

        public int? ReflectorAmount { get; set; }

        public int? HousingAmount { get; set; }

        public int? LensAmount { get; set; }

        public int? BulbAmount { get; set; }

        public int? BezelAmount { get; set; }
    }
}
