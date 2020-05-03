using BL.ViewModels.Internals;
using DAL.Models;

namespace BL.Mappers
{
    internal static class VariableMapper
    {
        public static VariableViewModel ToVariableViewModel(Variable entity)
        {
            if (entity == null)
                return null;

            return new VariableViewModel
            {
                Id = entity.Id,
                Type = entity.Type,
                Name = entity.Name,
                ConstantValue = entity.ConstantValue,
                ParentVariable = ToVariableViewModel(entity.ParentVariable)
            };
        }

        public static Variable ToVariable(VariableViewModel model)
        {
            if (model == null)
                return null;

            return new Variable
            {
                Id = model.Id,
                ParentId = model.ParentVariable?.Id,
                Type = model.Type,
                Name = model.Name,
                ConstantValue = model.ConstantValue,
                ParentVariable = ToVariable(model.ParentVariable)
            };
        }
    }
}