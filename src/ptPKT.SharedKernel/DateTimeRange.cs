using ptPKT.SharedKernel.Interfaces;
using System;

namespace ptPKT.SharedKernel
{
    public class DateTimeRange : IRange<DateTime>
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End= end;
        }

        public DateTimeRange(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start.Add(duration);
        }

        protected DateTimeRange()
        {
        }

        public bool Includes(DateTime value)
        {
            return Start <= value && value <= End;
        }

        public bool Includes(IRange<DateTime> range)
        {
            return Start <= range.Start && range.End <= End;
        }

        public int DurationMinutes => (End - Start).Minutes;

        public DateTimeRange NewStart(DateTime newStart)
        {
            return new DateTimeRange(newStart, this.End);
        }

        public DateTimeRange NewEnd(DateTime newEnd)
        {
            return new DateTimeRange(this.Start, newEnd);
        }

        public DateTimeRange NewEnd(TimeSpan newDuration)
        {
            return new DateTimeRange(this.Start, newDuration);
        }
    }
}
