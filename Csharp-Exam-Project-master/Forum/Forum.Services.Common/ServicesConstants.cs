namespace Forum.Services.Common
{
    public class ServicesConstants
    {
        public static string[] AllowedTags = new string[] { "h1", "h2", "h3", "i", "b", "center", "ol", "ul", "li" };

        public const string MessagesSeparator = " - ";

        public const string ParseTagsRegex = @"(\[(\w+)\])(.*?)(\[\/\2\])";

        public const string ReplyingTo = "<center><h2>Replying to {0}</h2></center>";

        public const string CloudinaryPublicId = "{0}_profile_pic";

        public const string CloudinaryPictureName = "{0}_profile_pic";

    }
}