namespace KanbanCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigTable")]
    public partial class ConfigTable
    {
        [Key]
        [StringLength(81)]
        public string SystemProperty { get; set; }

        [Required]
        [StringLength(24)]
        public string SystemValue { get; set; }
    }
}
