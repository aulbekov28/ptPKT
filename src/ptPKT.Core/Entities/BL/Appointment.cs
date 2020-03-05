using System;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class Appointment : BaseEntity<Guid>
    {
        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        public int RoomId { get; set; }

        public DateTimeRange StartEndTime { get; set; }

        public bool DateTimeConfirmed { get; set; }

        public bool IsPotentiallyConflicted { get; set; }


        public void UpdateTime(DateTimeRange newStartEndTime)
        {
            StartEndTime = newStartEndTime;
        }

        public void UpdateRoom(int newRoomId)
        {
            RoomId = newRoomId;
        }
    }
}
