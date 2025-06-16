using AgentInvestigation.Models;


public abstract class Agent
{
    public string Name { get; }
    public string Rank { get; }
    public int MaxWeaknesses { get; }

    private readonly List<Weakness> _weaknesses;
    private List<Sensor> _attachedSensors = new List<Sensor>();

    private static readonly Random _random = new Random();

    //====================================
    protected Agent(string name, string rank, int maxWeaknesses)
    {
        Name = name;
        Rank = rank;
        MaxWeaknesses = maxWeaknesses;
        _weaknesses = GenerateRandomWeaknesses();
    }

    //--------------------------------------------------------------
    private List<Weakness> GenerateRandomWeaknesses()
    {
        Array allWeaknesses = Enum.GetValues(typeof(Weakness));
        List<Weakness> generated = new List<Weakness>();

        for (int i = 0; i < MaxWeaknesses; i++)
        {
            int index = _random.Next(allWeaknesses.Length);
            Weakness selected = (Weakness)allWeaknesses.GetValue(index);
            generated.Add(selected);
        }

        return generated;
    }

    //--------------------------------------------------------------
    public void AttachSensor(Sensor sensor)
    {
        _attachedSensors.Add(sensor);
    }

    //--------------------------------------------------------------
    public int GetMatchingSensorCount()
    {
        int matchCount = 0;
        
        return matchCount;
    }

    //--------------------------------------------------------------
    public bool IsExposed()
    {
        return GetMatchingSensorCount() == MaxWeaknesses;
    }

    //--------------------------------------------------------------
    public List<Weakness> GetWeaknesses()
    {
        return new List<Weakness>(_weaknesses);
    }

    //--------------------------------------------------------------
    public List<Sensor> GetAttachedSensors()
    {
        return new List<Sensor>(_attachedSensors);
    }
}
