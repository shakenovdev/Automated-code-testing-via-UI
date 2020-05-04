using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Scenario : ScenarioModel
    {
        public int? FolderId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Action> Actions { get; set; }
        public virtual Folder Folder { get; set; }
    }
}