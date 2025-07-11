using E_exam.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_exam.DTOs.OptionDTOs;

namespace E_exam.DTOs.QuestionDTOs
{
    public class QuestionDTO
    {

        public int Id { get; set; }
        public string Title { get; set; } 
        public int Score { get; set; }
        public int Type { get; set; } 
        public int Difficulty { get; set; } 
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public List<OptionDTO> Options { get; set; } 
    }
}
