using ICourses.Entities;
using ICourses.Interfaces;
using ICourses.Repositories;
using ICourses.Services;
using System.Collections.Generic;
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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.IO;
using ICourses.Logger;
using ICourses.SignalR;

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
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization();// добавляем локализацию представлений;

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
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IComment, CommentRepository>();
            services.AddTransient<ICommentService, CommentService>();


            services.AddDbContext<CourseDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ICourses.Entities.User, IdentityRole>()
                .AddEntityFrameworkStores<CourseDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication()
            //       .AddGoogle(options =>
            //       {
            //           options.ClientId = "454857910144-4f2e8u6ulf7reicpf3a47m4bo7117us4.apps.googleusercontent.com";
            //           options.ClientSecret = "ilVna-yu_xSBxMAofOXRBRwC";
            //       });

            services.AddMvc();          
            services.AddSession();
            services.AddSignalR();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("pl")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/error-local-development");
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
            app.UseForwardedHeaders();

            app.UseDeveloperExceptionPage();

            //400-499
            app.UseStatusCodePages();
           
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            //app.UseRequestLocalization();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            
            //var logger = loggerFactory.CreateLogger("FileLogger");

            //var loggerfactory = LoggerFactory.Create(builder => builder.ClearProviders());
            //ILogger logger = loggerfactory.AddFileProvider("configs/Log.txt")
            //                .CreateLogger<Startup>();
            //logger.LogInformation("Error info");


            app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync($"Ops. Errooor!: {context.Request.Query["code"]}");
            }));


            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/Chat");

            });
        }
    }
}
