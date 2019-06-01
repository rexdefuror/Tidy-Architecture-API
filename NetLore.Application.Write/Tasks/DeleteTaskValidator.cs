using FluentValidation;

namespace NetLore.Application.Write.Tasks
{
    public class DeleteTaskValidator : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteTaskValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
