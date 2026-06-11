using GymApp.Models;
using GymApp.Services;
using System;
using GymApp.Models;
using GymApp.Services;
using System;

namespace GymApp.Fakes
{
    public class FakeTrainerPerformanceService : ITrainerPerformanceService
    {
        private readonly PerformanceReport _report;

        public FakeTrainerPerformanceService(PerformanceRank rank, int percentNotHeld, int freeDaysLeft)
        {
            _report = new PerformanceReport()
            {
                PerformanceRank = rank,
                PercentOfTrainingsNotHeld = percentNotHeld,
                NumberOfFreeDaysLeft = freeDaysLeft
            };
        }

        public PerformanceReport GetTrainerPerformanceReport(Guid trainerId)
        {
            return _report;
        }
    }
}