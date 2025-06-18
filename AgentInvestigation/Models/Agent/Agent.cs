// Agent.cs
using AgentInvestigation.Models;
using System;
using System.Linq;

public abstract class Agent
{
    public string Name { get; }
    public string Rank { get; }
    public int MaxWeaknesses { get; }

    private readonly Weakness[] _weaknesses;
    private readonly Sensor[] _attachedSensors;
    private static readonly Random _random = new Random();
    private static readonly Weakness[] AllWeaknesses = (Weakness[])Enum.GetValues(typeof(Weakness));

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
        return Enumerable.Range(0, count)
            .Select(_ => AllWeaknesses[_random.Next(AllWeaknesses.Length)])
            .ToArray();
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
    public void ActivateAllSensors()
    {
        foreach (var sensor in _attachedSensors)
            sensor?.Activate();
    }

    //--------------------------------------------------------------
    public int GetMatchingSensorCount()
    {
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
    public Sensor[] GetAttachedSensors() => _attachedSensors.ToArray();

    //--------------------------------------------------------------
    public Weakness[] GetWeaknesses() => _weaknesses.ToArray();
} 

// AgentPrinter.cs


namespace AgentInvestigation.Models
{
    public static class AgentPrinter
    {
        public static void PrintAgentInfo(Agent agent)
        {
            Console.WriteLine($"\nAgent: {agent.Name}, Rank: {agent.Rank}, Weaknesses: {agent.MaxWeaknesses}");

            Console.Write("Weaknesses: ");
            foreach (var w in agent.GetWeaknesses())
                Console.Write($"{w} ");

            Console.Write("\nSensors:    ");
            foreach (var s in agent.GetAttachedSensors())
                Console.Write($"{(s?.Type.ToString() ?? "None")} ");

            Console.WriteLine();
        }
    }
}
