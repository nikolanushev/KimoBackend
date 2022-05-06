using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using KimoBackend.Data;
using KimoBackend.GraphQL.Users;
using KimoBackend.Models;

namespace KimoBackend.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<AddUserPayload> AddUserAsync(AddUser input, [ScopedService] ApplicationDbContext context)
        {
            var user = new User
            {
                FirstName = input.firstname,
                LastName = input.LastName,
                Email = input.Email
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AddUserPayload(user);
        }
    }
}