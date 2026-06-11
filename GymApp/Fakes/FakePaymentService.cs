using GymApp.Models;
using GymApp.Services;
using System;

namespace GymApp.Fakes
{
    public class FakePaymentService : IPaymentService
    {
        public BonusPayment BonusPayment { get; set; }

        public void UpdateTrainerBonusPayment(Guid trainerId, BonusPayment payment)
        {
            BonusPayment = payment;
        }
    }
}