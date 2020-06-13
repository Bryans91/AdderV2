using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components.Nodes
{
    public class Xor : Node
    {
        public override void Handle()
        {
            base.Handle();
            int nrOfTrue = 0;

            InputList.ForEach((bool input) =>
            {
                if (input)
                {
                    nrOfTrue++;
                }
            });

            Output = (nrOfTrue == 1);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

   
}
