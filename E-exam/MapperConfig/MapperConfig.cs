using AutoMapper;
using E_exam.DTOs.ExamDTOs;
using E_exam.DTOs.OptionDTOs;
using E_exam.DTOs.QuestionDTOs;
using E_exam.Models;

namespace E_exam.MapperConfiq
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            ///////////// Exam
            // Exam <==> ExamListDTM
            CreateMap<Exam,ExamListDTO>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(exam => exam.Subject.Name));
            // Exam <==> AddExamDTM
            CreateMap<Exam, ExamFormDTO>().ForMember(dest => dest.ExamQuestions,
               opt => opt.Ignore());

            // Question -> QuestionDTO
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty.ToString()))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            // QuestionDTO -> Question (optional)
            CreateMap<QuestionDTO, Question>();

            // CreateQuestionDTO -> Question
            CreateMap<CreateQuestionDTO, Question>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));


            // Option -> OptionDTO
            CreateMap<Option, OptionDTO>();

            // CreateOptionDTO -> Option
            CreateMap<CreateOptionDTO, Option>();
        }
    }
}
