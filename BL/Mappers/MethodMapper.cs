using System.Linq;
using BL.ViewModels.Internals;
using DAL.Models;

namespace BL.Mappers
{
    internal static class MethodMapper
    {
        public static MethodViewModel ToMethodViewModel(Method entity)
        {
            if (entity == null)
                return null;

            return new MethodViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsStatic = entity.IsStatic,
                IsConstructor = entity.IsConstructor,
                Variable = VariableMapper.ToVariableViewModel(entity.Variable),
                Arguments = entity.Arguments
                    .Select(ArgumentMapper.ToArgumentViewModel)
                    .ToList()
            };
        }

        public static Method ToMethod(MethodViewModel model)
        {
            if (model == null)
                return null;

            return new Method
            {
                Id = model.Id,
                VariableId = model.Variable?.Id,
                IsStatic = model.IsStatic,
                IsConstructor = model.IsConstructor,
                Name = model.Name,
                Variable = VariableMapper.ToVariable(model.Variable),
                Arguments = model.Arguments
                    .Select(x => ArgumentMapper.ToArgument(x, model.Id))
                    .ToList()
            };
        }
    }
}