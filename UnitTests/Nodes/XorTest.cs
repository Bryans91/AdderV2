using Adder.Components;
using Adder.Components.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Nodes
{
    [TestClass]
    public class XorTest
    {
        [TestMethod]
        public void TestHandle()
        {
            Node node = new Xor();
            node.AddDefaultInputs("IN1", true);
            node.AddDefaultInputs("IN2", false);
            node.SetDefaultInputs();

            node.Handle();

            Assert.IsTrue(node.Output);
        }

        [TestMethod]
        public void TestHandleNegative()
        {
            Node node = new Xor();
            node.AddDefaultInputs("IN1", false);
            node.AddDefaultInputs("IN2", false);

            node.SetDefaultInputs();

            node.Handle();

            Assert.IsFalse(node.Output);
        }

    }
}
