using AgentInvestigation.Models;

namespace AgentInvestigation;

class Program
{
    static void Main(string[] args)
    {
        InvestigationManager manager = new InvestigationManager();
        manager.Start();
    }
}
