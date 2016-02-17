namespace UnitTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Lanche.Entityframework.UnitOfWork;

    public partial class TestDbContext : DbContextBase
    {
        public TestDbContext()
            : base("name=TestDbContext")
        {
        }

        public virtual DbSet<Students> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
