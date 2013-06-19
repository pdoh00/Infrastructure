namespace Infrastructure
{
    using System;

    public class Clock : IClock
    {
        public DateTime GetCurrentTimeUTC()
        {
            return DateTime.UtcNow;
        }
    }
}
