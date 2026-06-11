using GymApp.Models;
using GymApp.Services;
using System;
using System.Collections.Generic;

namespace GymApp.Fakes
{
    public class FakeTrainingService : ITrainingService
    {
        private readonly List<Training> _trainings;

        public FakeTrainingService(List<Training> trainings)
        {
            _trainings = trainings;
        }

        public List<Training> GetTrainingsInTheLastMonth(Guid trainerId)
        {
            return _trainings;
        }
    }
}