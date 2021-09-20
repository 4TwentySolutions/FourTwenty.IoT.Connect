namespace FourTwenty.IoT.Connect.Constants
{
    public enum ModuleType
    {
        //Humidity = 1,
        Temperature = 2,
        HumidityAndTemperature = 3,
        Relay = 4,
        RangeFinder = 5,
        Camera = 6
    }

    public enum RuleType : byte
    {
        Cron = 1,
        Action = 2,
        Display = 3,
    }

    public enum JobType
    {
        Read = 1,
        Toggle = 2,
        On = 3,
        Off = 4,
        Period = 5,
        Action = 6,
    }

    public enum DisplayType
    {
        Text = 1,
        Percent = 2,
    }

    public enum ActionType
    {
        TakePhoto = 0
    }

    public enum RelayState
    {
        Opened = 1,
        Closed = 2
    }

    public enum HealthCheck
    {
        Unhealthy,
        Degraded,
        Healthy,
    }

    public enum WorkState
    {
        Running,
        Paused,
        Stopped,
        Mixed
    }
}
