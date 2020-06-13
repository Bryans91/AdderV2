using System;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public class Connections : IVisitor
    {
        public void Visit(Component visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(Circuit visited)
        {
            Console.Out.WriteLine(visited.Name + " Contains:");
            visited.Components.ForEach((component) =>
            {
                Console.Out.WriteLine(component.Name + " = " + component.GetType().Name);
            });
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
            Console.WriteLine(node.Name + " " + node.GetType().Name + " connects to:");
            node.OutputList.ForEach((item) => {
                Console.WriteLine(item.Out.Name + " " + item.Out.GetType().Name);
            });
        }
    }
}
