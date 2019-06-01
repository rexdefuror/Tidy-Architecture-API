using FluentValidation;

namespace NetLore.Application.Read.TaskLists
{
    public class FindTaskListByIdValidator : AbstractValidator<FindTaskListByIdRequest>
    {
        public FindTaskListByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
