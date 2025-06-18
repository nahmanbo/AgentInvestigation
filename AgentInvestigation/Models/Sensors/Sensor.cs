namespace AgentInvestigation.Models
{
    public class Sensor
    {
        public Weakness Type { get; }

        private static int _activationCount = 0;

        public Sensor(Weakness type)
        {
            Type = type;
        }

        //--------------------------------------------------------------
        public virtual void Activate()
        {
            _activationCount++;
        }

        //--------------------------------------------------------------
        public static int GetTotalActivations() => _activationCount;

        //--------------------------------------------------------------
        public virtual bool IsUsable => true;
    }
}
