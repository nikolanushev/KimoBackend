using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using KimoBackend.Data;
using KimoBackend.Models;

namespace KimoBackend.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUser([ScopedService] ApplicationDbContext context)
        {
            return context.Users;
        }

        
    }
}