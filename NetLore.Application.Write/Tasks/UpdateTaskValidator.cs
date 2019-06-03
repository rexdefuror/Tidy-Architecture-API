using FluentValidation;

namespace NetLore.Application.Write.Tasks
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).DependentRules(() =>
            {
                RuleFor(x => x.Task).NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.Task.Name).NotEmpty();
                });
            });
        }
    }
}
