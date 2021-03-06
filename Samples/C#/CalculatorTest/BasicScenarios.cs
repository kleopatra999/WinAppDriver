﻿//******************************************************************************
//
// Copyright (c) 2016 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace CalculatorTest
{
    [TestClass]
    public class BasicScenarios
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static RemoteWebDriver CalculatorSession;
        protected static RemoteWebElement CalculatorResult;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Launch the calculator app
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            CalculatorSession = new RemoteWebDriver(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(CalculatorSession);
            CalculatorSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

            // Use series of operation to locate the calculator result text element as a workaround
            // We currently cannot query element by automationId without using modified appium dot net driver
            // TODO: Use a proper appium/webdriver nuget package that allow us to query based on automationId
            CalculatorSession.FindElementByName("Clear").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorResult = CalculatorSession.FindElementByName("Display is  7 ") as RemoteWebElement;
            Assert.IsNotNull(CalculatorResult);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            CalculatorResult = null;
            CalculatorSession.Dispose();
            CalculatorSession = null;
        }

        [TestInitialize]
        public void Clear()
        {
            CalculatorSession.FindElementByName("Clear").Click();
            Assert.AreEqual("Display is  0 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Addition()
        {
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Plus").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Combination()
        {
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorSession.FindElementByName("Multiply by").Click();
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Plus").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            CalculatorSession.FindElementByName("Divide by").Click();
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Division()
        {
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Divide by").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Multiplication()
        {
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Multiply by").Click();
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  81 ", CalculatorResult.Text);
        }

        [TestMethod]
        public void Subtraction()
        {
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Minus").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }
    }
}
