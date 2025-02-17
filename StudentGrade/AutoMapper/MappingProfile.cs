using AutoMapper;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;

namespace StudentGradeApp.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentGradeModel>()
                .ForMember(s => s.StudentNumber, b => b.MapFrom(b => b.StudentNumber))
                .ForMember(s => s.StudentName, b => b.MapFrom(b => b.StudentName))
                .ForMember(s => s.Grade, b => b.MapFrom(b => b.Grade))
                .ForMember(s => s.Remark, b => b.MapFrom(b => b.Remark))
                .ReverseMap();

            CreateMap<StudentGradeResponse, Student>()
               .ForMember(s => s.StudentNumber, b => b.MapFrom(b => b.StudentNumber))
               .ForMember(s => s.StudentName, b => b.MapFrom(b => b.StudentName))
               .ForMember(s => s.Grade, b => b.MapFrom(b => b.Grade))
               .ForMember(s => s.Remark, b => b.MapFrom(b => b.Remark))
               .ReverseMap();

            CreateMap<StudentEditGradeModel, Student>()
              .ForMember(s => s.StudentNumber, b => b.MapFrom(b => b.StudentNumber))
              .ForMember(s => s.StudentName, b => b.MapFrom(b => b.StudentName))
              .ForMember(s => s.Grade, b => b.MapFrom(b => b.Grade))
              .ForMember(s => s.Remark, b => b.MapFrom(b => b.Remark))
              .ReverseMap();

            CreateMap<StudentResponse, StudentAccount>()
             .ForMember(s => s.StudentNumber, b => b.MapFrom(b => b.StudentNumber))
             .ForMember(s => s.StudentFullName, b => b.MapFrom(b => b.StudentFullName))
             .ForMember(s => s.Address, b => b.MapFrom(b => b.Address))
             .ForMember(s => s.DateOfBirth, b => b.MapFrom(b => b.DateOfBirth))
             .ForMember(s => s.Department, b => b.MapFrom(b => b.Department))
             .ForMember(s => s.State, b => b.MapFrom(b => b.State))
             .ForMember(s => s.Phone, b => b.MapFrom(b => b.Phone))
             .ReverseMap();

            CreateMap<CourseModel, Course>()
             .ForMember(s => s.CourseCode, b => b.MapFrom(b => b.CourseCode))
             .ForMember(s => s.CourseName, b => b.MapFrom(b => b.CourseName))
             .ReverseMap();

            CreateMap<CourseResponse, Course>()
            .ForMember(s => s.CourseCode, b => b.MapFrom(b => b.CourseCode))
            .ForMember(s => s.CourseName, b => b.MapFrom(b => b.CourseName))
            .ReverseMap();
        }
    }
}

