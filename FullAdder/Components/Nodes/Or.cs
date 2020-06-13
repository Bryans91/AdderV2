using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components.Nodes
{
    public class Or : Node
    {
        public override void Handle()
        {
            base.Handle();

            Output = false;

            InputList.ForEach((bool input) =>
            {
                if (input)
                {
                    Output = true;
                }
            });
        }


        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
