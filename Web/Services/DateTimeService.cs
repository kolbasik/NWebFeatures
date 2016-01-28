using System;

namespace Web.Services
{
    public interface IDateTimeService
    {
        DateTime GetUtcTime();
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTime GetUtcTime()
        {
            return DateTime.UtcNow;
        }
    }
}