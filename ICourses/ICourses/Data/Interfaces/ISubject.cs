using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ISubject
    {
        Task AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetAllSubject();
        Task DeleteSubject(Subject subject);
        Task DeleteSubjectById(Guid id);
        Task<Subject> GetSubject(Guid id);
        Task UpdateSubject(Subject subject);
        Task<IEnumerable<Course>> GetCourses(Subject subject);
    }
}
