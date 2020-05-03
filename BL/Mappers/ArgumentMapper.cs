using BL.ViewModels.Internals;
using DAL.Models;

namespace BL.Mappers
{
    internal static class ArgumentMapper
    {
        public static ArgumentViewModel ToArgumentViewModel(Argument entity)
        {
            return new ArgumentViewModel
            {
                Id = entity.Id,
                Order = entity.Order,
                Variable = VariableMapper.ToVariableViewModel(entity.Variable)
            };
        }

        public static Argument ToArgument(ArgumentViewModel model, int methodId)
        {
            return new Argument
            {
                Id = model.Id,
                MethodId = methodId,
                VariableId = model.Variable.Id,
                Order = model.Order,
                Variable = VariableMapper.ToVariable(model.Variable)
            };
        }
    }
}