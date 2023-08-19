using ForumsService.Models;

namespace ForumsService.Data
{
    public class ForumRepo : IForumRepo
    {
        private readonly AppDbContext _context;

        public ForumRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateForum(int userId, Forum forum)
        {
            if(forum == null)
            {
                throw new ArgumentNullException(nameof(forum));
            }
            forum.UserId = userId;
            _context.Forums.Add(forum);

        }

        public void CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public bool ExternalUserExists(int externalUserId)
        {
             return _context.Users.Any(u => u.ExternalID == externalUserId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Forum GetForum(int userId, int forumId)
        {
            return _context.Forums
                .Where(f => f.UserId == userId && f.Id == forumId).FirstOrDefault();
        }

        public IEnumerable<Forum> GetForumsForUser(int userId)
        {
            return _context.Forums.Where(f=> f.UserId == userId)
            .OrderBy(f=>f.User.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}