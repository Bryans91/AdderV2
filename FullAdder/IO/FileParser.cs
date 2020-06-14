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
        public IDictionary<string, bool> InputDictionary = new Dictionary<string, bool>();
        public IDictionary<string, Node> NodeDictionairy = new Dictionary<string, Node>();
        public List<string[]> Nodes { get; set; }
        public List<string[]> Edges { get; set; }
        Circuit circuit;

        public Circuit ParseCircuit(String filePath)
        {
            FileReader fr = new FileReader(filePath);
            bool readingEdges = false;
            List<String> lines = fr.GetLines();

            if (lines != null)
            {
                Nodes = new List<string[]>();
                Edges = new List<string[]>();

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
                                Edges.Add(circuitParts);
                            }
                            else
                            {
                                Nodes.Add(circuitParts);
                            }
                        }
                    }
                }

                return circuit;
            }

            return null;
        }

        public String[] GetCircuitParts(String line)
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
