using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components.Nodes
{
    public class Not : Node
    {
        public override void Handle()
        {
            base.Handle();
            this.Output = true;

            InputList.ForEach((bool input) =>
            {
                if (input)
                {
                    this.Output = false;
                }
            });
        }


        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
