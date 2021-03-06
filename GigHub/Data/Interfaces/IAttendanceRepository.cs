﻿using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Data.Interfaces
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        void Remove(Attendance attendance);
        void Add(Attendance attendance);
        bool IsAttending(int gigId, string userId);
    }
}
