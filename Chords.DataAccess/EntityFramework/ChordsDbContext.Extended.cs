using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Chords.CoreLib.DataAccess.Extensions;
using Chords.CoreLib.DataAccess.Models;
using Chords.CoreLib.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Chords.DataAccess.EntityFramework
{
    public partial class ChordsDbContext
    {
        private const string PrimaryKeyName = "Id";
        
        public override int SaveChanges()
        {
            OnBeforeSaveChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        #region Insert
        
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            PopulateGuidField(entity);
            return base.Add(entity);
        }

        public override EntityEntry Add(object entity)
        {
            PopulateGuidField(entity);
            return base.Add(entity);
        }
        
        public override ValueTask<EntityEntry> AddAsync(object entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            PopulateGuidField(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            PopulateGuidField(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        #endregion

        #region Modify

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            object[] keyValues = ReflectionHelpers.GetKeyValues(entity);
            TEntity entityDb = Find<TEntity>(keyValues);
            if (entityDb == null)
            {
                throw new Exception($"{entity.GetType()} not found with id: {keyValues}");
            }

            entityDb = ReflectionHelpers.MergeFieldsChanged(entity, entityDb);
            
            return base.Update(entityDb);
        }

        public override EntityEntry Update(object entity)
        {
            object[] keyValues = ReflectionHelpers.GetKeyValues(entity);
            object entityDb = Find(entity.GetType(), keyValues);
            if (entityDb == null)
            {
                throw new Exception($"{entity.GetType()} not found with id: {keyValues}");
            }

            ReflectionHelpers.MergeFieldsChanged(entity, entityDb);
            
            return base.Update(entity);
        }

        #endregion

        #region Remove

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            EntityEntry<TEntity> result;
            
            object[] keyValues = ReflectionHelpers.GetKeyValues(entity);
            TEntity entityDb = Find<TEntity>(keyValues);
            if (entityDb == null)
            {
                throw new Exception($"{entity.GetType()} not found with id: {keyValues}");
            }

            if (entityDb is ISoftDeletedFields softDeletedEntity)
            {
                softDeletedEntity.IsDeleted = true;
                result = base.Update(entityDb);
            }
            else
            {
                result = base.Remove(entityDb);
            }
            
            return result;
        }

        public override EntityEntry Remove(object entity)
        {
            EntityEntry result;
            
            object[] keyValues = ReflectionHelpers.GetKeyValues(entity);
            object entityDb = Find(entity.GetType(), keyValues);
            if (entityDb == null)
            {
                throw new Exception($"{entity.GetType()} not found with id: {keyValues}");
            }

            if (entityDb is ISoftDeletedFields softDeletedEntity)
            {
                softDeletedEntity.IsDeleted = true;
                result = base.Update(entityDb);
            }
            else
            {
                result = base.Remove(entityDb);
            }
            
            return result;
        }
        
        #endregion
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            
            modelBuilder.SetQueryFilterOnAllEntities<ISoftDeletedFields>(p => !p.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }

        private void OnBeforeSaveChanges()
        {
            // ChangeTracker.DetectChanges();
            //
            // var modifiedEntries = ChangeTracker.Entries();
            // List<AuditLog> auditLogs = new List<AuditLog>();
            //
            // foreach (var entry in modifiedEntries)
            // {
            //     PopulateAuditFields(entry);
            //     
            //     AuditLog auditLog = TrackingAuditLogs(entry);
            //     if (auditLog != null)
            //         auditLogs.Add(auditLog);
            // }
            //
            // if (auditLogs.Count > 0)
            // {
            //     AuditLogs.AddRange(auditLogs);
            // }
        }

        private void PopulateGuidField(object entry)
        {
            PropertyInfo primaryKey = entry.GetType().GetProperties().FirstOrDefault(_ => _.Name == PrimaryKeyName);
            if (primaryKey == null)
            {
                throw new Exception($"Primary key of {entry.GetType()} not found.");
            }
            
            string guid = Guid.NewGuid().ToString();
            primaryKey.SetValue(entry, guid, null);
        }
        
        private void PopulateAuditFields(EntityEntry entry)
        {
            var now = DateTime.Now;
            if (entry.Entity is IAuditFields entityEntry)
            {
                if (entry.State == EntityState.Added)
                {
                    entityEntry.CreatedAt = now;
                    entityEntry.UpdatedAt = now;
                    entityEntry.CreatedBy = entityEntry.UpdatedBy;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entityEntry.UpdatedAt = now;
                }
            }
        }

        private void PopulateDefaultValue(EntityEntry entry)
        {
            if (entry.Entity is ISoftDeletedFields entityEntry && 
                entry.State == EntityState.Modified &&
                !entityEntry.IsDeleted)
            {
                entityEntry.IsDeleted = false;
            }
        }
        
        // private AuditLog TrackingAuditLogs(EntityEntry entry, string userId = null)
        // {
        //     if (!(entry.Entity is IAuditLogTrail)
        //         || entry.State == EntityState.Added
        //         || entry.State == EntityState.Detached
        //         || entry.State == EntityState.Unchanged)
        //         return null;
        //
        //     if (string.IsNullOrEmpty(userId) && entry.Entity is IAuditFields entityEntry)
        //     {
        //         userId = entityEntry.UpdatedBy;
        //     } else if (string.IsNullOrEmpty(userId))
        //     {
        //         userId = "system";
        //     }
        //     
        //     var auditEntry = new AuditEntry(entry);
        //     auditEntry.ExtractEntityEntry();
        //
        //     if (auditEntry.ChangedFields.Count == 0)
        //         return null;
        //     
        //     string resourceId = auditEntry.ResourceId is string id ? id : string.Empty;
        //
        //     AuditLog auditLog = new AuditLog
        //     {
        //         Resource = auditEntry.Resource,
        //         ResourceId = resourceId,
        //         Type = (byte)auditEntry.AuditType,
        //         ChangedField = JsonSerializer.Serialize(auditEntry.ChangedFields, null),
        //         CreatedDate = DateTime.Now,
        //         CreatedBy = userId
        //     };
        //
        //     return auditLog;
        // }

    }
}