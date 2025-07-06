namespace E_exam.DTOs.StudentExamDTO
{
    public class StudentExamDetailsDTO
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string ExamTitle { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public bool Passed { get; set; }
    }
}
