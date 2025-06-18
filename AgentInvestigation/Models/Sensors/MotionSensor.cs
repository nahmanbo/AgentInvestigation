namespace AgentInvestigation.Models
{
    public class MotionSensor : Sensor
    {
        private int _activationCount = 0;
        private const int MaxActivations = 3;

        public MotionSensor() : base(Weakness.Motion)
        {
        }

        //--------------------------------------------------------------
        public override void Activate()
        {
            if (IsUsable)
            {
                base.Activate();
                _activationCount++;
            }
        }

        //--------------------------------------------------------------
        public override bool IsUsable => _activationCount < MaxActivations;
    }
}