using BookCatalog.DAL.FileStorage.Filesystems;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Services.Logging;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.Repositories.nonasync
{

    // Generic Repository for all entities using a file system
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        // List of entities, creating a logger and declaring the file system
        protected List<T> _entities;
        protected readonly IGeneralLogger _logger;
        protected readonly IFileSystem<T> _fileSystem;
        protected readonly Type _entityType = typeof(T);

        // using dependency injection to inject the file system as a parameter

        // File system is used to read and write data to a file
        public FileRepository(IFileSystem<T> fileSystem, IGeneralLogger logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("logger"));
                _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem), LoggingStrings.ErrorNullArgument("filesystem"));

                // if the file system is selected, read the data from the file
                // if the file is empty create a new one
                _entities = _fileSystem.Read().ToList();
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.Error(ex, LoggingStrings.ErrorCreatingRepository(_entityType.Name));
                }
                throw;
            }
        }

        // Get all entities
        public IEnumerable<T> GetAll()
        {
            try
            {   
                // logging that we are getting the entities
                _logger.Log(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));

                // read the data from the file system
                _entities = _fileSystem.Read().ToList();


                if (_entities.Count() == 0)
                {
                    // logging that the db is empty
                    _logger.Log(LoggingStrings.WarningNoEntitiesFound(_entityType.Name));
                }
                else
                {
                    // logging that we have gotten the entities
                    _logger.Log(LoggingStrings.InfoRetrievedEntities(_entityType.Name, _entities.Count()));
                }
                // return the entities
                return _entities;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("retrieving", _entityType.Name));
                throw;
            }
  
        }

        // Get entity by id
        public T GetById(Guid id)
        {
            try
            {
                // get all entities
                GetAll();

                // logging that we are getting the entity by id
                _logger.Log(LoggingStrings.InfoRetrievedEntityId(_entityType.Name,id));

                // return the entity with the id
                var result = _entities.FirstOrDefault(e => e.Id == id);

                if (result == null)
                {
                    // logging that the entity was not found
                    _logger.Log(LoggingStrings.WarningNoEntitiyIdFound(_entityType.Name, id));
                }
                else
                {
                    // logging that we have gotten the entity by id
                    _logger.Log(LoggingStrings.InfoRetrievedEntityId(_entityType.Name, id));
                }
                return result!;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("retreiving", _entityType.Name, id));
                throw;
            }
        }

        // Add entity
        public void Add(T item)
        {
            try {

                // logging that we are adding the entity
                _logger.Log(LoggingStrings.InfoAdding(_entityType.Name));

                if (item.Id == Guid.Empty)
                {
                    // the Id is empty, create a new one
                    item.Id = Guid.NewGuid();
                    _logger.Log(LoggingStrings.InfoGuidCreated(item.Id));
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
                _entities.Add(item);

                // saving the repository
                Save();

                // logging that the entity was added
                _logger.Log(LoggingStrings.InfoAdded(_entityType.Name));
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("adding", _entityType.Name, item.Id));
                throw;
            }

        }

        // Delete entity
        public void Delete(Guid id)
        {
            try
            {
                // get the entity by id
                var itemToDelete = GetById(id);

                // if the entity exists, remove it
                if (itemToDelete != null)
                {
                    // remove the entity
                    _entities.Remove(itemToDelete);

                    // save the changes
                    Save();

                    // logging that the item has been removed
                    _logger.Log(LoggingStrings.InfoDeleted(_entityType.Name, id));
                }
                else
                {
                    throw new InvalidOperationException(LoggingStrings.ErrorDoesNotExists(_entityType.Name, id));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("deleting", _entityType.Name, id));
                throw;
            }

        }

        // Update entity
        public void Update(T item)
        {
            try
            {
                // setting the last updated date to the current time
                item.LastUpdated = DateTime.Now;

                // retreive the item by id
                var existingItem = GetById(item.Id);

                // if the item exists, update it
                if (existingItem != null)
                {
                    _logger.Log(LoggingStrings.InfoUpdating(_entityType.Name, item.Id));
                    // replacing the item
                    _entities.Remove(existingItem);
                    _entities.Add(item);

                    // logging that the item has been updated
                    _logger.Log(LoggingStrings.InfoUpdateSucces(_entityType.Name, item.Id));

                    // saving the changes
                    Save();
                }
                else
                {
                    // if the item does not exist, throw an exception
                    _logger.Error(null, LoggingStrings.ErrorDoesNotExists(_entityType.Name, item.Id));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("updating", _entityType.Name, item.Id));
            }
        }

        // Save entities
        public void Save()
        {
            try
            {
                _fileSystem.Save(_entities);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, LoggingStrings.ErrorGeneralMethod("saving", _entityType.Name));
                throw;
            }
        }


    }
}
