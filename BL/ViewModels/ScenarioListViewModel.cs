using System.Collections.Generic;
using BL.ViewModels.Internals;

namespace BL.ViewModels
{
    public class ScenarioListViewModel
    {
        public List<FolderViewModel> Folders { get; set; }
        public List<ScenarioViewModel> RootScenarios { get; set; }
        public List<ScenarioViewModel> TrashScenarios { get; set; }
    }
}