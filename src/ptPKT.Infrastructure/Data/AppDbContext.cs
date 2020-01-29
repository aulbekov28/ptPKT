using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ptPKT.Core.Entities;
using ptPKT.SharedKernel.Interfaces;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ptPKT.Core.Identity;
using ptPKT.SharedKernel;

namespace ptPKT.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public readonly IDomainEventDispatcher _dispatcher;

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher) 
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationFromCurrentAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            //TODO modified by,created by. Or nove it to reposirory?
            var now = DateTime.Now;
            var changeSet = ChangeTracker.Entries<BaseEntity>();

            foreach (var changedItem in changeSet)
            {
                switch (changedItem.State)
                {
                    case EntityState.Added:
                        changedItem.Entity.CreateDate = now;
                        changedItem.Entity.ModifyDate = now;
                        //changedItem.Entity.CreatorId = 
                        //changedItem.Entity.ModifierId = 
                        break;
                    case EntityState.Modified:
                        changedItem.Entity.ModifyDate = now;
                        //changedItem.Entity.ModifierId = 
                        break;
                }
            }

            var result = base.SaveChanges();

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    _dispatcher.Dispatch(domainEvent);
                }
            }

            return result;
        }
    }
}
