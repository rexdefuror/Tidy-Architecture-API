using FluentValidation;

namespace NetLore.Application.Read.Tasks
{
    public class FindTaskByIdValidator : AbstractValidator<FindTaskByIdRequest>
    {
        public FindTaskByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
