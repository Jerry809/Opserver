using System.Collections.Generic;
using StackExchange.Opserver.Data.Dashboard;

namespace StackExchange.Opserver.Views.Dashboard
{
    public class DashboardModel
    {
        public string Filter { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<Node> Nodes { get; set; }
        public bool IsStartingUp { get; set; }
        public List<Data.PerformanceCounter.Node> PerformanceCounterNodes { get; set; }
    }
}
