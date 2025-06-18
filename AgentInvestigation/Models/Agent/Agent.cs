namespace AgentInvestigation.Models
{
    public abstract class Agent
    {
        public string Name { get; }
        public string Rank { get; }
        public int WeaknessesLen { get; }

        private readonly Weakness[] _weaknesses;
        private readonly Sensor[] _attachedSensors;

        //====================================
        protected Agent(string name, string rank, int weaknessesLen)
        {
            Name = name;
            Rank = rank;
            WeaknessesLen = weaknessesLen;
            _weaknesses = new Weakness[WeaknessesLen];
            _attachedSensors = new Sensor[WeaknessesLen];
        }

        //====================================
        public void SetWeaknesses(Weakness[] weaknesses)
        {
            for (int i = 0; i < WeaknessesLen && i < weaknesses.Length; i++)
            {
                _weaknesses[i] = weaknesses[i];
            }
        }

        //--------------------------------------------------------------
        public void AttachSensorAtPosition(int position, Sensor sensor)
        {
            if (position >= 0 && position < WeaknessesLen)
                _attachedSensors[position] = sensor;
            else
                Console.WriteLine($"Invalid position: {position}. Must be between 0 and {WeaknessesLen - 1}.");
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
                    matchCount += Math.Min(kvp.Value, countInList2);
            }

            return matchCount;
        }

        //--------------------------------------------------------------
        public bool IsExposed()
        {
            return GetMatchingSensorCount() == WeaknessesLen;
        }

        //--------------------------------------------------------------
        public Sensor[] GetAttachedSensors() => _attachedSensors.ToArray();

        //--------------------------------------------------------------
        public Weakness[] GetWeaknesses() => _weaknesses.ToArray();
    }
}
