using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components
{
    public class Circuit : Component //Composite
    {

        public List<Component> Components { get; set; }

        public Circuit() : base()
        {
            Components = new List<Component>();
        }

        public override void Run(IVisitor visitor)
        {
            base.Run(visitor);
            Components.ForEach((Component component) =>
            {
                component.Run(visitor);
            });
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
