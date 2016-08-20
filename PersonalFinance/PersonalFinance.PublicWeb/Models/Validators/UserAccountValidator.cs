using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.Ajax.Utilities;

namespace PersonalFinance.PublicWeb.Models.Validators
{
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        public UserAccountValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password required");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Password confirmation required");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}