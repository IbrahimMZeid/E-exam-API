﻿namespace E_exam.DTOs.ExamDTOs
{
    public class ExamListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = false;
        public int DurationInMinites { get; set; }
        public int? QuestionsCount { get; set; }
    }
}
