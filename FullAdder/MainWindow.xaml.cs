using Adder.Components;
using Adder.IO;
using Adder.Visitors;
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

        public List<Component> Items { get; set; }
        public IVisitor visit;
        public Dictionary<string, IVisitor> VisitorOptions { get; set; }

        public Circuit circuit;
        

        public List<InputViewModel> Inputs { get; set; }

        public MainWindow()
        {

            this.Items = new List<Component>();
            this.Inputs = new List<InputViewModel>();
            this.Output = new VisitorOutputModel();


            this.VisitorOptions = new Dictionary<string, IVisitor>();
            this.VisitorOptions.Add("Show connections", new Connections());
            this.VisitorOptions.Add("Print Output", new Displayer());

            InitializeComponent();
            this.DataContext = this;

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
                // Open document
                string filename = dlg.FileName;
           


                FileParser fp = new FileParser();
                circuit = fp.ParseCircuit(filename);
                //Circuit circuit = fp.ParseCircuit("../../../Files/Circuit1_FullAdder.txt");
                //Circuit circuit = fp.ParseCircuit("../../../Files/Circuit4_InfiniteLoop.txt");
                this.Items.AddRange(circuit.Components);


                //gets all possible input values
                circuit.Components.ForEach((comp) =>
                {
                    if(comp.ClassType != "Circuit")
                    {
                        Node node = (Node)comp;
                        if(node.DefaultInputs.Count > 0)
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


                //OutputList.ItemsSource = this.Output.List;
                

               
      

                InputChecks.ItemsSource = this.Inputs;

                VisitorList.ItemsSource = this.VisitorOptions;
               
                Console.WriteLine("ended");

            }

            Console.Read();
        }


        private void Visitors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.visit = (IVisitor) ((dynamic) VisitorList.SelectedItem).Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                circuit.Run(this.visit);
                this.Output.List = new ObservableCollection<string>(this.visit.Output);
         
                circuit.Run(new Cleaner());
                circuit.PrintTime();
            }
            catch (Exception ex)
            {
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
    }
}
