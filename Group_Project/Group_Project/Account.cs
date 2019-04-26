namespace Group_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            SendContends = new HashSet<SendContend>();
        }

        [Key]
        public int userID { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(20)]
        public string password { get; set; }

        [StringLength(100)]
        public string fullName { get; set; }

        [StringLength(11)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(20)]
        public string role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SendContend> SendContends { get; set; }
    }
}
