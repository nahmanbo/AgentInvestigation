namespace AgentInvestigation.Models;

using AgentInvestigation.Models;

public class InvestigationManager
{
    private readonly Agent _agent;
    private readonly List<Weakness> _sensorOptions;

    public InvestigationManager()
    {
        _agent = new FootSoldier("Iranian Agent");
        _sensorOptions = Enum.GetValues(typeof(Weakness)).Cast<Weakness>().ToList();
    }

    //--------------------------------------------------------------
    public void Start()
    {
        Console.WriteLine($"The agent's rank is {_agent.Rank}, and he has {_agent.MaxWeaknesses} weaknesses.");

        while (!_agent.IsExposed())
        {
            Console.WriteLine($"\nChoose a position to attach the sensor (0 to {0}):", _agent.MaxWeaknesses - 1);
            if (!int.TryParse(Console.ReadLine(), out int position) || position < 0 || position >= _agent.MaxWeaknesses)
            {
                Console.WriteLine("Invalid position.");
                continue;
            }

            Console.WriteLine("Choose a sensor type:");
            for (int i = 0; i < _sensorOptions.Count; i++)
                Console.WriteLine($"{i + 1}. {_sensorOptions[i]}");

            if (!int.TryParse(Console.ReadLine(), out int sensorIndex) || sensorIndex < 1 || sensorIndex > _sensorOptions.Count)
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            Weakness selectedType = _sensorOptions[sensorIndex - 1];
            Sensor sensor = new Sensor(selectedType);
            _agent.AttachSensorAtPosition(position, sensor);

            int correct = _agent.GetMatchingSensorCount();
            Console.WriteLine($"Result: {correct}/{_agent.MaxWeaknesses} correct.");
            
        }

        Console.WriteLine("\n✅ Agent exposed successfully!");
    }
}
