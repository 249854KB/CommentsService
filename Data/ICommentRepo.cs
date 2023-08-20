using CommentsService.Models;

namespace CommentsService.Data
{
    public interface ICommentRepo
    {
        bool SaveChanges();
        IEnumerable<Forum> GetAllForums();
        void CreateForum(Forum forum);
        bool ForumExists(int externalForumId, int externalUserId);

        bool ExternalForumExists(int userId, int forumId);

        IEnumerable<Comment> GetCommentsForForum(int userId, int forumId);
        Comment GetComment(int userId, int forumId, int commentId);
        void CreateComment(int userId, int forumId, Comment comment);
    }
}