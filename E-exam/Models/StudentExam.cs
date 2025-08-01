﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    //this model is the exam when student takes an exam
    // it contains the score and whether the student passed or not
    [PrimaryKey(nameof(StudentId), nameof(ExamId))]
    public class StudentExam
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
    }
}
