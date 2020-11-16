using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface ISubjectService
    {
        Task AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetAllSubject();
        Task DeleteSubject(Subject subject);
        Task DeleteSubjectById(Guid id);
        Task<Subject> GetSubject(Guid id);
        Task<IEnumerable<Course>> GetCourses(Subject subject);
    }
}
