namespace AgentInvestigation.Models;

public class SquadLeader : Agent
{
    private static readonly Random _random = new Random();

    //====================================
    public SquadLeader(string name)
        : base(name, "SquadLeader", 4)
    {
    }
    //--------------------------------------------------------------
    public override void DetachSensors()
    {
        if (GetAttachedCount() % 3 != 0)
            return;
        Sensor[] sensors = GetInternalSensorArray();
        List<int> attachedIndexes = new List<int>();

        for (int i = 0; i < WeaknessesLen; i++)
        {
            if (sensors[i] != null)
                attachedIndexes.Add(i);
        }

        int toRemove = Math.Min(1, attachedIndexes.Count);

        for (int i = 0; i < toRemove; i++)
        {
            int randIndex = _random.Next(attachedIndexes.Count);
            int indexToRemove = attachedIndexes[randIndex];
            sensors[indexToRemove] = null;
            attachedIndexes.RemoveAt(randIndex);
        }
    }
}