using E_exam.DTOs.QuestionDTOs;

namespace E_exam.DTOs.ExamDTOs
{
    public class ExamDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = false;
        public int DurationInMinites { get; set; }
        public int? QuestionsCount { get; set; }
        public ICollection<QuestionDisplayDTO> Questions{ get; set; } = new List<QuestionDisplayDTO>();
    }
}
