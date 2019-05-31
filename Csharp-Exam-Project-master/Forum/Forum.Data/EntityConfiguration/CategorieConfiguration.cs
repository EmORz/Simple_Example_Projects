namespace Forum.Data.EntityConfiguration
{
    using Forum.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
    {
        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasMany(c => c.Forums)
                .WithOne(f => f.Categorie)
                .HasForeignKey(f => f.CategorieId);
        }
    }
}
