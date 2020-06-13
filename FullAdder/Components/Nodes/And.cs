using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Visitors;

namespace Adder.Components.Nodes
{
    public class And : Node
    {
        public override void Handle()
        {
            base.Handle();

            Output = true;

            InputList.ForEach((bool input) =>
            {
                if(!input)
                {
                    Output = false;
                }
            });
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
