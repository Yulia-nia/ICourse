using ICourses.Data.Interfaces;
using ICourses.Data. Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class SubjectRepository : ISubject
    {
        /*public IEnumerable<Subject> Subjects {
            get {
                return new List<Subject>(new Subject { Id = 1, Name = "Прога", Description = "Тут прогают",
                    Courses = new List<Course>(
                new Course { Name = "c++", Id = 2, Description = "ebani plusi", Language = "Russian",
                    SubjectID = 1, TagId = 3, 
                    Likes=new List<Like>(new Like
                    {
                        Id = 4,
                        CourseId = 2,
                        UserId = 5
                    }),

                }});*/
        //public ICourse _courses = new CourseRepository();

        //public IList<Subject> _subjects;
        /*= new List<Subject>(
                    new Subject
                    {
                        Id = 1,
                        Name = "Прога",
                        Description = "Typo progeri",
                        Courses = _courses.GetAllCourses().ToList(),
                        //Modified = DateTime.Now

                    },
                    new Subject
                    {
                        Id = 2,
                        Name = "Прога",
                        Description = "Typo progeri",
                        Courses = (ICollection<Course>)_courses.GetAllCourses(),
                        //Modified = DateTime.Now

                    }
                    );*/
        // public readonly IList<Subject> _subjects;

        private readonly AppDbContext _appDbContext;

        public SubjectRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void AddSubject(Subject subject)
        {
            _appDbContext.Subjects.Add(subject);
            _appDbContext.SaveChanges();
        }

        public void DeleteSubject(Subject subject)
        {
            _appDbContext.Subjects.Remove(subject);
            _appDbContext.SaveChanges();
        }

        public void DeleteSubjectById(Guid id)
        {
            var subject = _appDbContext.Subjects.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Subjects.Remove(subject);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Subject> GetAllSubject()
        {
            return _appDbContext.Subjects.ToList();
        }

        public Subject GetSubject(Guid id)
        {
            return _appDbContext.Subjects.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateSubject(Subject subject)
        {
            _appDbContext.Subjects.Update(subject);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Course> GetCourses(Subject subject)
        {
            var courses = _appDbContext.Subjects.Where(c => c.Id == subject.Id)?.SelectMany(c => c.Courses).ToList();
            return courses.AsReadOnly(); ;
        }

        
    }
}
