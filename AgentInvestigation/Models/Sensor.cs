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
    public void Activate()
    {
        _activationCount++;
    }

    public static int GetTotalActivations() => _activationCount;
}

public class ThermalSensor : Sensor
{
    public ThermalSensor() : base(Weakness.Thermal) { }
}

public class VisualSensor : Sensor
{
    public VisualSensor() : base(Weakness.Visual) { }
}

public class AcousticSensor : Sensor
{
    public AcousticSensor() : base(Weakness.Acoustic) { }
}

public class RadarSensor : Sensor
{
    public RadarSensor() : base(Weakness.Radar) { }
}
