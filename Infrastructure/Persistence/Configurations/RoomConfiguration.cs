using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        { // Primary Key
            builder.HasKey(r => r.Id);

            // Number mütləq, 1-dən böyük olmalıdır
            builder.Property(r => r.Number)
                   .IsRequired();

            // RoomType enum kimi saxlanılır (string və ya int ola bilər)
            builder.Property(r => r.RoomType)
                   .HasConversion<int>()  // Enum integer kimi DB-də saxlanacaq
                   .IsRequired();

            // Qiymət mütləq və decimal(10,2) kimi
            builder.Property(r => r.PricePerNight)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            // RoomStatus bool, mütləq
            builder.Property(r => r.RoomStatus)
                   .IsRequired();

       
            // Soft delete üçün IsDeleted (default false)
            builder.Property(r => r.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired();

            // Global query filter - soft delete-lənmişləri avtomatik çıxart
            builder.HasQueryFilter(r => !r.IsDeleted);

        }
    }
}
