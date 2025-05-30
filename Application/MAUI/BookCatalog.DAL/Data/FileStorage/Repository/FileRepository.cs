using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.FileStorage;


// Generic Repository for all entities using a file system
public class FileRepository<T> : IFileRepository<T>  where T : IEntity
{
    // List of entities, creating a logger and declaring the file system
    protected readonly bool _logging = true;
    protected IEnumerable<T> _entities = new List<T>();
    protected readonly ILogger<FileRepository<T>> _logger;
    protected readonly IFileSystem<T> _fileSystem;
    protected readonly Type _entityType = typeof(T);

    // using dependency injection to inject the file system as a parameter

    // File system is used to read and write data to a file
    public FileRepository(IFileSystem<T> fileSystem, ILogger<FileRepository<T>> logger)
    {
        try
        {
            _logging = typeof(T) == typeof(LoggingEntry) ? false : true;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("logger"));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem), LoggingStrings.ErrorNullArgument("filesystem"));
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
    public async Task<IEnumerable<T>> GetAll(bool Sort = false)
    {
        try
        {
            // logging that we are getting the entities
            _logger.LogInformation(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));
            _entities = await _fileSystem.Read();
            if (_entities.Count() == 0)
            {
                // logging that the db is empty
                _logger.LogWarning(LoggingStrings.WarningNoEntitiesFound(_entityType.Name));
            }
            else
            {
                // logging that we have gotten the entities
                _logger.LogInformation(LoggingStrings.InfoRetrievedEntities(_entityType.Name, _entities.Count()));
            }
            // return the entities

            if (Sort)
                return _entities.OrderBy(e => e.CreatedAt);

            return _entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retrieving", _entityType.Name));
            throw;
        }

    }

    // Get entity by id
    public async Task<T> GetById(Guid id)
    {
        try
        {
            // get all entities
            await GetAll();

            // logging that we are getting the entity by id
            _logger.LogInformation(LoggingStrings.InfoRetrievedEntityId(_entityType.Name, id));

            // return the entity with the id
            _entities = await _fileSystem.Read();
            var result = _entities.FirstOrDefault(e => e.Id == id);
            if (result == null)
            {
                // logging that the entity was not found
                _logger.LogWarning(LoggingStrings.WarningNoEntitiyIdFound(_entityType.Name, id));
            }
            else
            {
                // logging that we have gotten the entity by id
                _logger.LogInformation(LoggingStrings.InfoRetrievedEntityId(_entityType.Name, id));
            }
            return result!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving", _entityType.Name, id));
            throw;
        }
    }

    // Add entity
    public async Task Add(T item)
    {
        try
        {
            _entities = await _fileSystem.Read();
            // logging that we are adding the entity
            if (_logging)
                _logger.LogInformation(LoggingStrings.InfoAdding(_entityType.Name));

            if (item.Id == Guid.Empty)
            {
                // the Id is empty, create a new one
                item.Id = Guid.NewGuid();
                if (_logging)
                    _logger.LogInformation(LoggingStrings.InfoGuidCreated(item.Id));
            }
            else
            {
                // check if the entity already exists
                if (_entities.Any(e => e.Id == item.Id))
                {
                    throw new InvalidOperationException(LoggingStrings.ErrorAlreadyExists(_entityType.Name, item.Id));
                }
            }

            // adding the entity
            List<T> list = _entities.ToList();
			list.Add(item);
			_entities = list;
			await Save();
            if (_logging)
                // logging that the entity was added
                _logger.LogInformation(LoggingStrings.InfoAdded(_entityType.Name));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("adding", _entityType.Name, item.Id));
            throw;
        }

    }

    // Delete entity
    public async Task Delete(Guid id)
    {
        try
        {
            // get the entity by id
            var itemToDelete = await GetById(id);

            // if the entity exists, remove it
            if (itemToDelete != null)
            {
                var list = _entities.ToList();
                // remove the entity
                list.Remove(itemToDelete);

                _entities = list;

                await Save();
                // logging that the item has been removed
                _logger.LogInformation(LoggingStrings.InfoDeleted(_entityType.Name, id));

            }
            else
            {
                throw new InvalidOperationException(LoggingStrings.ErrorDoesNotExists(_entityType.Name, id));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting", _entityType.Name, id));
        }

    }

    // Update entity
    public async Task Update(T item)
    {
        try
        {
            // setting the last updated date to the current time
            item.UpdatedAt = DateTime.Now;

            // retreive the item by id
            var existingItem = await GetById(item.Id);

            // if the item exists, update it
            if (existingItem != null)
            {
                _logger.LogInformation(LoggingStrings.InfoUpdating(_entityType.Name, item.Id));
                var list = _entities.ToList();
                // replacing the item
                list.Remove(existingItem);
                list.Add(item);

                _entities = list;
                await Save();

                // logging that the item has been updated
                _logger.LogInformation(LoggingStrings.InfoUpdateSucces(_entityType.Name, item.Id));

            }
            else
            {
                // if the item does not exist, throw an exception
                _logger.LogError(LoggingStrings.ErrorDoesNotExists(_entityType.Name, item.Id));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating", _entityType.Name, item.Id));
        }
    }

    // Save entities
    public async Task Save()
    {
        try
        {
            await _fileSystem.Save(_entities);
        }
        catch (Exception)
        {
            _logger.LogError(LoggingStrings.ErrorGeneralMethod("saving", _entityType.Name));
            throw;
        }
    }


}
