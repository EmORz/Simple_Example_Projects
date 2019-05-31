using AutoMapper;
using Forum.MapConfiguration;
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

namespace Forum.Web.Utilities
{
    public class ForumProfile : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = AutoMapperConfig.RegisterMappings(
                 typeof(LoginUserInputModel).Assembly,
                 typeof(EditPostInputModel).Assembly,
                 typeof(RegisterUserViewModel).Assembly,
                 typeof(CategoryInputModel).Assembly,
                 typeof(UserJsonViewModel).Assembly,
                 typeof(ForumFormInputModel).Assembly,
                 typeof(ForumInputModel).Assembly,
                 typeof(EditForumFormInputModel).Assembly,
                 typeof(EditForumInputModel).Assembly,
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
                 typeof(QuoteReportInputModel).Assembly);

            return config;
         }
    }
}