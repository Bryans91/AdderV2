using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Factories;
using Adder.Components;

namespace Adder.Builders
{
    class Builder : IBuilder
    {
        private Node node;

        public Builder(String type)
        {
            NodeFactory nodeFactory = NodeFactory.GetInstance();
            node = nodeFactory.Create(type);
        }

        public void addEdges()
        {

        }

        public void setName(String name)
        {
            node.Name = name;
        }

        public void AddDefaultInput(string name, bool input)
        {
            node.AddDefaultInputs(name, input);
        }

        public Node Result()
        {
            return node;
        }
    }
}
