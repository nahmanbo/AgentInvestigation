using AgentInvestigation.Models;

public abstract class Agent
{
    public string Name { get; }
    public string Rank { get; }
    public int MaxWeaknesses { get; }
    private readonly Weakness[] _weaknesses;
    private readonly Sensor[] _attachedSensors;

    private readonly Random _random = new Random();

    //====================================
    protected Agent(string name, string rank, int maxWeaknesses)
    {
        Name = name;
        Rank = rank;
        MaxWeaknesses = maxWeaknesses;
        _weaknesses = GenerateRandomWeaknesses(maxWeaknesses);
        _attachedSensors = new Sensor[MaxWeaknesses];
    }

    //--------------------------------------------------------------
    private Weakness[] GenerateRandomWeaknesses(int count)
    {
        Array allWeaknesses = Enum.GetValues(typeof(Weakness));
        Weakness[] generated = new Weakness[count];

        for (int i = 0; i < count; i++)
        {
            int index = _random.Next(allWeaknesses.Length);
            generated[i] = (Weakness)allWeaknesses.GetValue(index);
        }

        return generated;
    }

    //--------------------------------------------------------------
    public void AttachSensorAtPosition(int position, Sensor sensor)
    {
        if (position >= 0 && position < MaxWeaknesses)
            _attachedSensors[position] = sensor;
        else
            Console.WriteLine($"Invalid position: {position}. Must be between 0 and {MaxWeaknesses - 1}.");
    }

//--------------------------------------------------------------
    public int GetMatchingSensorCount()
    {
        foreach (var sensor in _attachedSensors)
            sensor?.Activate();

        var grouped1 = _weaknesses
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

        var grouped2 = _attachedSensors
            .Where(s => s != null)
            .GroupBy(s => s.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        int matchCount = 0;

        foreach (var kvp in grouped1)
        {
            if (grouped2.TryGetValue(kvp.Key, out int countInList2))
            {
                matchCount += Math.Min(kvp.Value, countInList2);
            }
        }

        return matchCount;
    }


    //--------------------------------------------------------------
    public bool IsExposed()
    {
        return GetMatchingSensorCount() == MaxWeaknesses;
    }
    
    //--------------------------------------------------------------
    public void PrintAgentInfo()
    {
        Console.WriteLine($"\nAgent: {Name}, Rank: {Rank}, Weaknesses: {MaxWeaknesses}");

        Console.Write("Weaknesses: ");
        for (int i = 0; i < MaxWeaknesses; i++)
            Console.Write($"{_weaknesses[i]} ");

        Console.Write("\nSensors:    ");
        for (int i = 0; i < MaxWeaknesses; i++)
            Console.Write($"{(_attachedSensors[i]?.Type.ToString() ?? "None")} ");

        Console.WriteLine();
    }
}
