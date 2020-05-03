using System;

namespace DAL.Models
{
    public abstract class ScenarioModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}