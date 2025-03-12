using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CFMS.Infrastructure.Interceptors
{
    public class AuditInterceptor : ISaveChangesInterceptor
    {
        // Todo: try to fix the issue with _currnetUserId is null, handle all the possible cases, and go here to remove the need of using a default user id.
        private readonly ICurrentUserService _currentUser;

        public AuditInterceptor(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var currnetUserId = Guid.Parse(_currentUser?.GetUserId()!);

            foreach (var entry in eventData.Context!.ChangeTracker.Entries<EntityAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedByUserId = currnetUserId;
                        entry.Entity.CreatedWhen = DateTime.UtcNow;
                        entry.Entity.LastEditedByUserId = currnetUserId;
                        entry.Entity.CreatedWhen = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastEditedByUserId = currnetUserId;
                        entry.Entity.LastEditedWhen = DateTime.UtcNow;
                        break;
                }
            }

            return new ValueTask<InterceptionResult<int>>(result);
        }

        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            // This method can be left empty if you only intend to use async saving changes.
            return result;
        }
    }
}
