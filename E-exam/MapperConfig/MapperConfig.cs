using AutoMapper;
using E_exam.DTOs.ExamDTOs;
using E_exam.DTOs.ExamQuestionDTO;
using E_exam.DTOs.OptionDTOs;
using E_exam.DTOs.QuestionDTOs;
using E_exam.DTOs.StudentExamDTO;
using E_exam.DTOs.SubjectDTO;
using E_exam.Models;

namespace E_exam.MapperConfiq
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Subject
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            ///////////// Exam
            // Exam <==> ExamListDTM
            CreateMap<Exam, ExamListDTO>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(exam => exam.Subject.Name));
            // Exam <==> AddExamDTM
            CreateMap<Exam, ExamFormDTO>().ForMember(dest => dest.ExamQuestions,opt => opt.MapFrom(src => src.ExamQuestions.Select(eq => eq.QuestionId).ToList()));

            CreateMap<ExamFormDTO, Exam>()
            .ForMember(dest => dest.ExamQuestions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                 dest.ExamQuestions = src.ExamQuestions.Select(questionId => new ExamQuestion
                 {
                     QuestionId = questionId,
                     Exam = dest
                 }).ToList();
             });
            // Exam ==> ExamDisplayDTO
            CreateMap<Exam, ExamDisplayDTO>()
                .AfterMap((src, dest, context) => {
                    var mapper = context.Mapper;
                    dest.Questions = mapper.Map<List<QuestionDisplayDTO>>(src.ExamQuestions.Select(eq => eq.Question));
                    dest.Subject = src.Subject.Name;
                });
                //.ForMember(dest => dest.Questions, opt => opt.MapFrom(src =>  ));
            // Question -> QuestionDTO
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (int)src.Difficulty))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            // QuestionDTO -> Question (optional)
            CreateMap<QuestionDTO, Question>();

            // CreateQuestionDTO -> Question
            CreateMap<CreateQuestionDTO, Question>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
            // Question => QuestionDisplayDTO
            CreateMap<Question, QuestionDisplayDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.Title)));

            // Option -> OptionDTO
            CreateMap<Option, OptionDTO>();

            // CreateOptionDTO -> Option
            CreateMap<CreateOptionDTO, Option>();

            //ExamQuestion -> ExamQuestionDTO
            CreateMap<ExamQuestion, ExamQuestionDto>();

            // Mapping لـ StudentExam ➝ StudentExamResultDTO
            CreateMap<StudentExam, StudentExamResultDTO>()
                .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Name))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Exam.Subject.Name))
                .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.Exam.TotalMarks));

            // Mapping لـ StudentExam ➝ StudentExamDetailsDTO
            CreateMap<StudentExam, StudentExamDetailsDTO>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
            .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Exam.Subject.Name))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.Exam.TotalMarks));



        }
    }
}
