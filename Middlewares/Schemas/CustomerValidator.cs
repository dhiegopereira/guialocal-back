using guialocal.Models;
using FluentValidation;

namespace guialocal.Middlewares.Schemas
{
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Active).NotNull();
        }
    }
}
