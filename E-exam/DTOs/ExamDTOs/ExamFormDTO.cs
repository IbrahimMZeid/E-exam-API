﻿using E_exam.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace E_exam.DTOs.ExamDTOs
{
    public class ExamFormDTO
    {
        [Ignore]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TotalMarks { get; set; }
        public int DurationInMinites { get; set; }
        [Required]
        public int PassMark { get; set; }
        public bool IsPublished { get; set; } = false;
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public virtual ICollection<int> ExamQuestions { get; set; } = new List<int>();
    }
}
