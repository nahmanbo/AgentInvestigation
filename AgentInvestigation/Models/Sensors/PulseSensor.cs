namespace AgentInvestigation.Models
{
    public class PulseSensor : Sensor
    {
        private int _activationCount = 0;
        private const int MaxActivations = 3;

        public bool IsBroken => _activationCount >= MaxActivations;

        public PulseSensor() : base(Weakness.Pulse) { }

        //--------------------------------------------------------------
        public override void Activate()
        {
            if (!IsBroken)
            {
                base.Activate();
                _activationCount++;
            }
        }

        //--------------------------------------------------------------
        public override bool IsUsable => !IsBroken;
    }
}