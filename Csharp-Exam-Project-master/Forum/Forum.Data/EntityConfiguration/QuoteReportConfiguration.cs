using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.EntityConfiguration
{
    public class QuoteReportConfiguration : IEntityTypeConfiguration<QuoteReport>
    {
        public void Configure(EntityTypeBuilder<QuoteReport> builder)
        {
            builder
                .HasKey(qr => qr.Id);

            builder
                .HasOne(qr => qr.Author)
                .WithMany(u => u.ReportedQuotes)
                .HasForeignKey(qr => qr.AuthorId);

            builder
                .HasOne(qr => qr.Quote)
                .WithMany(p => p.Reports)
                .HasForeignKey(qr => qr.QuoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}