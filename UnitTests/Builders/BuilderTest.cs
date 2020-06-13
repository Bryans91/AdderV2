using Adder.Components;
using Adder.Components.Nodes;
using Adder.Factories;
using Adder.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    [TestClass]
    public class BuilderTest
    {
        [TestMethod]
        public void TestBuilderNameResult()
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

            foreach (string type in types)
            {
                Builder nodeBuilder = new Builder(type);
                nodeBuilder.setName(type + "_NODE");

                Assert.IsTrue(nodeBuilder.Result().Name.Equals(type + "_NODE"));
            }
        }

        [TestMethod]
        public void TestBuilderTypeResult()
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

            foreach (string type in types)
            {
                Builder nodeBuilder = new Builder(type);
                nodeBuilder.setName(type + "_NODE");

                Assert.IsTrue(nodeBuilder.Result().GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
