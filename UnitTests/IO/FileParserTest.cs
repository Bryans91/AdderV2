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

            foreach (string line in lines)
            {
                String[] circuitParts = fp.GetCircuitParts(line);
                fp.AddNode(circuitParts);
            }

            Assert.IsTrue(fp.InputDictionary["A"]);
            Assert.IsFalse(fp.InputDictionary["B"]);
            Assert.IsTrue(fp.InputDictionary["Cin"]);
            Assert.IsFalse(fp.InputDictionary["A0"]);
            Assert.IsTrue(fp.InputDictionary["A1"]);

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

            string[] types =
            {
                "Or",
                "Xor",
                "And",
                "Nand",
                "Nor",
                "Not"
            };

            int i = 0;
            foreach (string line in lines)
            {
                String[] circuitParts = fp.GetCircuitParts(line);
                Node node = fp.AddNode(circuitParts);

                Assert.IsTrue(node.GetType().Name.Equals(types[i]));
                i++;
            }
        }
    }
}
