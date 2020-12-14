using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using ICourses.Entities;
using Moq;
using Bogus;
using NUnit.Framework;

namespace ICourses.Tests
{
    public class SubjectServiceTest
    {


       // private User _userDb;
       //// private UserInfo _userInfo;
       // //private Mock<IUser _mockUserRepository;
       // private Mock<IMapper> _mockMapper;
       // private List<User> _usersDbList;
       // //private List<UserInfo> _usersInfoList;
       
       // [OneTimeSetUp]
       // public void Init()
       // {
       //     Faker<User> fakerUsers = new Faker<User>();
       //     fakerUsers.RuleFor(u => u.UserName, b => b.Random.String(8))
       //         .RuleFor(u => u.Email, b => b.Internet.UserName())
       //         .RuleFor(u => u.SubscriptionType, b => b.Random.Int(0, 3));
       // }

       // [SetUp]
       // public void InitMock()
       // {
       //     _userDb = new User() { Id = "id", Email = "A" };
       //     //_userInfo = new UserInfo() { Login = "A" };

       //     _usersDbList = new List<UserDb>() { new UserDb() { UserId = "id", Login = "A" } };
       //     _usersInfoList = new List<UserInfo>() { new UserInfo() { Login = "A" } };

       //     _mockUserRepository = new Mock<IUserRepository>();
       //     _mockMapper = new Mock<IMapper>();
       // }

       // [Test]
       // public void Test_GetById_User()
       // {
       //     _mockMapper.Setup(m => m.Map<User, UserInfo>(_userDb)).Returns(_userInfo);
       //     _mockUserRepository.Setup(c => c.GetById(_userDb.UserId)).Returns(_userDb);

       //     UserService service = new UserService(_mockMapper.Object, _mockUserRepository.Object);
       //     var res = service.GetUserById(_userDb.UserId);

       //     res.Should().BeEquivalentTo(_userInfo);

       //     //_mockUserRepository.Verify(v => v.GetById(_userDb.UserId), Times.Once);
       //     //_mockMapper.Verify(v => v.Map<UserDb, UserInfo>(_userDb), Times.Once);
       // }
       //-------------------------------------------------------------------------------------


        ////List<Subject> _listSubject;
        //Mock<CourseDbContext> _mockContext;
        //List<Subject> _fakeSubject;
        //Faker<Subject> _fake = new Faker<Subject>().RuleFor(x => x.Name, y => y.Name.JobTitle())
        //                                             .RuleFor(x => x.Id, Guid.NewGuid());

        //[OneTimeSetUp]
        //public void Starter()
        //{
        //    _mockContext = new Mock<CourseDbContext>();
        //    _fakeSubject = _fake.Generate(3);

        //   // _mockContext.Setup(_ => _.Subjects).Returns(_fakeSubject);
        //}

        //[Test]
        //public async Task GetCourseByIdAsyncTest()
        //{

        //    using (var uow = new UnitOfWorkRepository(_mockContext.Object))
        //    {
        //        var course = _fakeSubject.FirstOrDefault();

        //        var gettedCourse = await uow.Courses.GetByIdAsync(course.Id);

        //        course.Should().BeEquivalentTo(gettedCourse);
        //    }
        //}        //[Test]
        //public async Task GetCourseByIdAsyncTest()
        //{

        //    using (var uow = new UnitOfWorkRepository(_mockContext.Object))
        //    {
        //        var course = _fakeSubject.FirstOrDefault();

        //        var gettedCourse = await uow.Courses.GetByIdAsync(course.Id);

        //        course.Should().BeEquivalentTo(gettedCourse);
        //    }
        //}

        //public void Initialize()
        //{
        //    _listSubject = new List<Subject>()
        //    {
        //        new Subject(){ Id=Guid.NewGuid(),Name = "1", Description = "1.1"},
        //        new Subject(){ Id=Guid.NewGuid(),Name = "2", Description = "2.2"},
        //        new Subject(){ Id=Guid.NewGuid(), Name = "3", Description = "3.3"}
        //    };
        //}

        //[Fact]
        //public async Task SubjectServiceTest_GetAll()
        //{
        //    var result = await _service.GetAllSubject() as List<Course>;

        //    var service = new PrimeService();
        //    bool result = primeService.IsPrime(1);

        //    Assert.False(result, "1 should not be prime");
        //}

    }
}
