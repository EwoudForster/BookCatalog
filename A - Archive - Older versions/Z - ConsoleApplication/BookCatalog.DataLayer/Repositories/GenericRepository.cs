﻿using BookCatalog.DataLayer.Logging;
using BookCatalog.DataLayer.Filesystems;

namespace BookCatalog.DataLayer.Repositories
{
    // Generic Repository for all entities
    public class GenericRepository<T> : IRepository<T> where T : IEntity, new()
    {
        // List of entities, creating a logger and declaring the file system
        private readonly List<T> _entities;
        private ILogger logger = new Logger();
        private readonly IFileSystem<T> _fileSystem;

        // using dependency injection to inject the file system as a parameter

        public GenericRepository(IFileSystem<T> fileSystem)
        {
            _fileSystem = fileSystem;
            _entities = _fileSystem.Read().ToList();
        }

        // Get all entities
        public IEnumerable<T> GetAll()
        {
            return _entities;
        }

        // Get entity by id
        public T GetId(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        // Add entity
        public void Add(T item)
        {
            try { 
                if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                if (_entities.Any(e => e.Id == item.Id))
                {
                    throw new InvalidOperationException($"The Item {item.ToString} already exists");
                }
                _entities.Add(item);
                logger.Log("Item Added: ", item);
            }
            catch(Exception ex) 
            { 
                logger.Error(ex);
            }
            
        }

        // Delete entity
        public void Delete(Guid id)
        {
            try
            {
                var itemToDelete = GetId(id);
                if (itemToDelete != null)
                {
                    _entities.Remove(itemToDelete);
                    logger.Log("Item Deleted: ", itemToDelete);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
        }

        // Update entity
        public void Update(T item)
        {

            try
            {
                DateNow(item);
                var temp = _entities.SingleOrDefault(e => e.Id == item.Id);
                if (temp != null)
                {
                    _entities.Remove(temp);
                    _entities.Add(item);
                }
                logger.Log("Item Updated: ", item);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        // Save entities
        public void Save()
        {
            _fileSystem.Save(_entities);
        }

        // Date now
        private void DateNow(T item)
        {
            item.LastUpdated = DateTime.Now;
        }


    }
}
