using System;
using System.Collections.Generic;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities
{
    public class Schedule : BaseEntity<Guid>
    {
        public int ClinicId { get; set; }

        public DateTimeRange TimeRange { get; private set; }

        private IList<Appointment> _appointments;

        public IEnumerable<Appointment> Appointments
        {
            get => _appointments;
            set => _appointments = (List<Appointment>) value;
        }

        public Schedule(Guid id, DateTimeRange timeRange) : base(id)
        {
            TimeRange = timeRange;
            _appointments = new List<Appointment>();
        }

        private Schedule() : base(new Guid())
        { 
            _appointments = new List<Appointment>();
        }
    }
}
