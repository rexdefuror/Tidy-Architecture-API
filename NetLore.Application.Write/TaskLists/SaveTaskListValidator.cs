using FluentValidation;

namespace NetLore.Application.Write.TaskLists
{
    public class SaveTaskListValidator : AbstractValidator<SaveTaskListRequest>
    {
        public SaveTaskListValidator()
        {
            RuleFor(x => x.TaskList).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.TaskList.Name).NotEmpty();
            });
        }
    }
}
