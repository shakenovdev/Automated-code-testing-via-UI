using BL.ViewModels.Internals;
using DAL.Models;

namespace BL.Mappers
{
    internal static class AssertMapper
    {
        public static AssertViewModel ToAssertViewModel(Assert entity)
        {
            if (entity == null)
                return null;

            return new AssertViewModel
            {
                Id = entity.Id,
                Type = entity.Type,
                ValueVariable = VariableMapper.ToVariableViewModel(entity.ValueVariable),
                ExpectedVariable = VariableMapper.ToVariableViewModel(entity.ExpectedVariable),
                DeltaVariable = VariableMapper.ToVariableViewModel(entity.DeltaVariable)
            };
        }

        public static Assert ToAssert(AssertViewModel model)
        {
            if (model == null)
                return null;

            return new Assert
            {
                Id = model.Id,
                Type = model.Type,
                ValueVariableId = model.ValueVariable.Id,
                ExpectedVariableId = model.ExpectedVariable?.Id,
                DeltaVariableId = model.DeltaVariable?.Id,
                ValueVariable = VariableMapper.ToVariable(model.ValueVariable),
                ExpectedVariable = VariableMapper.ToVariable(model.ExpectedVariable),
                DeltaVariable = VariableMapper.ToVariable(model.DeltaVariable),
            };
        }
    }
}