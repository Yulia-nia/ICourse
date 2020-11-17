using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICourses.Data;
using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using ICourses.Data.Repositories;
using ICourses.Services;
using ICourses.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ICourses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // переписать на сервисы
            services.AddTransient<ISubject, SubjectRepository>();
            services.AddTransient<ISubjectService, SubjectService>();
            
            services.AddTransient<ICourse, CourseRepository>();
            services.AddTransient<ICourseService, CourseService>();

            services.AddTransient<IModule, ModuleRepository>();
            services.AddTransient<IModuleService, ModuleService>();

            services.AddTransient<ITextMaterial, TextRepository>();
            services.AddTransient<ITextService, TextService>();

            services.AddTransient<IVideo, VideoRepository>();
            services.AddTransient<IVideoService, VideoService>();
            
            services.AddTransient<ILike, LikeRepository>();
            //

            services.AddTransient<IComment, CommentRepository>();
            services.AddTransient<ICommentService, CommentService>();


            services.AddDbContext<CourseDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CourseDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();          
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");    //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseStatusCodePages(async context =>
            //{
            //    context.HttpContext.Response.ContentType = "text/plain";

            //    await context.HttpContext.Response.WriteAsync(
            //        "Status code page, status code: " +
            //        context.HttpContext.Response.StatusCode);
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
