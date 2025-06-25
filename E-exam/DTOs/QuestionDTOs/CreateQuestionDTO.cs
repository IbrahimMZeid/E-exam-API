using E_exam.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_exam.DTOs.OptionDTOs;

namespace E_exam.DTOs.QuestionDTOs
{
    public class CreateQuestionDTO
    {
       
        public string Title { get; set; } = string.Empty;
        public int Score { get; set; }
        public QuestionType Type { get; set; }
        
        public int SubjectId { get; set; }
        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Medium;
        
        public virtual List<CreateOptionDTO> Options { get; set; } = new List<CreateOptionDTO>();
       
    }
}
