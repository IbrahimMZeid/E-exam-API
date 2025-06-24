using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Option
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int  Mark { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}
