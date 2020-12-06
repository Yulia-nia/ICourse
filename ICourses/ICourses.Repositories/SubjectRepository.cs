using ICourses.Interfaces;
using ICourses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Repositories
{
    public class SubjectRepository : ISubject
    {
        private readonly CourseDbContext _appDbContext;

        public SubjectRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task AddSubject(Subject subject)
        {
            await _appDbContext.Subjects.AddAsync(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSubject(Subject subject)
        {
            _appDbContext.Subjects.Remove(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSubjectById(Guid id)
        {
            var subject = await _appDbContext.Subjects.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.Subjects.Remove(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubject()
        {
            return await _appDbContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubject(Guid id)
        {
            return await _appDbContext.Subjects.Where(x => x.Id == id).Include(i => i.Courses).FirstOrDefaultAsync();
        }

        public async Task UpdateSubject(Subject subject)
        {
            _appDbContext.Subjects.Update(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetCourses(Subject subject)
        {
            var courses = await _appDbContext.Subjects.Where(c => c.Id == subject.Id).SelectMany(c => c.Courses).ToListAsync();
            return courses.AsReadOnly(); ;
        }

        
    }
}
