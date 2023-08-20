using System.ComponentModel.DataAnnotations;

namespace CommentsService.Dtos
{
    public class CommentCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int CommentId { get; set; }
    }
}