using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public class Displayer : IVisitor
    {
        public void Visit(Component visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(Circuit visited)
        {
            Console.WriteLine("Circuit: "+ visited.Name);
        }

        public void Visit(Node visited)
        {

        }


        public void Visit(And visited)
        {
            PrintStandardNode(visited);
        }

        public void Visit(Nand visited)
        {
            PrintStandardNode(visited);
        }

        public void Visit(Nor visited)
        {
            PrintStandardNode(visited);
        }

        public void Visit(Not visited)
        {
            PrintStandardNode(visited);
        }

        public void Visit(Or visited)
        {
            PrintStandardNode(visited);
        }

        public void Visit(Xor visited)
        {
            PrintStandardNode(visited);
        }

        private void PrintStandardNode(Node node)
        {
            if (node.IsResolveable() && !node.Printed)
            {
                node.Printed = true;

                if (node.OutputList.Count > 0)
                {
                    Console.WriteLine("Node " + node.Name + " " + node.GetType().Name + " Outputs " + node.Output + " To:");
                    node.OutputList.ForEach((Edge e) =>
                    {
                        Console.WriteLine(e.Out.Name + " " + e.Out.GetType().Name);
                    });
                }
                else
                {
                    Console.WriteLine("Node " + node.Name + " Outputs " + node.Output);
                }
            }
        }
    }
}
