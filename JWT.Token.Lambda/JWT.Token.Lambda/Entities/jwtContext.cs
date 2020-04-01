namespace JWT.Token.Lambda.Entities
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public partial class jwtContext : DbContext
    {
        public jwtContext() { }

        public jwtContext(DbContextOptions<jwtContext> options) : base(options) { }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.UserName).IsRequired();
            });
        }
    }
}
