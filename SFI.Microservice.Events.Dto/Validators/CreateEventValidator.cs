using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SFI.Microservice.Events.Dto.Validators
{
    public class CreateEventValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
