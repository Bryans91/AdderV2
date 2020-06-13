using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Adder.Factories;
using Adder.Components;
using Adder.Builders;

namespace Adder.IO
{
    public class FileParser
    {

        public bool A = false;
        public bool B = false;
        public bool Cin = false;
        IDictionary<string, bool> InputDictionary = new Dictionary<string, bool>();
        IDictionary<string, Node> NodeDictionairy = new Dictionary<string, Node>();
        Circuit circuit;

        public Circuit ParseCircuit(String filePath)
        {
            FileReader fr = new FileReader(filePath);
            bool readingEdges = false;
            List<String> lines = fr.GetLines();

            if (lines != null)
            {

                circuit = new Circuit() { Name = "Circuit 1" };

                foreach (String line in lines)
                {
                    if (line.Equals("# Description of all the edges"))
                    {
                        readingEdges = true;
                    }

                    if ( ! line.StartsWith("#") && !String.IsNullOrEmpty(line))
                    {
                        if (line.Contains(":") && line.EndsWith(";"))
                        {
                            String[] circuitParts = GetCircuitParts(line);
                            if (readingEdges)
                            {
                                AddEdges(circuitParts);
                            }
                            else
                            {
                                Node node = AddNode(circuitParts);
                                if (node != null)
                                {
                                    NodeDictionairy[node.Name] = node;
                                }
                            }
                        }
                    }
                }

                return circuit;
            }

            return null;
        }

        private Node AddNode(String[] nodeParts)
        {
            if ( ! nodeParts[1].Contains("INPUT") && ! nodeParts[1].Contains("PROBE"))
            {
                Builder nodeBuilder = new Builder(nodeParts[1]);
                nodeBuilder.setName(nodeParts[0]);

                return nodeBuilder.Result();
            }
            if (nodeParts[1].Contains("INPUT"))
            {
                InputDictionary.Add(nodeParts[0], nodeParts[1].Contains("HIGH") ? true : false);
            }

            return null;
        }

        private void AddEdges(String[] edgeParts)
        {

            bool inputType = false;
            bool input = false;

            if (!edgeParts[0].StartsWith("NODE"))
            {
                inputType = true;
                input = InputDictionary[edgeParts[0]];
            }

            foreach(String edgePart in edgeParts.Skip(1))
            {
               
                if (edgePart.StartsWith("NODE"))
                {

                    if (inputType)
                    {
                        NodeDictionairy[edgePart].AddDefaultInputs(edgeParts[0],input);
                    }
                    else
                    {
                        NodeDictionairy[edgeParts[0]].AddOutput(NodeDictionairy[edgePart]);
                    }
                } else {
                    NodeDictionairy[edgeParts[0]].OutputName = edgePart;
                }
            }

            if (!inputType)
            {
                circuit.Components.Add(NodeDictionairy[edgeParts[0]]);
            }

        }

        private String[] GetCircuitParts(String line)
        {
            String pattern = @"([a-zA-Z0-9_]+)";
            var result = Regex.Matches(line, pattern)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();

            return result;
        }
    }
}
