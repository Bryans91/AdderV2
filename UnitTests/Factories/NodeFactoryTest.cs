using Adder.Components;
using Adder.Components.Nodes;
using Adder.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Factories
{
    [TestClass]
    public class NodeFactoryTest
    {
        [TestMethod]
        public void TestResultType()
        {
            string[] types =
            {
                "OR",
                "XOR",
                "AND",
                "NAND",
                "NOR",
                "NOT"
            };

            NodeFactory nodeFactory = NodeFactory.GetInstance();

            foreach (string type in types)
            {
                Node node = nodeFactory.Create(type);
                Assert.IsTrue(node.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
