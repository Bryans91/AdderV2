using System;
using System.Collections.Generic;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public class Connections : IVisitor
    {
        public List<string> Output { get; set; }


        public Connections()
        {
            Output = new List<string>();
        }

        public void Visit(Component visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(Circuit visited)
        {
            // Console.Out.WriteLine(visited.Name + " Contains:");
            visited.Components.ForEach((component) =>
            {
                if (component.ClassType == "Node")
                {
                    Node node = (Node)component;

                    Output.Add(node.Name + " Connects to: ");
                    node.OutputList.ForEach((edge) => {
                        Output.Add(edge.Out.Name + ' ' + edge.Out.ClassType);
                    });
                }
               // Console.Out.WriteLine(component.Name + " = " + component.GetType().Name);
            });
            Console.Out.WriteLine("hoi");
        }

        public void Visit(Node visited)
        {
            throw new NotImplementedException();
        }


        public void Visit(And visited)
        {
            NodeConnectsTo(visited);
        }

        public void Visit(Nand visited)
        {
            NodeConnectsTo(visited);
        }

        public void Visit(Nor visited)
        {
            NodeConnectsTo(visited);
        }

        public void Visit(Not visited)
        {
            NodeConnectsTo(visited);
        }

        public void Visit(Or visited)
        {
            NodeConnectsTo(visited);
        }

        public void Visit(Xor visited)
        {
            NodeConnectsTo(visited);
        }

        private void NodeConnectsTo(Node node)
        {

            if (node.IsResolveable())
            {
                Output.Add(node.Name + " " + node.GetType().Name + " connects to:");


                Console.WriteLine(node.Name + " " + node.GetType().Name + " connects to:");
                node.OutputList.ForEach((item) =>
                {
                    Output.Add(item.Out.Name + " " + item.Out.GetType().Name);
                    Console.WriteLine(item.Out.Name + " " + item.Out.GetType().Name);
                });
            }
        }
    }
}
