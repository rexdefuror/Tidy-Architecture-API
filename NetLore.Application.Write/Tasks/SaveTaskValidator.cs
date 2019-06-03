using FluentValidation;

namespace NetLore.Application.Write.Tasks
{
    public class SaveTaskValidator : AbstractValidator<SaveTaskRequest>
    {
        public SaveTaskValidator()
        {
            RuleFor(x => x.Task).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Task.Name).NotEmpty();
            });
        }
    }
}
