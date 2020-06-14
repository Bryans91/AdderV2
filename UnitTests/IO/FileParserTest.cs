using Adder.Components;
using Adder.Components.Nodes;
using Adder.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.IO
{
    [TestClass]
    public class FileParserTest
    {
        FileParser fp = new FileParser();

        [TestMethod]
        public void TestInput()
        {
            string[] lines =
            {
                "A: INPUT_HIGH;",
                "B: INPUT_LOW;",
                "Cin: INPUT_HIGH;",
                "A0: INPUT_LOW;",
                "A1: INPUT_HIGH;"
            };

            string[] expectedKeys =
            {
                "A", "B", "Cin", "A0", "A1"
            };

            string[] expectedValues =
            {
                "INPUT_HIGH", "INPUT_LOW", "INPUT_HIGH", "INPUT_LOW", "INPUT_HIGH"
            };

            String[] circuitParts;

            int i = 0;
            foreach (string line in lines)
            {
                circuitParts = fp.GetCircuitParts(line);
                Assert.AreEqual(circuitParts[0], expectedKeys[i]);
                Assert.AreEqual(circuitParts[1], expectedValues[i]);
                i++;
            }

        }

        [TestMethod]
        public void TestNodes()
        {
            string[] lines =
            {
                "ORNODE: OR;",
                "XORNODE: XOR;",
                "ANDNODE: AND;",
                "NANDNODE: NAND;",
                "NORNODE: NOR;",
                "NOTNODE: NOT;"
            };

            string[] expectedKeys =
            {
                "ORNODE", "XORNODE", "ANDNODE", "NANDNODE", "NORNODE", "NOTNODE"
            };

            string[] expectedValues =
            {
                "OR", "XOR", "AND", "NAND", "NOR", "NOT"
            };

            String[] circuitParts;

            int i = 0;
            foreach (string line in lines)
            {
                circuitParts = fp.GetCircuitParts(line);
                Assert.AreEqual(circuitParts[0], expectedKeys[i]);
                Assert.AreEqual(circuitParts[1], expectedValues[i]);
                i++;
            }
        }
    }
}
