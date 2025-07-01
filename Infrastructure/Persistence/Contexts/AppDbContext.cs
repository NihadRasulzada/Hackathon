using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AppUserOtp> AppUserOtps { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<BaseEntity>> entries = ChangeTracker.Entries<BaseEntity>();
            List<AuditLog> allAuditLogs = new List<AuditLog>();

            IEnumerable<EntityEntry<AppUser>> appUserEntries = ChangeTracker.Entries<AppUser>();
            IEnumerable<EntityEntry<AppRole>> appRoleEntries = ChangeTracker.Entries<AppRole>();

            // Process AppUser and AppRole changes manually
            allAuditLogs.AddRange(ProcessAppUserChanges(appUserEntries, AuditAction.Update));
            allAuditLogs.AddRange(ProcessAppRoleChanges(appRoleEntries, AuditAction.Update));

            foreach (EntityEntry<BaseEntity> entry in entries)
            {
                IBaseEntity<string> entity = entry.Entity;
                //get userId from context or service provider
                string userId = ""; // Replace with actual logic to get the userId, e.g., from a service or context

                if (entry.State == EntityState.Added)
                {
                    DateTime date = DateTime.UtcNow.AddHours(4);
                    foreach (IProperty property in entry.CurrentValues.Properties)
                    {
                        allAuditLogs.Add(new AuditLog
                        {
                            ChangeDate = date,
                            ChangeType = AuditAction.Create,
                            EntityId = entity.Id.ToString(),
                            EntityName = entity.GetType().Name,
                            PropertyName = property.Name,
                            UserId = userId,
                            NewValue = entry.CurrentValues[property].ToString(),
                        });
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    DateTime date = DateTime.UtcNow.AddHours(4);
                    foreach (IProperty property in entry.CurrentValues.Properties)
                    {
                        allAuditLogs.Add(new AuditLog
                        {
                            ChangeDate = date,
                            ChangeType = AuditAction.HardDelete,
                            EntityId = entity.Id.ToString(),
                            EntityName = entity.GetType().Name,
                            UserId = userId,
                        });
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    bool isDeletedOriginalValue = (bool)entry.OriginalValues["IsDeleted"];
                    bool isDeletedCurrentValue = (bool)entry.CurrentValues["IsDeleted"];

                    if (isDeletedOriginalValue != isDeletedCurrentValue)
                    {
                        DateTime date = DateTime.UtcNow.AddHours(4);
                        foreach (IProperty property in entry.CurrentValues.Properties)
                        {
                            allAuditLogs.Add(new AuditLog
                            {
                                ChangeDate = date,
                                ChangeType = AuditAction.SoftDelete,
                                EntityId = entity.Id.ToString(),
                                EntityName = entity.GetType().Name,
                                UserId = userId,
                            });
                        }
                    }
                    else
                    {
                        DateTime date = DateTime.UtcNow.AddHours(4);
                        foreach (IProperty property in entry.CurrentValues.Properties)
                        {
                            allAuditLogs.Add(new AuditLog
                            {
                                ChangeDate = date,
                                ChangeType = AuditAction.Update,
                                EntityId = entity.Id.ToString(),
                                EntityName = entity.GetType().Name,
                                UserId = userId,
                                NewValue = entry.CurrentValues[property].ToString(),
                                OldValue = entry.OriginalValues[property].ToString(),
                                PropertyName = property.Name
                            });
                        }
                    }
                }
            }

            // After all entries have been processed, save the audit logs
            if (allAuditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(allAuditLogs);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Helper method to process AppUser and AppRole changes
        private List<AuditLog> ProcessAppUserChanges(IEnumerable<EntityEntry<AppUser>> entries, AuditAction action)
        {
            List<AuditLog> auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                DateTime date = DateTime.UtcNow.AddHours(4);
                string userId = ""; // Get userId from context or service provider if needed

                foreach (IProperty property in entry.CurrentValues.Properties)
                {
                    auditLogs.Add(new AuditLog
                    {
                        ChangeDate = date,
                        ChangeType = action,
                        EntityId = entry.Entity.Id,
                        EntityName = entry.Entity.GetType().Name,
                        PropertyName = property.Name,
                        UserId = userId,
                        NewValue = entry.CurrentValues[property]?.ToString(),
                        OldValue = entry.OriginalValues[property]?.ToString()
                    });
                }
            }

            return auditLogs;
        }

        private List<AuditLog> ProcessAppRoleChanges(IEnumerable<EntityEntry<AppRole>> entries, AuditAction action)
        {
            List<AuditLog> auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                DateTime date = DateTime.UtcNow.AddHours(4);
                string userId = ""; // Get userId from context or service provider if needed

                foreach (IProperty property in entry.CurrentValues.Properties)
                {
                    auditLogs.Add(new AuditLog
                    {
                        ChangeDate = date,
                        ChangeType = action,
                        EntityId = entry.Entity.Id,
                        EntityName = entry.Entity.GetType().Name,
                        PropertyName = property.Name,
                        UserId = userId,
                        NewValue = entry.CurrentValues[property]?.ToString(),
                        OldValue = entry.OriginalValues[property]?.ToString()
                    });
                }
            }

            return auditLogs;
        }

    }
}
