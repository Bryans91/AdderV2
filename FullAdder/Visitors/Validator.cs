using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adder.Components;
using Adder.Components.Nodes;

namespace Adder.Visitors
{
    public class Validator : IVisitor
    {
        public void Visit(Component visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(Circuit visited)
        {
            if (visited.Components.Count == 0)
            {
                throw new Exception("The circuit has no nodes.");
            }


            //visited.Components.ForEach((node) => {
            //    if (node.OutputName != null || node.Visited == 0)
            //    {
            //        throw new Exception("No input deteced at probe " + node.Name);
            //    }
            //});

        }

        public void Visit(Node visited)
        {
            throw new Exception("An abstract node has been created.");
        }


        public void Visit(And visited)
        {
            HasCorrectNumberOfInputNodes(visited, 2);
            IsInfinite(visited);
        }

        public void Visit(Nand visited)
        {
            HasCorrectNumberOfInputNodes(visited, 2);
            IsInfinite(visited);
        }

        public void Visit(Nor visited)
        {
            HasCorrectNumberOfInputNodes(visited, 2);
            IsInfinite(visited);
        }

        public void Visit(Not visited)
        {
            HasCorrectNumberOfInputNodes(visited, 1);
            IsInfinite(visited);
        }

        public void Visit(Or visited)
        {
            HasCorrectNumberOfInputNodes(visited, 2);
            IsInfinite(visited);
        }

        public void Visit(Xor visited)
        {
            HasCorrectNumberOfInputNodes(visited, 2);
            IsInfinite(visited);
        }


        private void HasCorrectNumberOfInputNodes(Node node, int min)
        {
            if (node.NrOfInputs < min)
            {
                throw new Exception("Node: "+ node.Name + " does not have enough inputs. Has:" + node.NrOfInputs + " Minimum requirement:" + min);
            }
        }

        private bool HasNext(Node node)
        {
            return node.OutputList.Count != 0;
        }


        private bool IsInfinite(Node node)
        {
            if (node.Visited > node.NrOfInputs)
            {
                throw new Exception("Infinite loop detected at node (visited counter): " + node.Name);
            }

            return false;
        }

    }
}
