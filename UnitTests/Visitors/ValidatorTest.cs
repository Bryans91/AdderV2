using Adder.Components;
using Adder.Components.Nodes;
using Adder.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Visitors
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestNotCompletedLoop()
        {

            /** Basic circuit **/
            //Adder
            Node node1 = new Or() { Name = "Or 1" };
            Node node2 = new Or() { Name = "Or 2" };

            Node node3 = new Not() { Name = "Not 3" };
            Node node4 = new Not() { Name = "Not 4" };

            Node node5 = new And() { Name = "And 5" };
            Node node6 = new And() { Name = "And 6" };

            bool s = true;
            bool r = false;

            //Edges
            node5.AddDefaultInputs("IN1", s); //input 1
            node6.AddDefaultInputs("IN2", s);

            node5.AddDefaultInputs("IN3", r);
            node6.AddDefaultInputs("IN4", r);


            node1.AddOutput(node3);
            node2.AddOutput(node4);
            node3.AddOutput(node2);
            node4.AddOutput(node1);
            node5.AddOutput(node1);
            node6.AddOutput(node2);


            Circuit circuit = new Circuit() { Name = "Circuit 1" };
            circuit.Components.Add(node1);
            circuit.Components.Add(node2);
            circuit.Components.Add(node3);
            circuit.Components.Add(node4);
            circuit.Components.Add(node5);
            circuit.Components.Add(node6);

            circuit.Run(new Validator());

            foreach (Component comp in circuit.Components)
            {
                if (comp.ClassType != "Circuit" && !comp.Resolved)
                {
                    throw new Exception("Circuit not completed: " + comp.Name);
                }
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestIncorrectNumberOfInputs()
        {

            /** Basic circuit **/
            //Adder
            Node not1 = new Not() { Name = "Not 1" };
            Node or1 = new Or() { Name = "Or 1" };

            Node and1 = new And() { Name = "And 1" };
            Node nor1 = new Nor() { Name = "Nor 1" };

            bool input1 = true;
            bool input2 = false;

            //Edges
            not1.AddDefaultInputs("IN1", input1); //input 1

            or1.AddDefaultInputs("IN1", input1);
            or1.AddDefaultInputs("IN2", input2);


            //endpoint AND
            not1.AddOutput(and1);
            or1.AddOutput(and1);

            //create loop with too few inputs
            or1.OutputList.Add(new Edge(or1, not1));

            Circuit circuit = new Circuit() { Name = "Circuit 1" };
            circuit.Components.Add(not1);
            circuit.Components.Add(or1);
            circuit.Components.Add(and1);
            circuit.Components.Add(nor1);


            circuit.Run(new Validator());

        }
    }
}
