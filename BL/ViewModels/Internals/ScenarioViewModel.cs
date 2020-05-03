using System;
using DAL.Enums;

namespace BL.ViewModels.Internals
{
    public class ScenarioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastExecutedDate { get; set; }
        public ExecutionStatus LastExecutedStatus { get; set; }
        public TimeSpan LastExecutionTime { get; set; }
    }
}