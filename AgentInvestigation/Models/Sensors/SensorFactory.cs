using System;
using System.Collections.Generic;

namespace AgentInvestigation.Models
{
    public static class SensorFactory
    {
        private static readonly Dictionary<Weakness, Func<Sensor>> _sensorFactories = new()
        {
            { Weakness.Thermal, () => new ThermalSensor() },
            { Weakness.Motion, () => new MotionSensor() },
            { Weakness.Audio, () => new AudioSensor() },
            { Weakness.Pulse, () => new PulseSensor() },
        };

        public static Sensor CreateSensor(Weakness type)
        {
            if (_sensorFactories.TryGetValue(type, out var factory))
                return factory();

            throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported sensor type: {type}");
        }
    }
}