using ForumsService.Models;

namespace ForumsService.Data
{
    public interface IForumRepo
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        bool UserExists(int externalUserId);

        bool ExternalUserExists(int userId);

        IEnumerable<Forum> GetForumsForUser(int userId);
        Forum GetForum(int userId, int forumId);
        void CreateForum(int userId, Forum forum);
    }
}