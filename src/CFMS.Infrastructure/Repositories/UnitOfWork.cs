using CFMS.Domain.Interfaces;
using CFMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using Task = CFMS.Domain.Entities.Task;

namespace CFMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private CfmsDbContext context;
        private IDbContextTransaction _transaction;

        private GenericRepository<Assignment> assignmentRepository;
        private GenericRepository<BreedingArea> breedingAreaRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<ChickenBatch> chickenBatchRepository;
        private GenericRepository<ChickenCoop> chickenCoopRepository;
        private GenericRepository<CoopEquipment> coopEquipmentRepository;
        private GenericRepository<Equipment> equipmentRepository;
        private GenericRepository<Farm> farmRepository;
        private GenericRepository<FarmEmployee> farmEmployeeRepository;
        private GenericRepository<Food> foodRepository;
        private GenericRepository<HealthLog> healthLogRepository;
        private GenericRepository<HealthLogDetail> healthLogDetailRepository;
        private GenericRepository<Notification> notificationRepository;
        private GenericRepository<QuantityLog> quantityLogRepository;
        private GenericRepository<Request> requestRepository;
        private GenericRepository<SubCategory> subCategoryRepository;
        private GenericRepository<Task> taskRepository;
        private GenericRepository<TaskLog> taskLogRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<VaccinationEmployee> vaccinationEmployeeRepository;
        private GenericRepository<WareTransaction> wareTransactionRepository;
        private GenericRepository<Warehouse> warehouseRepository;
        private GenericRepository<RevokedToken> revokedTokenRepository;

        public UnitOfWork(CfmsDbContext _context)
        {
            context = _context;
        }

        public IGenericRepository<Assignment> AssignmentRepository => assignmentRepository ??= new GenericRepository<Assignment>(context);
        public IGenericRepository<BreedingArea> BreedingAreaRepository => breedingAreaRepository ??= new GenericRepository<BreedingArea>(context);
        public IGenericRepository<Category> CategoryRepository => categoryRepository ??= new GenericRepository<Category>(context);
        public IGenericRepository<ChickenBatch> ChickenBatchRepository => chickenBatchRepository ??= new GenericRepository<ChickenBatch>(context);
        public IGenericRepository<ChickenCoop> ChickenCoopRepository => chickenCoopRepository ??= new GenericRepository<ChickenCoop>(context);
        public IGenericRepository<CoopEquipment> CoopEquipmentRepository => coopEquipmentRepository ??= new GenericRepository<CoopEquipment>(context);
        public IGenericRepository<Equipment> EquipmentRepository => equipmentRepository ??= new GenericRepository<Equipment>(context);
        public IGenericRepository<Farm> FarmRepository => farmRepository ??= new GenericRepository<Farm>(context);
        public IGenericRepository<FarmEmployee> FarmEmployeeRepository => farmEmployeeRepository ??= new GenericRepository<FarmEmployee>(context);
        public IGenericRepository<Food> FoodRepository => foodRepository ??= new GenericRepository<Food>(context);
        public IGenericRepository<HealthLog> HealthLogRepository => healthLogRepository ??= new GenericRepository<HealthLog>(context);
        public IGenericRepository<HealthLogDetail> HealthLogDetailRepository => healthLogDetailRepository ??= new GenericRepository<HealthLogDetail>(context);
        public IGenericRepository<SubCategory> SubCategoryRepository => subCategoryRepository ??= new GenericRepository<SubCategory>(context);
        public IGenericRepository<Task> TaskRepository => taskRepository ??= new GenericRepository<Task>(context);
        public IGenericRepository<TaskLog> TaskLogRepository => taskLogRepository ??= new GenericRepository<TaskLog>(context);
        public IGenericRepository<User> UserRepository => userRepository ??= new GenericRepository<User>(context);
        public IGenericRepository<VaccinationEmployee> VaccinationEmployeeRepository => vaccinationEmployeeRepository ??= new GenericRepository<VaccinationEmployee>(context);
        public IGenericRepository<WareTransaction> WareTransactionRepository => wareTransactionRepository ??= new GenericRepository<WareTransaction>(context);
        public IGenericRepository<Warehouse> WarehouseRepository => warehouseRepository ??= new GenericRepository<Warehouse>(context);
        public IGenericRepository<Notification> NotificationRepository => notificationRepository ??= new GenericRepository<Notification>(context);
        public IGenericRepository<QuantityLog> QuantityLogRepository => quantityLogRepository ??= new GenericRepository<QuantityLog>(context);
        public IGenericRepository<Request> RequestRepository => requestRepository ??= new GenericRepository<Request>(context);
        public IGenericRepository<RevokedToken> RevokedTokenRepository => revokedTokenRepository ??= new GenericRepository<RevokedToken>(context);

        public void Save()
        {
            var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }
            context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }

            return context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Transaction action cannot be null");

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                T result = await action(); 
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
