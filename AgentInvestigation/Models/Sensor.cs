namespace AgentInvestigation.Models;

public class Sensor
{
    public Weakness Type { get; }

    private static int _activationCount = 0;

    public Sensor(Weakness type)
    {
        Type = type;
    }

    //--------------------------------------------------------------
    public virtual bool Activate(List<Weakness> weaknesses)
    {
        _activationCount++;
        return weaknesses.Contains(Type);
    }

    public static int GetTotalActivations() => _activationCount;
}