using GymApp.Exceptions;
using GymApp.Fakes;
using GymApp.Models;
using GymApp.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
namespace GymApp.Test

{
    //Guid example: "00000000-0000-0000-0000-000000000001"
    [TestFixture]
    public class GymServiceTest
    {

                                                                          //neodrzani, grupa, free
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.Second, 14, 5, 18, 160.0)]

        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.Second, 14, 5, 16, 130.0)]
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.Second, 15, 18, 5, 0.0)]
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.First, 20, 11, 1, 180.0)]
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.First, 8, 5, 1, 180.0)]
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.First, 10, 9, 1, 150.0)]
        [TestCase("00000000-0000-0000-0000-000000000001", PerformanceRank.Third, 5, 5, 5, 0.0)]
        public void DoStaffBonusPaymentCalculation_ShouldCalculateBonusPayment(Guid Id, PerformanceRank rank,  int percentOfTrainingsNotHeld, int numberOfGroupTrainings, int numberOfFreeDaysLeft, double expectedBonus)
        {
            FakePaymentService fakePaymentService = new FakePaymentService();

            FakeTrainerPerformanceService fakeTrainerPerformanceService = new FakeTrainerPerformanceService( rank, percentOfTrainingsNotHeld, numberOfFreeDaysLeft);
            Guid trainerGuid = Id;
            Trainer trainer = new Trainer() { Id = trainerGuid };
            List<Training> trainings = new List<Training>();
            for (int i = 0; i < numberOfGroupTrainings; i++)
            {
                trainings.Add(new Training() { Type = TrainingType.Group });
            }
            
            FakeTrainingService fakeTrainingService = new FakeTrainingService(trainings);

            GymService gymService = new GymService(fakePaymentService, fakeTrainingService, fakeTrainerPerformanceService);
    
            gymService.DoStaffBonusPaymentCalculation(trainer);
            Assert.That(expectedBonus, Is.EqualTo(fakePaymentService.BonusPayment.Amount));
        }

        // F1 - Exception:
        // 
        public void DoStaffBonusPaymentCalculation_ShouldThrowNoTrainingsInTheLastMonthException()
        {

            List<Training> trainings = new List<Training>();//pravi praznu listu pa mu je count 0 
            FakeTrainingService fakeTrainingService = new FakeTrainingService(trainings);

            GymService gymService = new GymService(null, fakeTrainingService, null);
            NoTrainingsInTheLastMonthException ex = Assert.Throws<NoTrainingsInTheLastMonthException>((TestDelegate)
                (() => gymService.DoStaffBonusPaymentCalculation(new Trainer() { Id = Guid.NewGuid() })));


        }
        public void DoStaffBonusPaymentCalculation_ShouldThrowNoTrainingsInTheLastMonthException_poruka()
        {

            List<Training> trainings = new List<Training>();//pravi praznu listu pa mu je count 0 
            FakeTrainingService fakeTrainingService = new FakeTrainingService(trainings);

            GymService gymService = new GymService(null, fakeTrainingService, null);
            NoTrainingsInTheLastMonthException ex = Assert.Throws<NoTrainingsInTheLastMonthException>((TestDelegate)
                (() => gymService.DoStaffBonusPaymentCalculation(new Trainer() { Id = Guid.NewGuid() })));

            Assert.That(ex.Message, Is.EqualTo("Bonus payment cannot be calculated"));


        }

        //F2 - substitute
        [Test]
        public void DoStairBonusPaymentCalculation_SubstituteException()
        {
            FakeTrainingService fakeTraining = new FakeTrainingService(new List<Training>());
            IPaymentService paymentService = Substitute.For<IPaymentService>();
            ITrainerPerformanceService performanceService= Substitute.For<ITrainerPerformanceService>();
            GymService servis= new GymService(paymentService, fakeTraining, performanceService);
            NoTrainingsInTheLastMonthException ex = Assert.Throws<NoTrainingsInTheLastMonthException>((TestDelegate)
               (() => servis.DoStaffBonusPaymentCalculation(new Trainer() { Id = Guid.NewGuid() })));

            Assert.That(ex.Message, Is.EqualTo("Bonus payment cannot be calculated"));

        }


        //F3

        // (exp)membershipType	trainingTime	numberOfMonths	groupTrainings	monthlyPriceBudget
        [TestCaseSource(typeof(Parser), "Parsiraj" , new object[] {"Rez.txt"})]
        public void GetMemberhipType_Succes(MembershipType exp, TrainingTime time, int numberOfMonths, bool groupTrainings, double monthlyPriceBoudget)
        {
            GymService gymService = new GymService();
            MembershipType? tip= gymService.GetMemberhipType(numberOfMonths,groupTrainings, monthlyPriceBoudget,time);
            Assert.That(exp, Is.EqualTo(tip));
        }


    }
}