using CommentsService.Models;
using CommentsService.SyncDataServices.Grpc;
using System;

namespace CommentsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var servicesScope  = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = servicesScope.ServiceProvider.GetService<IForumDataClient>();
                var forums = grpcClient.ReturnAllForums();
                SeedData(servicesScope.ServiceProvider.GetService<ICommentRepo>(),forums);
            }
        }
        private static void SeedData(ICommentRepo repo, IEnumerable<Forum> forums)
        {
            Console.WriteLine("Seeding new forums...");

            foreach (var forum in forums)
            {
                if(!repo.ExternalForumExists(forum.UserId, forum.ExternalID))
                {
                    repo.CreateForum(forum);
                }
                repo.SaveChanges();
            }
        }

        
    }
}