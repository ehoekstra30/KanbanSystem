namespace KanbanDal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FogLamp")]
    public partial class FogLamp
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestTrayId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FogLampId { get; set; }

        public int ExperienceLevelOfAssembler { get; set; }

        public bool? IsEffective { get; set; }

        public virtual TestTray TestTray { get; set; }
    }
}
