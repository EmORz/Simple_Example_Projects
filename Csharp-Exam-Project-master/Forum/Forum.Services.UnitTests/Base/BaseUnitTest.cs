using AutoMapper;
using Forum.Data;
using Forum.MapConfiguration;
using Forum.Models;
using Forum.Services.Account;
using Forum.Services.Category;
using Forum.Services.Chat;
using Forum.Services.Common;
using Forum.Services.Db;
using Forum.Services.Forum;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Chat;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Forum;
using Forum.Services.Interfaces.Message;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Profile;
using Forum.Services.Interfaces.Quote;
using Forum.Services.Interfaces.Reply;
using Forum.Services.Interfaces.Report;
using Forum.Services.Interfaces.Report.Post;
using Forum.Services.Interfaces.Report.Quote;
using Forum.Services.Interfaces.Report.Reply;
using Forum.Services.Interfaces.Role;
using Forum.Services.Interfaces.Settings;
using Forum.Services.Message;
using Forum.Services.Pagging;
using Forum.Services.Post;
using Forum.Services.Profile;
using Forum.Services.Quote;
using Forum.Services.Reply;
using Forum.Services.Report;
using Forum.Services.Report.Post;
using Forum.Services.Report.Quote;
using Forum.Services.Report.Reply;
using Forum.Services.Role;
using Forum.Services.Settings;
using Forum.ViewModels.Account;
using Forum.ViewModels.Category;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Message;
using Forum.ViewModels.Post;
using Forum.ViewModels.Profile;
using Forum.ViewModels.Quote;
using Forum.ViewModels.Reply;
using Forum.ViewModels.Report;
using Forum.ViewModels.Role;
using Forum.ViewModels.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forum.Services.UnitTests.Base
{
    public class BaseUnitTest
    {
        private readonly IMapper mapper;

        private readonly DbContextOptionsBuilder<ForumDbContext> options;

        public BaseUnitTest()
        {
            this.options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: TestsConstants.InMemoryDbName);

            this.mapper = AutoMapperConfig.RegisterMappings(
                typeof(LoginUserInputModel).Assembly,
                typeof(EditPostInputModel).Assembly,
                typeof(RegisterUserViewModel).Assembly,
                typeof(CategoryInputModel).Assembly,
                typeof(UserJsonViewModel).Assembly,
                typeof(ForumFormInputModel).Assembly,
                typeof(ForumInputModel).Assembly,
                typeof(RecentConversationViewModel).Assembly,
                typeof(ForumPostsInputModel).Assembly,
                typeof(PostInputModel).Assembly,
                typeof(LatestPostViewModel).Assembly,
                typeof(ProfileInfoViewModel).Assembly,
                typeof(PopularPostViewModel).Assembly,
                typeof(ReplyInputModel).Assembly,
                typeof(PostViewModel).Assembly,
                typeof(ReplyViewModel).Assembly,
                typeof(EditProfileInputModel).Assembly,
                typeof(SendMessageInputModel).Assembly,
                typeof(QuoteInputModel).Assembly,
                typeof(PostReportInputModel).Assembly,
                typeof(ReplyReportInputModel).Assembly,
                typeof(UserRoleViewModel).Assembly,
                typeof(ChatMessageViewModel).Assembly,
                typeof(QuoteReportInputModel).Assembly)
                .CreateMapper();

            this.ServiceCollection = new ServiceCollection();
            this.ConfigureServices();

            this.Provider = ServiceCollection.BuildServiceProvider();
        }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider Provider { get; private set; }

        //Dependency injecting all needed services.
        //And instancing the InMemory Database.
        private void ConfigureServices()
        {
            this.ServiceCollection
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

            //Creating the InMemoryDatabase
            this.ServiceCollection
                .AddDbContext<ForumDbContext>(options => options.UseInMemoryDatabase(databaseName: TestsConstants.InMemoryDbName));

            //Injecting services
            this.ServiceCollection.AddScoped<ICategoryService, CategoryService>();
            this.ServiceCollection.AddScoped<IAccountService, AccountService>();
            this.ServiceCollection.AddScoped<IPaggingService, PaggingService>();
            this.ServiceCollection.AddScoped<IDbService, DbService>();
            this.ServiceCollection.AddScoped<IForumService, ForumService>();
            this.ServiceCollection.AddScoped<IPostService, PostService>();
            this.ServiceCollection.AddScoped<IRoleService, RoleService>();
            this.ServiceCollection.AddScoped<IReportService, ReportService>();
            this.ServiceCollection.AddScoped<IReplyService, ReplyService>();
            this.ServiceCollection.AddScoped<IProfileService, ProfileService>();
            this.ServiceCollection.AddScoped<IMessageService, MessageService>();
            this.ServiceCollection.AddScoped<IQuoteService, QuoteService>();
            this.ServiceCollection.AddScoped<IPostReportService, PostReportService>();
            this.ServiceCollection.AddScoped<IReplyReportService, ReplyReportService>();
            this.ServiceCollection.AddScoped<IQuoteReportService, QuoteReportService>();
            this.ServiceCollection.AddScoped<ISettingsService, SettingsService>();
            this.ServiceCollection.AddScoped<IChatService, ChatService>();
            this.ServiceCollection.AddScoped<IUserClaimsPrincipalFactory<ForumUser>, UserClaimsPrincipalFactory<ForumUser, IdentityRole>>();

            //Injecting the AutoMapper instance
            this.ServiceCollection.AddSingleton(mapper);
        }
    }
}