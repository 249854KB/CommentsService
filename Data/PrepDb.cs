using ForumsService.Models;
using ForumsService.SyncDataServices.Grpc;
using System;

namespace ForumsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var servicesScope  = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = servicesScope.ServiceProvider.GetService<IUserDataClient>();
                var users = grpcClient.ReturnAllUsers();
                SeedData(servicesScope.ServiceProvider.GetService<IForumRepo>(),users);
            }
        }
        private static void SeedData(IForumRepo repo, IEnumerable<User> users)
        {
            Console.WriteLine("Seeding new users...");

            foreach (var user in users)
            {
                if(!repo.ExternalUserExists(user.ExternalID))
                {
                    repo.CreateUser(user);
                }
                repo.SaveChanges();
            }
        }

        
    }
}