using HotChocolate.Types;
using KimoBackend.Models;

namespace KimoBackend.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents a user of the Kimo app");
        }

    
    }
}