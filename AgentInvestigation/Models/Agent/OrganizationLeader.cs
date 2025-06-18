namespace AgentInvestigation.Models
{
    public class OrganizationLeader : Agent
    {
        private static readonly Random _random = new Random();

        //====================================
        public OrganizationLeader(string name)
            : base(name, "Organization Leader", 8)
        {
        }

        //--------------------------------------------------------------
        public override void DetachSensors()
        {
            Sensor[] sensors = GetInternalSensorArray();
            int count = GetAttachedCount();

            if (count % 10 == 0)
            {
                for (int i = 0; i < WeaknessesLen; i++)
                {
                    sensors[i] = null;
                }
            }

            if (count % 3 == 0)
            {
                List<int> attachedIndexes = new List<int>();

                for (int i = 0; i < WeaknessesLen; i++)
                {
                    if (sensors[i] != null)
                        attachedIndexes.Add(i);
                }

                int toRemove = Math.Min(2, attachedIndexes.Count);
                Random rng = new Random();

                for (int i = 0; i < toRemove; i++)
                {
                    int randIndex = rng.Next(attachedIndexes.Count);
                    int indexToRemove = attachedIndexes[randIndex];
                    sensors[indexToRemove] = null;
                    attachedIndexes.RemoveAt(randIndex);
                }
            }
        }

    }
}