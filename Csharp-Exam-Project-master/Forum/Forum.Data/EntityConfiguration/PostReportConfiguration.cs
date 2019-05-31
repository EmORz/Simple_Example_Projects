using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.EntityConfiguration
{
    public class PostReportConfiguration : IEntityTypeConfiguration<PostReport>
    {
        public void Configure(EntityTypeBuilder<PostReport> builder)
        {
            builder
                .HasKey(pr => pr.Id);

            builder
                .HasOne(pr => pr.Author)
                .WithMany(u => u.ReportedPosts)
                .HasForeignKey(pr => pr.AuthorId);

            builder
                .HasOne(pr => pr.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(pr => pr.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}