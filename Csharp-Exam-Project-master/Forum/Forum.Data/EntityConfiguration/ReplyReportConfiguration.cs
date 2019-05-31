using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.EntityConfiguration
{
    public class ReplyReportConfiguration : IEntityTypeConfiguration<ReplyReport>
    {
        public void Configure(EntityTypeBuilder<ReplyReport> builder)
        {
            builder
                .HasKey(rr => rr.Id);

            builder
                .HasOne(rr => rr.Author)
                .WithMany(u => u.ReportedReplies)
                .HasForeignKey(rr => rr.AuthorId);

            builder
                .HasOne(rr => rr.Reply)
                .WithMany(p => p.Reports)
                .HasForeignKey(rr => rr.ReplyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}