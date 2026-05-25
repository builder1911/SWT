using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace PTE72BSelenium
{
    [TestClass]
    public sealed class PTE72BSeleniumChromedriver
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Chrome()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-features=PasswordLeakDetection");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.demoblaze.com/");
            Thread.Sleep(3000);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }

        [TestMethod]
        public void Lumia1520Vasarlas()
        {
            // Log In: username, pw
            driver.FindElement(By.Id("login2")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.Id("loginusername")).SendKeys("Bela1969");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("loginpassword")).SendKeys("Valami123");
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//button[text()='Log in']")).Click();
            Thread.Sleep(3000);

            // Sikeres bejelentkezés ellenőrzése
            string felhasznalo = driver.FindElement(By.Id("nameofuser")).Text;
            Assert.IsTrue(felhasznalo.Contains("Bela1969"));

            // Lumia 1520 kiválasztása
            driver.FindElement(By.XPath("//a[text()='Nokia lumia 1520']")).Click();
            Thread.Sleep(3000);

            // Lumia 1520 ellenőrzése
            string termekNev = driver.FindElement(By.ClassName("name")).Text;
            Assert.AreEqual("Nokia lumia 1520", termekNev);

            // Lunia 1520 Kosárba helyezés
            driver.FindElement(By.XPath("//a[text()='Add to cart']")).Click();
            Thread.Sleep(3000);

            // A kosárba helyezés üzenet elfogadása
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(3000);

            // Mi van a kosárban?
            driver.FindElement(By.Id("cartur")).Click();
            Thread.Sleep(3000);

            // Ellenőrizzük, hogy a termék a kosárban van-e?
            string kosarTermek = driver.FindElement(By.XPath("//td[text()='Nokia lumia 1520']")).Text;
            Assert.AreEqual("Nokia lumia 1520", kosarTermek);

            // Megindítjuk a vásárlást
            driver.FindElement(By.XPath("//button[text()='Place Order']")).Click();
            Thread.Sleep(3000);

            // Adatok kitöltése
            driver.FindElement(By.Id("name")).SendKeys("Bela Teszt");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("country")).SendKeys("Hungary");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("city")).SendKeys("Budapest");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("card")).SendKeys("123456789");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("month")).SendKeys("12");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("year")).SendKeys("2026");
            Thread.Sleep(3000);

            // Vásárlás
            driver.FindElement(By.XPath("//button[text()='Purchase']")).Click();
            Thread.Sleep(3000);

            // Vásárlás ellenőrzése
            string sikeresVasarlas = driver.FindElement(By.XPath("//h2[text()='Thank you for your purchase!']")).Text;
            Assert.AreEqual("Thank you for your purchase!", sikeresVasarlas);
            Thread.Sleep(3000);

            // A vásárlást megköszönő üzenet és rendelési szám felugró tájékoztató ablak okézása
            driver.FindElement(By.XPath("//button[text()='OK']")).Click();
            Thread.Sleep(3000);

            // Kijelentkezés
            driver.FindElement(By.Id("logout2")).Click();
            Thread.Sleep(3000);
        }
    }
}