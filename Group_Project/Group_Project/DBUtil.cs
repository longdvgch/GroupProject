namespace Group_Project
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBUtil : DbContext
    {
        public DBUtil()
            : base("name=DBUtil")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<SendContend> SendContends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
