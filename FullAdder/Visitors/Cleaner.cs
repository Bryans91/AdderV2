using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public class Cleaner : IVisitor
    {
        public void Visit(Component visited)
        {
            ResetPrinted(visited);
        }

        public void Visit(Circuit visited)
        {
            ResetPrinted(visited);
        }

        public void Visit(Node visited)
        {
            ResetNode(visited);
        }

        public void Visit(And visited)
        {
            ResetNode(visited);
        }

        public void Visit(Nand visited)
        {
            ResetNode(visited);
        }

        public void Visit(Nor visited)
        {
            ResetNode(visited);
        }

        public void Visit(Not visited)
        {
            ResetNode(visited);
        }

        public void Visit(Or visited)
        {
            ResetNode(visited);
        }

        public void Visit(Xor visited)
        {
            ResetNode(visited);
        }

        private void ResetNode(Node node)
        {
            ResetPrinted(node);
            ResetInputs(node);
        }

        private void ResetPrinted(Component component)
        {
            component.Printed = false;
        }

        private void ResetInputs(Node node)
        {
            node.InputList.Clear();
            node.Visited = 0;
        }
    }
}
