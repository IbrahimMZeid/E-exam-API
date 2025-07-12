using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    // This model represents the answers given by students for each exam question
    // It includes the selected option, whether the answer is correct, and the marks awarded

    [PrimaryKey(nameof(StudentId), nameof(ExamId), nameof(SelectedOptionId))]
    public class StudentAnswers
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int ExamId { get; set; }
        public Exam? Exam { get; set; }
        [ForeignKey("SelectedOption")]
        public int SelectedOptionId { get; set; }
        public virtual Option? SelectedOption { get; set; }

        public bool IsCorrect { get; set; }
        public int Mark { get; set; }
    }
}
