namespace E_exam.DTOs.QuestionDTOs
{
    public class QuestionDisplayDTO
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public ICollection<string> Options { get; set; } = new List<string>();
    }
}
