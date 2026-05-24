using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using StudentPerformanceApp.Models;
using StudentPerformanceApp.Services;

namespace TanulmanyiEredmenyTeszt
{
    [TestClass]
    public sealed class PTE72B_teszt
    {
        [TestMethod]
        public void Atlag_OtosJegy_HelyesEredmeny()  // Ellenőrzük, hogy az 5-ös jegy elfogadott érték
        {
            var ertekelo = new StudentPerformanceEvaluator();
            var tantargyak = new List<CourseRecord>
            {
                new CourseRecord { Grade = 5, Credit = 4 }
            };

            double eredmeny = ertekelo.CalculateWeightedAverage(tantargyak);
            Assert.AreEqual(5.0, eredmeny, 0.001);
        }

        [TestMethod]
        public void Atlag_EgyKredit_HelyesEredmeny()  // Ellenőrzük, hogy az 1 kredit még elfogadott pozitív kredit
        {
            var ertekelo = new StudentPerformanceEvaluator();
            var tantargyak = new List<CourseRecord>
            {
                new CourseRecord { Grade = 3, Credit = 1 }
            };

            double eredmeny = ertekelo.CalculateWeightedAverage(tantargyak);
            Assert.AreEqual(3.0, eredmeny, 0.001);
        }

        [TestMethod]
        public void Atlag_TobbTargy_EgeszAtlag()  // Több tárgyból számolunk átlagot, ahol az eredmény egész szám
        {
            var ertekelo = new StudentPerformanceEvaluator();
            var tantargyak = new List<CourseRecord>
            {
                 new CourseRecord { Grade = 2, Credit = 2 },
                 new CourseRecord { Grade = 4, Credit = 2 },
                 new CourseRecord { Grade = 3, Credit = 2 }
            };

            double eredmeny = ertekelo.CalculateWeightedAverage(tantargyak);
            Assert.AreEqual(3.0, eredmeny, 0.001);
        }

        [TestMethod]
        public void Atlag_MinuszKredit_HibatJelez() // Ha negatív kredit lenne akkor hibát jelez
        {
            var ertekelo = new StudentPerformanceEvaluator();
            var tantargyak = new List<CourseRecord>
            {
                new CourseRecord { Grade = 4, Credit = -1 }
            };

            try
            {
                ertekelo.CalculateWeightedAverage(tantargyak);
                Assert.Fail("A programnak jeleznie kellett volna, hogy a kredit hibás.");
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        public void Minosites_EgyesAtlag_Elegtelen() // Ha 1.0 lett az átlag akkor Elégtelen minősítést kapunk
        {
            var ertekelo = new StudentPerformanceEvaluator();
            string eredmeny = ertekelo.DetermineClassification(1.0);
            Assert.AreEqual("Elégtelen", eredmeny);
        }

        [TestMethod]
        public void Minosites_HaromAtlag_Kozepes() // Ha 3.0 lett az átlag akkor Közepes lesz a minősítés
        {
            var ertekelo = new StudentPerformanceEvaluator();
            string eredmeny = ertekelo.DetermineClassification(3.0);
            Assert.AreEqual("Közepes", eredmeny);
        }

        [TestMethod]
        public void Minosites_NegyAtlag_Jo() // 4.0 átlag esetén a minősítés Jó
        {
        
            var ertekelo = new StudentPerformanceEvaluator();
            string eredmeny = ertekelo.DetermineClassification(4.0);
            Assert.AreEqual("Jó", eredmeny);
        }

        [TestMethod]
        public void Osztondij_HaromPontKilencAtlag_NemJar() // 4.0 átlag alatt nincs ösztöndíj
        {
        
            var osztondijErtekelo = new ScholarshipEvaluator();
            bool eredmeny = osztondijErtekelo.IsEligible(3.9, 0);
            Assert.IsFalse(eredmeny);
        }

        [TestMethod]
        public void Osztondij_JoAtlagDeVanBukas_NemJar() // Jó az átlag, de van bukás, így nem jár ösztöndíj :(
        {
            var osztondijErtekelo = new ScholarshipEvaluator();
            bool eredmeny = osztondijErtekelo.IsEligible(4.8, 1);
            Assert.IsFalse(eredmeny);
        }

        [TestMethod]
        public void Osztondij_HatosAtlag_HibatJelez() // 5.0 feletti átlag nem lehetséges
        {
            var osztondijErtekelo = new ScholarshipEvaluator();

            try
            {
                osztondijErtekelo.IsEligible(6.0, 0);
                Assert.Fail("A programnak jeleznie kellett volna, hogy az átlag hibás.");
            }
            catch (ArgumentException) { }
        }
    }
}