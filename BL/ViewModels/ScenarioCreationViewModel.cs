using System.Collections.Generic;
using BL.ViewModels.Internals;

namespace BL.ViewModels
{
    public class ScenarioCreationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ActionViewModel> Actions { get; set; }
    }
}