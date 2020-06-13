using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components
{
    public class Edge
    {
        public Node In { get; set; }
        public Node Out { get; set; }

        public Edge(Node nodeIn, Node nodeOut)
        {
            In = nodeIn;
            Out = nodeOut;
        }

    }
}
