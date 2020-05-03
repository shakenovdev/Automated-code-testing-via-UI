using System;
using BL.ViewModels.Internals;
using ActionEntity = DAL.Models.Action;

namespace BL.Mappers
{
    internal static class ActionMapper
    {
        internal static ActionViewModel ToActionViewModel(ActionEntity entity)
        {
            return new ActionViewModel
            {
                Id = entity.Id,
                Type = entity.Type,
                Order = entity.Order,
                Variable = VariableMapper.ToVariableViewModel(entity.Variable),
                Method = MethodMapper.ToMethodViewModel(entity.Method),
                Assert = AssertMapper.ToAssertViewModel(entity.Assert)
            };
        }

        internal static ActionEntity ToAction(ActionViewModel model, int scenarioId)
        {
            return new ActionEntity
            {
                Id = model.Id,
                ScenarioId = scenarioId,
                VariableId = model.Variable?.Id,
                MethodId = model.Method?.Id,
                AssertId = model.Assert?.Id,
                Type = model.Type,
                Order = model.Order,
                Variable = VariableMapper.ToVariable(model.Variable),
                Method = MethodMapper.ToMethod(model.Method),
                Assert = AssertMapper.ToAssert(model.Assert)
            };
        }
    }
}