using FluentValidation;

namespace NetLore.Application.Write.TaskLists
{
    public class UpdateTaskListValidator : AbstractValidator<UpdateTaskListRequest>
    {
        public UpdateTaskListValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).DependentRules(() =>
            {
                RuleFor(x => x.TaskList).NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.TaskList.Name).NotEmpty();
                });
            });
        }
    }
}
