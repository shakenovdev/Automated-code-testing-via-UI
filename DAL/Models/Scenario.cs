using System;
using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Models
{
    public class Scenario : ScenarioModel
    {
        public int? FolderId { get; set; }
        public string Name { get; set; }
        public DateTime LastExecutedDate { get; set; }
        public ExecutionStatus LastExecutedStatus { get; set; }
        public TimeSpan LastExecutionTime { get; set; }

        public virtual List<Action> Actions { get; set; }
        public virtual Folder Folder { get; set; }
    }
}