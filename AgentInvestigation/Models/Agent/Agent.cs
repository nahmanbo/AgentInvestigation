namespace AgentInvestigation.Models
{
    public abstract class Agent
    {
        public string Name { get; }
        public string Rank { get; }
        public int WeaknessesLen { get; }

        private int _attachedCount;

        private readonly Weakness[] _weaknesses;
        private readonly Sensor[] _attachedSensors;

        //====================================
        protected Agent(string name, string rank, int weaknessesLen)
        {
            Name = name;
            Rank = rank;
            WeaknessesLen = weaknessesLen;
            _attachedCount = 0;

            _weaknesses = new Weakness[WeaknessesLen];
            _attachedSensors = new Sensor[WeaknessesLen];
        }

        //====================================
        public void SetWeaknesses(Weakness[] weaknesses)
        {
            if (weaknesses.Length != WeaknessesLen)
                throw new ArgumentException($"Weaknesses array must be of length {WeaknessesLen}");

            for (int i = 0; i < WeaknessesLen; i++)
                _weaknesses[i] = weaknesses[i];
        }

        //--------------------------------------------------------------
        public void AttachSensorAtPosition(int position, Sensor sensor)
        {
            if (position < 0 || position >= WeaknessesLen)
                throw new ArgumentOutOfRangeException(nameof(position), $"Must be between 0 and {WeaknessesLen - 1}");

            _attachedSensors[position] = sensor;
            _attachedCount++;
        }

        //--------------------------------------------------------------
        public int GetAttachedCount()
        {
            return _attachedCount;
        }

        //--------------------------------------------------------------
        public bool IsExposed()
        {
            return GetMatchingSensorCount() == WeaknessesLen;
        }

        //--------------------------------------------------------------
        public int GetMatchingSensorCount()
        {
            ActivateAllSensors();

            Dictionary<Weakness, int> required = new Dictionary<Weakness, int>();
            foreach (Weakness w in _weaknesses)
            {
                if (required.ContainsKey(w))
                    required[w]++;
                else
                    required[w] = 1;
            }

            Dictionary<Weakness, int> attached = new Dictionary<Weakness, int>();
            foreach (Sensor sensor in _attachedSensors)
            {
                if (attached.ContainsKey(sensor.Type))
                    attached[sensor.Type]++;
                else
                    attached[sensor.Type] = 1;
            }

            int matchCount = 0;
            foreach (var kvp in required)
            {
                if (attached.TryGetValue(kvp.Key, out int attachedCount))
                    matchCount += Math.Min(kvp.Value, attachedCount);
            }

            return matchCount;
        }

        //--------------------------------------------------------------
        private void ActivateAllSensors()
        {
            foreach (Sensor sensor in _attachedSensors)
                sensor.Activate();
        }

        //--------------------------------------------------------------
        protected Sensor[] GetInternalSensorArray()
        {
            return _attachedSensors;
        }

        //--------------------------------------------------------------
        public virtual void DetachSensors()
        {
        }
    }
}
