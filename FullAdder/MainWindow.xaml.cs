using Adder.Components;
using Adder.IO;
using Adder.Visitors;
using Adder.Builders;
using FullAdder.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FullAdder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public VisitorOutputModel Output { get; set; }
        public List<InputViewModel> Inputs { get; set; }
        public Dictionary<string, IVisitor> VisitorOptions { get; set; }
        public IDictionary<string, bool> InputDictionary = new Dictionary<string, bool>();
        public IDictionary<string, Node> NodeDictionairy = new Dictionary<string, Node>();

        public IVisitor visit;
        public ErrorViewModel Error { get; set; }
        public TimerViewModel Timer { get; set; }
        public Circuit circuit;

        public MainWindow()
        {
         
            InitializeComponent();
            this.DataContext = this;

            InitializeProperties(); //Prepare view variables

            OpenFileDialog();
        }

        private void InitializeProperties()
        {
            this.Timer = new TimerViewModel();
            this.Error = new ErrorViewModel();
            this.Inputs = new List<InputViewModel>();
            this.Output = new VisitorOutputModel();
            this.InitVisitorOptions();
        }

        private void InitVisitorOptions()
        {
            this.VisitorOptions = new Dictionary<string, IVisitor>
            {
                { "Show connections", new Connections() },
                { "Print Output", new Displayer() }
            };

            VisitorList.ItemsSource = this.VisitorOptions;
        }

        private void SetInputBoxes()
        {
            //gets all possible input values
            circuit.Components.ForEach((comp) =>
            {
                if (comp.ClassType != "Circuit")
                {
                    Node node = (Node)comp;
                    if (node.DefaultInputs.Count > 0)
                    {
                        foreach (KeyValuePair<string, bool> entry in node.DefaultInputs)
                        {
                            if (!Inputs.Exists(x => x.Name == entry.Key))
                            {
                                Inputs.Add(new InputViewModel() { Name = entry.Key, Value = entry.Value });
                            }
                        }
                    }
                }
            });

            InputChecks.ItemsSource = this.Inputs;
        }

        private void OpenFileDialog()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Circuit1_FullAdder"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                try
                {
                    // Open document
                    string filename = dlg.FileName;

                    FileParser fp = new FileParser();
                    fp.ParseCircuit(filename);
                    List<string[]> Nodes = fp.Nodes;
                    List<string[]> Edges = fp.Edges;

                    circuit = new Circuit() { Name = "Circuit 1" };

                    foreach (string[] NodeData in Nodes)
                    {
                        Node node = AddNode(NodeData);
                        if (node != null)
                        {
                            NodeDictionairy[node.Name] = node;
                        }
                    }

                    foreach (string[] EdgeData in Edges)
                    {
                        AddEdges(EdgeData);
                    }


                    this.SetInputBoxes();
                }
                catch (Exception e)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public Node AddNode(String[] nodeParts)
        {
            if (!nodeParts[1].Contains("INPUT") && !nodeParts[1].Contains("PROBE"))
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

        public void AddEdges(String[] edgeParts)
        {

            bool inputType = false;
            bool input = false;

            if (!edgeParts[0].StartsWith("NODE"))
            {
                inputType = true;
                input = InputDictionary[edgeParts[0]];
            }

            foreach (String edgePart in edgeParts.Skip(1))
            {

                if (edgePart.StartsWith("NODE"))
                {

                    if (inputType)
                    {
                        NodeDictionairy[edgePart].AddDefaultInputs(edgeParts[0], input);
                    }
                    else
                    {
                        NodeDictionairy[edgeParts[0]].AddOutput(NodeDictionairy[edgePart]);
                    }
                }
                else
                {
                    NodeDictionairy[edgeParts[0]].OutputName = edgePart;
                }
            }

            if (!inputType)
            {
                circuit.Components.Add(NodeDictionairy[edgeParts[0]]);
            }

        }

        private void Visitors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.visit = (IVisitor) ((dynamic) VisitorList.SelectedItem).Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
          
                circuit.Run(this.visit);
                this.Output.List = new ObservableCollection<string>(this.visit.Output);
                this.visit.Output.Clear();

                circuit.Run(new Cleaner());

                this.Timer.Message = circuit.GetTime() + "(ns)";
            }
            catch (Exception ex)
            {
                this.Error.Message = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.SetInputs();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.SetInputs();
        }

        private void SetInputs()
        {
            circuit.Components.ForEach((comp) =>
            {
                if (comp.ClassType != "Circuit")
                {
                    Node node = (Node)comp;
                    foreach (var input in Inputs) {
                        if (node.DefaultInputs.ContainsKey(input.Name))
                        {
                            node.DefaultInputs[input.Name] = input.Value;
                        }
                    }
                }
            });
        }

        private void Validate()
        {
            circuit.Run(new Cleaner());
            circuit.Run(new Validator());
            foreach(Component comp in circuit.Components)
            {
                if (comp.ClassType != "Circuit" && ! comp.Resolved)
                {
                    throw new Exception("Circuit not completed: " + comp.Name);
                }
            }
            circuit.Run(new Cleaner());
        }
    }
}
