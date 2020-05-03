namespace DAL.Models
{
    public class Folder : ScenarioModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual Folder ParentFolder { get; set; }
    }
}