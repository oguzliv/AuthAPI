using Auth.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validator
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(user => user.Password).
                NotEmpty().
                Matches("^(?=.*?[a-z])(?=.*?[0-9]).{6,}$").
                WithMessage("Password must contain one lower character and one numeric character and has at least 6 characters");
            RuleFor(user => user.Email).NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            RuleFor(user => user.Name).NotEmpty().MinimumLength(4);
            RuleFor(user => user.IsAdmin).NotNull();
        }
    }
}
