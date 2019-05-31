using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Forum.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Forum.Web;
using Forum.Web.Middlewares;
using Forum.Services.Forum;
using Forum.Services.Post;
using Forum.Services.Category;
using Forum.Services.Db;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Forum;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Db;
using Forum.Services.Reply;
using Forum.Services.Interfaces.Reply;
using Forum.Services.Interfaces.Quote;
using Forum.Services.Quote;
using Forum.Services.Report;
using Forum.Services.Interfaces.Report;
using Forum.Services.Interfaces.Message;
using Forum.Services.Message;
using System;
using Forum.Services.Interfaces.Report.Post;
using Forum.Services.Report.Post;
using Forum.Services.Report.Reply;
using Forum.Services.Interfaces.Report.Reply;
using Forum.Services.Interfaces.Report.Quote;
using Forum.Services.Report.Quote;
using Forum.Services.Interfaces.Chat;
using Forum.Services.Chat;
using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Account;
using Forum.Services.Role;
using Forum.Services.Interfaces.Role;
using Forum.Services.Interfaces.Profile;
using Forum.Services.Profile;
using Forum.Services.Settings;
using Forum.Services.Interfaces.Settings;
using Forum.Services.Pagging;
using Forum.Services.Interfaces.Pagging;
using Forum.Web.Utilities;

namespace Forum
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = ForumProfile.RegisterMappings();

            var mapper = config.CreateMapper();

            services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services
                .AddDbContext<ForumDbContext>(options =>
                options
                   .UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<ForumUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ForumDbContext>();

            services.AddOptions();

            services.Configure<CloudConfiguration>(Configuration.GetSection("CloudConfiguration"));

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });

            //AntiforgeryToken 
            services.AddAntiforgery();

            //Registrating services
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPaggingService, PaggingService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReplyService, ReplyService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IPostReportService, PostReportService>();
            services.AddScoped<IReplyReportService, ReplyReportService>();
            services.AddScoped<IQuoteReportService, QuoteReportService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ForumUser>, UserClaimsPrincipalFactory<ForumUser, IdentityRole>>();

            //Registrating the automapper
            services.AddSingleton(mapper);

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Role.Administrator,
                    authBuilder =>
                    {
                        authBuilder.RequireRole(Role.Administrator);
                    });

                options.AddPolicy(Role.Owner,
                    authBuilder =>
                    {
                        authBuilder.RequireRole(Role.Owner);
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseWebSockets(
                new WebSocketOptions
                {
                    KeepAliveInterval = TimeSpan.FromSeconds(120),
                    ReceiveBufferSize = 4 * 1024,
                });

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //Custom middlewares
            app.UseMiddleware(typeof(SeedRolesMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );
            });
        }
    }
}