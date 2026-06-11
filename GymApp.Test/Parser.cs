using GymApp.Models;
using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GymApp.Test
{
    public class Parser
    {
        // numberOfMonths  groupTrainings  monthlyPriceBudget  trainingTime  membershipType
        public static IEnumerable Parsiraj(string fileName)
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}/{fileName}";
            //string path= $@"C:\Users\IT21 - 2024\source\repos\test - 2 - grupa - d - NinaAtanaskovic\GymApp.Test\Rez.txt";
            string[] lines = File.ReadAllLines(path);
            List<TestCaseData> testCases = new List<TestCaseData>();

            foreach (string line in lines)
            {
                string[] values = line.Split('\t');
                int numberOfMonths = int.Parse(values[0]);
                bool groupTrainings = bool.Parse(values[1]);
                double monthlyPriceBudget = double.Parse(values[2]);
                TrainingTime trainingTime = (TrainingTime)Enum.Parse(typeof(TrainingTime), values[3]);
                MembershipType? membershipType = values[4].Equals("null", StringComparison.OrdinalIgnoreCase)
                    ? (MembershipType?)null
                    : (MembershipType)Enum.Parse(typeof(MembershipType), values[4]);

                testCases.Add(new TestCaseData(numberOfMonths, groupTrainings, monthlyPriceBudget, trainingTime, membershipType));
            }

            return testCases;
        }
    }
}