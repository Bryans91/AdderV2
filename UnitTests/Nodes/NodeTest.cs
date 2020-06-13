using Adder.Components;
using Adder.Components.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Nodes
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void TestAddOutput()
        {
            Node from = new Not();
            Node to = new Not();

            from.AddOutput(to);

            Assert.AreEqual(to.NrOfInputs, 1);
            Assert.AreEqual(from.OutputList[0].Out, to);
        }

        [TestMethod]
        public void TestAddDefaults()
        {
            Node node = new Not();

            node.AddDefaultInputs("IN1", false);
            node.AddDefaultInputs("IN2", true);
            node.SetDefaultInputs();

            Assert.AreEqual(2, node.NrOfInputs);
            Assert.AreEqual(2, node.InputList.Count);
        }
    }
}
