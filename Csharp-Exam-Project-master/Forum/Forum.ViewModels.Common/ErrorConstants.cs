namespace Forum.ViewModels.Common
{
    public class ErrorConstants
    {
        public const int MinimumDescriptionLength = 5;

        public const int MaximumNamesLength = 20;

        public const int MinimumNamesLength = 5;

        public const int MaximumForumDescriptionLength = 150;

        public const int MinimumForumDescriptionLength = 5;

        public const int MaximumPostDescriptionLength = 500;

        public const int MinimumPostDescriptionLength = 5;

        public const int MaximumPasswordsLength = 20;

        public const int MinimumPasswordsLength = 5;

        public const int MaximumLocationLength = 20;

        public const int MinimumLocationLength = 5;

        public const string IncorrectUsernameOrPasswordError = "Incorrect username or password";

        public const string InvalidLoginAttempt = "Invalid login attempt.";

        public const string RequiredError = "You must enter a/an {0}.";

        public const string StringLengthErrorMessage = "{0} must be between {1} and {2} characters long.";

        public const string AlreadyExistsError = "{0} already exists.";

        public const string PasswordDontMatch = "Passwords do not match.";

        public static string AllowedImgExtensionsError = "Allowed image extansions are " + string.Join(", ", ModelsConstants.AllowedImageExtensions);

        public const string MustEnterValidValueError = "You must enter a valid {0} value.";

        public const string NamesAllowedCharactersError = "{0} is allowed to contain only lowercase/uppercase characters, digits and '_', '-'";

        public const string DescriptionsAllowedCharactersError = "{0} is allowed to contain only lowercase/uppercase characters, digits and '_', '-', '(', ')', '&', '.', '/', '?', '!'";

        public const string MinimumLengthError = "{0} must be atleast {1} characters long";

        public const string InvalidPostIdError = "Error. Invalid post id.";

        public const string InvalidQuoteIdError = "Error. Invalid quote id.";

        public const string InvalidReplyIdError = "Error. Invalid reply id.";

        public const string InvalidForumIdError = "Error. Invalid forum id.";

        public const string InvalidPostReportIdError = "Error. Invalid post report id.";

        public const string InvalidReplyReportIdError = "Error. Invalid reply report id.";

        public const string InvalidQuoteReportIdError = "Error. Invalid quote report id.";

        public const string InvalidCategory = "Error. Invalid category.";

        public const string IncorrectPasswordError = "Error. Incorrect password";

        public const string UserNotFoundError = "Error. User not found.";

        public const string MustChooseAnImage = "Error. You must choose an image.";

        public const string CategoryExistsError = "Error. Category already exists";

        public const string PostExistsError = "Error. Post already exists";

        public const string ForumExistsError = "Error. Forum already exists";
    }
}