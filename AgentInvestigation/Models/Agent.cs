using AgentInvestigation.Models;

//--------------------------------------------------------------
public abstract class Agent
{
    public string Name { get; }
    public string Rank { get; }
    public int MaxWeaknesses { get; }

    private static readonly Random _random = new Random();


    //====================================
    protected Agent(string name, string rank, int maxWeaknesses)
    {
        Name = name;
        Rank = rank;
        MaxWeaknesses = maxWeaknesses;
    }

    //--------------------------------------------------------------
    public List<string> GetRandomWeaknessNames()
    {
        Array allWeaknesses = Enum.GetValues(typeof(Weakness));
        List<string> randomWeaknesses = new List<string>();

        for (int i = 0; i < MaxWeaknesses; i++)
        {
            int index = _random.Next(allWeaknesses.Length);
            Weakness selected = (Weakness)allWeaknesses.GetValue(index);
            randomWeaknesses.Add(selected.ToString());
        }

        return randomWeaknesses;
    }
}