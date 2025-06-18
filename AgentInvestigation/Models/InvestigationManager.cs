using Models.DBManager;

namespace AgentInvestigation.Models;

public class InvestigationManager
{
    private readonly DabManager _db;
    private readonly List<Weakness> _sensorOptions;
    private int _currentAgentId = 1;

    //====================================
    public InvestigationManager()
    {
        _db = new DabManager("AgentInvestigation"); 
        _sensorOptions = Enum.GetValues<Weakness>().ToList();
    }

    //--------------------------------------------------------------
    public void Start()
    {
        while (true)
        {
            try
            {
                var agent = IranianAgentFactory.CreateAgentById(_currentAgentId, _db);
                Console.WriteLine($"Starting investigation on {agent.Name}");
                InvestigateAgent(agent);
                Console.WriteLine($"{agent.Name} exposed!\n");

                _currentAgentId++; // עבור לסוכן הבא
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Finished. {ex.Message}");
                break;
            }
        }

        Console.WriteLine("Game over! All agents investigated.");
        _db.Close();
    }

    //--------------------------------------------------------------
    private void InvestigateAgent(Agent agent)
    {
        Console.WriteLine($"The agent's rank is {agent.Rank}, and he has {agent.WeaknessesLen} weaknesses.");

        while (!agent.IsExposed())
        {
            int position = GetSensorPosition(agent);
            Sensor sensor = ChooseSensor();
            agent.AttachSensorAtPosition(position, sensor);

            int correct = agent.GetMatchingSensorCount();
            Console.WriteLine($"Result: {correct}/{agent.WeaknessesLen} correct.");
        }
    }

    //--------------------------------------------------------------
    private int GetSensorPosition(Agent agent)
    {
        while (true)
        {
            Console.WriteLine($"\nChoose a position to attach the sensor (0 to {agent.WeaknessesLen - 1}):");
            if (int.TryParse(Console.ReadLine(), out int position) &&
                position >= 0 && position < agent.WeaknessesLen)
                return position;

            Console.WriteLine("Invalid position.");
        }
    }

    //--------------------------------------------------------------
    private Sensor ChooseSensor()
    {
        Console.WriteLine("Choose a sensor type:");
        for (int i = 0; i < _sensorOptions.Count; i++)
            Console.WriteLine($"{i + 1}. {_sensorOptions[i]}");

        Console.Write("Your choice: ");
        if (int.TryParse(Console.ReadLine(), out int index) &&
            index >= 1 && index <= _sensorOptions.Count)
        {
            Weakness selected = _sensorOptions[index - 1];
            return selected switch
            {
                Weakness.Thermal => new ThermalSensor(),
                Weakness.Visual => new VisualSensor(),
                Weakness.Acoustic => new AcousticSensor(),
                Weakness.Radar => new RadarSensor(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine("Invalid sensor type.");
        return ChooseSensor();
    }
}
