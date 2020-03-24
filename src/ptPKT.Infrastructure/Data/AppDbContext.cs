using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ptPKT.Core.Entities.BL;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Interfaces;
using ptPKT.SharedKernel;
using ptPKT.SharedKernel.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace ptPKT.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IEnvironmentService _environmentService;

        public AppDbContext() { }

        public AppDbContext(DbContextOptions options) : base(options) { }

        public AppDbContext(DbContextOptions options, IDomainEventDispatcher dispatcher) 
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public AppDbContext(DbContextOptions options, IDomainEventDispatcher dispatcher, IEnvironmentService environmentService)
            : base(options)
        {
            _dispatcher = dispatcher;
            _environmentService = environmentService;
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;
            var user = _environmentService.GetCurrentUser();
            var changeSet = ChangeTracker.Entries<BaseEntity>();

            foreach (var changedItem in changeSet)
            {
                switch (changedItem.State)
                {
                    case EntityState.Added:
                        changedItem.Entity.CreateDate = now;
                        changedItem.Entity.ModifyDate = now;
                        changedItem.Entity.OwnerId = user.Id;
                        changedItem.Entity.ModifiedBy = user.Id;
                        break;
                    case EntityState.Modified:
                        changedItem.Entity.ModifyDate = now;
                        changedItem.Entity.ModifiedBy = user.Id;
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
