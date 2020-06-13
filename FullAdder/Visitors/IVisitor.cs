using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public interface IVisitor
    {

        void Visit(Component visited);

        void Visit(Circuit visited);

        void Visit(Node visited);

        void Visit(And visited);

        void Visit(Nand visited);

        void Visit(Nor visited);

        void Visit(Not visited);

        void Visit(Or visited);

        void Visit(Xor visited);

    }
}
