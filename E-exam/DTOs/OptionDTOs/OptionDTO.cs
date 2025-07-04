using E_exam.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_exam.DTOs.OptionDTOs
{
    public class OptionDTO
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        //public int Mark { get; set; }
    }
}
