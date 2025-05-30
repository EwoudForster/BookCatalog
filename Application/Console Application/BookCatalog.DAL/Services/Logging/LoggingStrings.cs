using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookCatalog.DAL.Services.Logging
{
    public static class LoggingStrings
    {

        // Information

        // GET METHODS
        // For get all method
        public static string InfoRetrievingAllEntities(string entityName)
        {
            return $"🔎 Retrieving all {entityName}(s) out of the database";
        }        
        
        public static string InfoRetrievingEntityId(string entityName, Guid id)
        {
            return $"🔎 Retrieving the {entityName} out of the database with Id: {id}";
        }


        // All the entities are retreived
        public static string InfoRetrievedEntities(string entityName, int count)
        {
            return $"📦 Retrieved {count} {entityName} out of the database";
        }

        public static string InfoRetrievedEntityId(string entityName, Guid id)
        {
            return $"📦 Retrieved the {entityName} out of the database with Id: {id}";
        }

        // SEARCHING
        // For the searching method
        public static string InfoSearchingEntitiesQuery(string entityName, string query)
        {
            return $"🔎 Searching for the {entityName}(s) with the query: {query}";
        }

        // For get by id method
        public static string InfoSearchEntityID(string entityName, Guid id)
        {
            return $"🔎 Searching for the {entityName} with the id: {id.ToString()}";
        }

        // For the searching method
        public static string InfoRetreivingEntitiesQuery(string entityName, string query)
        {
            return $"📦 Retrieving the {entityName} out of the database with the query {query}";
        }

        // FILTERING
        public static string InfoFilteringBy(string entityName, string GenreToFilterOn)
        {
            return $"📇 Filtering the {entityName}(s) by the {GenreToFilterOn}";
        }

        // ORDERING
        public static string InfoOrderingBy(string entityName, string orderBy, bool desc)
        {
            string sortingWay = desc ? "Descending" : "Ascending";
            string sortingWayEmoji = desc ? "⬇️" : "⬆️";
            return $"{sortingWayEmoji} Sorting {entityName} by {orderBy} in a {sortingWay} way!";

        }

        // ADDING
        public static string InfoAdding(string name)
        {
            return $"💾 Adding {name} to the database";
        }

        internal static string? InfoGuidCreated(Guid id)
        {
            return $"🔑 Generated Id because it's empty: {id}";
        }

        public static string InfoAdded(string entityName)
        {
            return $"✅ the {entityName} was added succesfully";
        }

        // Deleting

        public static string InfoDeleted(string entityName, Guid id)
        {
            return $"🗑️ {entityName} with ID:{id} removed succesfully";
        }

        // Updating

        public static string? InfoUpdating(string entityName, Guid id)
        {
            return $"🔨 Updating {entityName} with {id}";
        }
        public static string InfoUpdateSucces(string entityName, Guid id)
        {
            return $"✅ Updated {entityName} with {id} Succesfully";
        }

        // Warning

        // No entities found
        public static string WarningNoEntitiesFound(string entityName)
        {
            return $"⚠️ There were no {entityName}(s) found";
        }

        // no entity with specific Id found
        public static string WarningNoEntitiyIdFound(string entityName, Guid id)
        {
            return $"⚠️ No {entityName} with the ID: {id} was found"
;
        }
        public static string WarningNoEntitiesFoundQuery(string entityName, string query)
        {
            return $"⚠️ There were no {entityName}(s) found that match the query: {query}";
        }

        // Errors
        // General error creating the repository
        public static string ErrorCreatingRepository(string entityName)
        {
            return $"❌ Error occurred while creating the {entityName} repository!";
        }

        public static string ErrorGeneralMethod(string action, string? entityName = null, Guid? id = null)
        {
            var result = $"❌ Error occured while {action}";
            result = string.IsNullOrWhiteSpace(entityName) ? result : result + $" the {entityName}(s)";
            return id == null ? result : result + $" with ID: {id}";
        }

        public static string ErrorNullArgument(string nameArgument)
        {
            return $"❌ Error: The {nameArgument} cannot be null";
        }

        public static string ErrorAlreadyExists(string entityName, Guid id)
        {
            return $"❌ Error: The {entityName} with ID: {id} already exists";
        }

        public static string ErrorDoesNotExists(string entityName, Guid id)
        {
            return $"❌ Error: The {entityName} with ID: {id} does not exist";
        }
    }
}
