namespace KanbanCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FogLamp")]
    public partial class FogLamp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FogLamp()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int TestTrayId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FogLampId { get; set; }

        public int ExperienceLevelOfAssembler { get; set; }

        public bool? IsEffective { get; set; }

        public virtual TestTray TestTray { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
