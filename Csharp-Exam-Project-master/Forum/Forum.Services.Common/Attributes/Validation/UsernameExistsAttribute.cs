namespace Forum.Services.Common.Attributes.Validation
{
    using Forum.Services.Interfaces.Account;
    using Forum.Services.Interfaces.Db;
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class UsernameExistsAttribute : ValidationAttribute
    {
        private IDbService dbService;

        private IAccountService accountService;

        public UsernameExistsAttribute()
        {

        }
        
        public UsernameExistsAttribute(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.dbService = (IDbService)validationContext
                .GetService(typeof(IDbService));

            this.accountService = (IAccountService)validationContext
                .GetService(typeof(IAccountService));
            
            if (!this.accountService.UsernameExists(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Username already exists.");
            }
        }
    }
}