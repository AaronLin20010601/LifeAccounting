using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models.Entities;

namespace LifeAccounting_Backend.Models
{
    public class LifeAccountingDbContext : DbContext
    {
        // Code First Migration 資料庫 context
        public LifeAccountingDbContext(DbContextOptions<LifeAccountingDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Record> Records { get; set; } = null!;
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 確保不會有重複的 email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 刪除使用者時 同時刪除所有帳戶
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 刪除使用者時 同時刪除所有收支類型
            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 刪除使用者時 同時刪除所有收支紀錄
            modelBuilder.Entity<Record>()
                .HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 刪除帳戶時 同時刪除所有收支紀錄
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Account)
                .WithMany(a => a.Records)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // 刪除收支類型時 將收支紀錄的類型改為空值
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
