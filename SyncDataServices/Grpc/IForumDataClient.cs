using System.Collections.Generic;
using CommentsService.Models;

namespace CommentsService.SyncDataServices.Grpc
{
    public interface IForumDataClient
    {
        IEnumerable<Forum> ReturnAllForums();
    }
}