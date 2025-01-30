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

        }
    }
}

