namespace AgentInvestigation.Models;

public class InvestigationManager
{
    private readonly List<Agent> _agents;
    private readonly List<Weakness> _sensorOptions;

    //====================================
    public InvestigationManager()
    {
        _agents = new List<Agent>
        {
            new FootSoldier("Iranian Agent #1"),
            new FootSoldier("Iranian Agent #2"),
            new SquadLeader("Iranian Agent #3")
        };

        _sensorOptions = Enum.GetValues<Weakness>().ToList();
    }

    //--------------------------------------------------------------
    public void Start()
    {
        foreach (var agent in _agents)
        {
            Console.WriteLine($"Starting investigation on {agent.Name}");
            InvestigateAgent(agent);
            Console.WriteLine($"{agent.Name} exposed!\n");
        }

        Console.WriteLine("Game over! All Agents ");
    }

    //--------------------------------------------------------------
    private void InvestigateAgent(Agent agent)
    {
        Console.WriteLine($"The agent's rank is {agent.Rank}, and he has {agent.MaxWeaknesses} weaknesses.");

        while (!agent.IsExposed())
        {
            int position = GetSensorPosition(agent);
            Sensor sensor = ChooseSensor();
            agent.AttachSensorAtPosition(position, sensor);

            sensor.Activate();

            int correct = agent.GetMatchingSensorCount();
            Console.WriteLine($"Result: {correct}/{agent.MaxWeaknesses} correct.");

            agent.PrintAgentInfo();
        }
    }

    //--------------------------------------------------------------
    private int GetSensorPosition(Agent agent)
    {
        while (true)
        {
            Console.WriteLine($"\nChoose a position to attach the sensor (0 to {agent.MaxWeaknesses - 1}):");
            if (int.TryParse(Console.ReadLine(), out int position) &&
                position >= 0 && position < agent.MaxWeaknesses)
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

            switch (selected)
            {
                case Weakness.Thermal:
                    return new ThermalSensor();
                case Weakness.Visual:
                    return new VisualSensor();
                case Weakness.Acoustic:
                    return new AcousticSensor();
                case Weakness.Radar:
                    return new RadarSensor();
            }
        }

        Console.WriteLine("Invalid sensor type.");
        return ChooseSensor(); 
    }
}
