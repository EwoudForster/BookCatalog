using BookCatalog.DataLayer.Logging;
using BookCatalog.DataLayer.DataBase;
using BookCatalog.DataLayer.FileStorage.Filesystems;
using BookCatalog.DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BookCatalog.DataLayer.Repositories
{
 
    // Generic Repository for all entities
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        // List of entities, creating a logger and declaring the file system
        protected readonly List<T> _entities;
        protected readonly IGeneralLogger _logger = new Logger();
        protected readonly IFileSystem<T>? _fileSystem;
        protected readonly BookCatalogDbContext? _bookCatalogDbContext;
        protected readonly bool _dataBase;

        // using dependency injection to inject the file system as a parameter

        // File system is used to read and write data to a file
        public GenericRepository(IFileSystem<T> fileSystem)
        {
            _fileSystem = fileSystem;

            // if the file system is selected, read the data from the file
            // if the file is empty create a new one
            _entities = _fileSystem.Read().ToList();
            _dataBase = false;


        }

        // EF core is used to save and read data from the database using dependency injection
        public GenericRepository(BookCatalogDbContext bookCatalogDbContext)
        {
            // put the DbContext in a variable that is private and read only
            _bookCatalogDbContext = bookCatalogDbContext;
            _dataBase = true;

        }

        // Get all entities
        public IEnumerable<T> GetAll()
        {
            if (_dataBase)
            {
                return _bookCatalogDbContext.Set<T>().ToList();
            }
            return _entities;
        }

        // Get entity by id
        public T GetById(Guid id)
        {
            if (_dataBase)
            {
                return _bookCatalogDbContext.Set<T>().FirstOrDefault(e => e.Id == id);
            }
            
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        // Add entity
        public void Add(T item)
        {
            var alreadyExists = false;
            if (_bookCatalogDbContext != null)
            {
                alreadyExists = _bookCatalogDbContext.Set<T>().Any(e => e.Id == item.Id);
            }
            else
            {
                alreadyExists = _entities.Any(e => e.Id == item.Id);
            }

            try { 
                if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                if (alreadyExists)
                {
                    throw new InvalidOperationException($"An Item with this ID {item.ToString} already exists");
                }
                if (_dataBase)
                {
                    _bookCatalogDbContext.Set<T>().Add(item);
                }
                else
                {
                    _entities.Add(item);
                }
                Save();
                _logger.Log("Item Added: ", item);
            }
            catch(Exception ex) 
            { 
                _logger.Error(ex);
            }
            
        }

        // Delete entity
        public void Delete(Guid id)
        {
            try
            {
                var itemToDelete = GetById(id);
                if (itemToDelete != null)
                {
                    if (_dataBase)
                    {
                        _bookCatalogDbContext.Set<T>().Remove(itemToDelete);
                    }
                    else
                    {
                        _entities.Remove(itemToDelete);
                    }
                    Save();
                    _logger.Log("Item Deleted: ", itemToDelete);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            
        }

        // Update entity
        public void Update(T item)
        {
            try
            {
                DateNow(item);
                if (_dataBase)
                {
                    _bookCatalogDbContext.Set<T>().Update(item);
                }
                else
                {
                    var temp = _entities.SingleOrDefault(e => e.Id == item.Id);
                    if (temp != null)
                    {
                        _entities.Remove(temp);
                        _entities.Add(item);
                    }
                }
                _logger.Log("Item Updated: ", item);
                Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        // Save entities
        public void Save()
        {
            if (_dataBase)
            {
                _bookCatalogDbContext.SaveChanges();
            }
            else
            {
                _fileSystem.Save(_entities);
            }
        }

        // Date now
        private static void DateNow(T item)
        {
            item.LastUpdated = DateTime.Now;
        }


    }
}
