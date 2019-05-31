namespace Forum.Data.EntityConfiguration
{
    using Forum.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReplyConfiguration: IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder
                .HasKey(q => q.Id);

            builder
                .HasOne(r => r.Author)
                .WithMany(u => u.Replies)
                .HasForeignKey(r => r.AuthorId);
        }
    }
}