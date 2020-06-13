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
        public void TestInfiniteLoop()
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

            //endpoint NOR
            not1.AddOutput(nor1);
            or1.AddOutput(nor1);


            //create infinite loop
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
