using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL.Storage.DataBase;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace BookCatalog.DAL.Repositories.Generic.Async
{

    // Generic Repository for all entities using Entity Framework
    public class GenericRepositoryAsync<T> : IRepositoryAsync<T> where T : class, IEntity, new()
    {
        // List of entities, creating a logger and declaring the file system
        protected readonly ILogger<GenericRepositoryAsync<T>> _logger;
        protected readonly BookDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected readonly Type _entityType = typeof(T);


        // EF core is used to save and read data from the database using dependency injection
        public GenericRepositoryAsync(BookDbContext DbContext, ILogger<GenericRepositoryAsync<T>> logger)
        {
            try
            {
                // declare the logger and the DbContext
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                // DbContext declaration and checking if not null
                _dbContext = DbContext ?? throw new ArgumentNullException(nameof(DbContext), LoggingStrings.ErrorNullArgument("DbContext"));
                // DbContext declaration and checking if not null
                _dbSet = DbContext.Set<T>();
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(ex, LoggingStrings.ErrorCreatingRepository(_entityType.Name));
                }
                throw;
            }
        }

        // Get all entities
        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                // logging that we are getting the entities
                _logger.LogInformation(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));

                // make a query and give it in the method to add all the includes (eager loading)
                IQueryable<T> query = _dbSet.AsQueryable();
                query = ApplyIncludes(query, includes);

                List<T> result = await query.ToListAsync();

                if (result.Count() == 0)
                {
                    // logging that the db is empty
                    _logger.LogWarning(LoggingStrings.WarningNoEntitiesFound(_entityType.Name));
                }
                else
                {
                    // logging that we have gotten the entities
                    _logger.LogInformation(LoggingStrings.InfoRetrievedEntities(_entityType.Name, result.Count()));
                }
                return result;
            }
            // Catching any exceptions and logging them
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving", _entityType.Name));
                throw;
            }

        }

        // Get entity by id
        public async Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                // logging that we are getting the entity with the specific id
                _logger.LogInformation(LoggingStrings.InfoRetrievingEntityId(_entityType.Name, id));

                // creating a query and giving it in the method to get the inlcudes
                IQueryable<T> query = _dbSet.Where(e => e.Id == id);
                query = ApplyIncludes(query, includes);

                // getting the entity with the specific id
                var result = await query.FirstOrDefaultAsync();
                if (result == null)
                {
                    // logging that the entity was not found
                    _logger.LogWarning(LoggingStrings.WarningNoEntitiyIdFound(_entityType.Name, id));

                }
                else
                {
                    // logging that we got the entities
                    _logger.LogInformation(LoggingStrings.InfoRetrievedEntityId(_entityType.Name, id));
                }
                return result!;
            }
            catch (Exception ex)
            {
                // catching any exceptions and logging them
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving", _entityType.Name, id));
                throw;
            }
        }


        // Add entity
        public async Task Add(T item)
        {

            try {

                // logging that we are adding the entity
                _logger.LogInformation(LoggingStrings.InfoAdding(_entityType.Name));

                if (item.Id == Guid.Empty)
                {
                    // the Id is empty, create a new one
                    item.Id = Guid.NewGuid();
                    _logger.LogInformation(LoggingStrings.InfoGuidCreated(item.Id));
                }

                else
                {
                    // if item already exists, throw an exception
                    if (await GetById(item.Id) != null)
                    {
                        throw new InvalidOperationException(LoggingStrings.ErrorAlreadyExists(_entityType.Name, item.Id));
                    }
                }
                    // adding the entity to the database
                    await _dbSet.AddAsync(item);

                // saving the changes
                await Save();

                // logging that the entity has been added
                _logger.LogInformation(LoggingStrings.InfoAdded(_entityType.Name));
            }
            catch (InvalidOperationException ex)
            {
                // catching any exceptions and logging them
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("adding", _entityType.Name, item.Id));
                throw;
            }
            catch (Exception ex) 
            {
                // catching any exceptions and logging them
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("adding", _entityType.Name, item.Id));
                throw;
            }
            
        }

        // Delete entity
        public async Task Delete(Guid id)
        {
            try
            {
                // getting the item we want to delete
                var itemToDelete = await GetById(id);

                // if the item exists, remove it
                if (itemToDelete != null)
                {
                    // removing the item
                    _dbContext.Remove(itemToDelete);

                    // saving the changes asynchroniously
                    await Save();

                    // logging that the item has been removed
                    _logger.LogInformation(LoggingStrings.InfoDeleted(_entityType.Name, id));
                }
                else
                {
                    // if the item does not exist, throw an exception
                    throw new InvalidOperationException(LoggingStrings.ErrorDoesNotExists(_entityType.Name, id));
                }
            }
            catch (Exception ex)
            {
                // catching any exceptions and logging them
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("removing", _entityType.Name, id));
                throw;
            }
            
        }

        // Update entity
        public async Task Update(T item)
        {
            try
            {

                    // setting the last updated date to the current time
                    item.LastUpdated = DateTime.Now;

                    // logging that the item will be updated
                    _logger.LogInformation(LoggingStrings.InfoUpdating(_entityType.Name, item.Id));

                    // updating the item
                    var existing = await _dbSet.FindAsync(item.Id);
                    if(existing == null)
                    {
                        throw new DbUpdateConcurrencyException();
                    }
                    _dbContext.Entry(existing).CurrentValues.SetValues(item);
                    // logging that the item will be updated
                    _logger.LogInformation(LoggingStrings.InfoUpdateSucces(_entityType.Name, item.Id));

                    // saving the changes asynchroniously
                    await Save();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError(LoggingStrings.ErrorDoesNotExists(_entityType.Name, item.Id));
                throw;
            }
            catch (Exception ex)
            {
                // catching any exceptions and logging them
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating", _entityType.Name, item.Id));
                throw;
            }
        }

        // Save entities
        public async Task Save()
        {
            try
            {
                // saving the changes asynchroniously
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // logging any errors that might happen
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("saving", _entityType.Name));
                throw;
            }
        }

        // applying the includes where necessary
        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            // check if the includes are not null
            if (includes != null)
            {
                // if not null add them in the query
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


    }
}
