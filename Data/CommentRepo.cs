using CommentsService.Models;

namespace CommentsService.Data
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _context;

        public CommentRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateComment(int userId, int forumId, Comment comment)
        {
            if(comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
             Console.WriteLine($"--> Comment {comment} being created");
            _context.Comments.Add(comment);
            Console.WriteLine($"--> Comment {comment} created");
        }

        public void CreateForum(Forum forum)
        {
            if(forum == null)
            {
                throw new ArgumentNullException(nameof(forum));
            }
            _context.Forums.Add(forum);
        }

        public bool ExternalForumExists(int externalUserId, int externalForumId)
        {
             return _context.Forums.Any(f => f.ExternalID == externalForumId && f.UserId == externalUserId);
        }

        public IEnumerable<Forum> GetAllForums()
        {
            return _context.Forums.ToList();
        }

        public Comment GetComment(int userId, int forumId, int commentId)
        {
            return _context.Comments
                .Where(c => c.ForumId == forumId && c.Id == commentId && c.Forum.UserId == userId).FirstOrDefault();
        }

        public IEnumerable<Comment> GetCommentsForForum(int userId, int forumId)
        {
            return _context.Comments.Where(f=> f.ForumId == forumId)
            .OrderBy(f=>f.Forum.Title);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ForumExists(int userId, int forumId)
        {
            return _context.Forums.Any(f => f.Id == forumId && f.UserId == userId);
                                 
        }
    }
}