using System.ComponentModel.DataAnnotations;

namespace ForumsService.Dtos
{
    public class ForumCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int DogId { get; set; }
    }
}