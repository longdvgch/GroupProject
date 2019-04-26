namespace Group_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SendContend")]
    public partial class SendContend
    {
        public int id { get; set; }

        [StringLength(200)]
        public string wordContend { get; set; }

        [StringLength(200)]
        public string imageContend { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uploaddDate { get; set; }

        public int? userID { get; set; }

        [StringLength(500)]
        public string comment { get; set; }

        [StringLength(30)]
        public string contendStatus { get; set; }

        public virtual Account Account { get; set; }
    }
}
