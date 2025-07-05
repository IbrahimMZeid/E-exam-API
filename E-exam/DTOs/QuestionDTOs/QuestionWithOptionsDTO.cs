using E_exam.DTOs.OptionDTOs;
using E_exam.Models;

namespace E_exam.DTOs.QuestionDTOs
{
    public class QuestionWithOptionsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
        public QuestionType Type { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public List<OptionDTO> Options { get; set; }
    }
}
