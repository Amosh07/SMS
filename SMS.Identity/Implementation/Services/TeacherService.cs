using SMS.Application.DTOs.Teacher;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class TeacherService(IGenericRepository genericRepository) : ITeacherService
    {
        public GetStudentDetailDto GetTeacherDetails(Guid Id)
        {
            var teacherModel = genericRepository.GetById<Teacher>(Id)
                 ?? throw new NotFoundException("The Teacher was not found.");

            var result = new GetStudentDetailDto()
            {
                Name = teacherModel.Name,
                Address = teacherModel.Address,
                Email = teacherModel.Email,
                Number = teacherModel.Number
            };

            return result;

        }

        public void InsertTeacher(InsertTeacherDto insertTeacherDto)
        {
            throw new NotImplementedException();
        }
    }
}
