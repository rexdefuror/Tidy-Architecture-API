using FluentValidation;

namespace NetLore.Application.Write.TaskLists
{
    public class DeleteTaskListValidator : AbstractValidator<DeleteTaskListRequest>
    {
        public DeleteTaskListValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
