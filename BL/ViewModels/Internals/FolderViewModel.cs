using System.Collections.Generic;

namespace BL.ViewModels.Internals
{
    public class FolderViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public List<FolderViewModel> Folders { get; set; }
        public List<ScenarioViewModel> Scenarios { get; set; }
    }
}