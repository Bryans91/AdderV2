using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components.Nodes
{
    public class Nand : Node
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

            Output = (nrOfTrue != NrOfInputs);
        }


        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
