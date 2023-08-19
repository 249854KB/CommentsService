using System.Collections.Generic;
using ForumsService.Models;

namespace ForumsService.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<User> ReturnAllUsers();
    }
}