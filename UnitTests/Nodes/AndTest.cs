using Adder.Components;
using Adder.Components.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Nodes
{
    [TestClass]
    public class AndTest
    {
        [TestMethod]
        public void TestHandle()
        {
            Node and = new And();
            and.AddDefaultInputs("IN",true);
            and.AddDefaultInputs("IN2",true);
            and.SetDefaultInputs();
       
            and.Handle();

            Assert.IsTrue(and.Output);
        }

        [TestMethod]
        public void TestHandleNegative()
        {
            Node and = new And();
            and.AddDefaultInputs("IN",true);
            and.AddDefaultInputs("IN2", false);
            and.SetDefaultInputs();

            and.Handle();

            Assert.IsFalse(and.Output);
        }

    }
}
