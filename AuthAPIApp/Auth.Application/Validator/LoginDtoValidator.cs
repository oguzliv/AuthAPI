using Auth.Application.Dto.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validator
{
    public class LoginDtoValidator: AbstractValidator<LoginModel>
    { 
        public LoginDtoValidator()
        {
            RuleFor(user => user.Password).
                NotEmpty().
                Matches("^(?=.*?[a-z])(?=.*?[0-9]).{6,}$").
                WithMessage("Password must contain one lower character and one numeric character and has at least 6 characters");
            RuleFor(user => user.Email).NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            RuleFor(user => user.IsAdmin).NotNull();
        }
    }
}
