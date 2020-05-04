using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Folder : ScenarioModel
    {
        [Required]
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }

        public virtual Folder ParentFolder { get; set; }
        public virtual ICollection<Folder> Children { get; set; }
    }
}