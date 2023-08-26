using System.ComponentModel.DataAnnotations;

namespace CommentsService.Models
{
    public class Comment
    {
        [Key]
         [Required]
        public int Id { get; set; }
         [Required]
        public string Text { get; set; }
         [Required]
        public int ForumId{ get; set; }
        [Required]
        public DateTime Time {get; set; }
        public int CommentId { get; set; }
        public Forum Forum { get; set; }
    }
}