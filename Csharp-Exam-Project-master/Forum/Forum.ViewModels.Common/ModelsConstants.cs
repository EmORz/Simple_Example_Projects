namespace Forum.ViewModels.Common
{
    public class ModelsConstants
    {
        public static string[] AllowedImageExtensions = new string[] { ".jpg", ".png", ".jpeg", ".bmp" };

        public const string NamesRegex = @"^[a-zA-Z_\-0-9]*$";

        public const string DescriptionsRegex = @"^[a-zA-Z _\/\-0-9!.?()&]*$";
    }
}