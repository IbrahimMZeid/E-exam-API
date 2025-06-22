using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Answer
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }

    }
}
