using Adder.Components;
using Adder.IO;
using Adder.Visitors;
using System;
using System.Collections.Generic;
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

        public List<Component> Items { get; set; }
        public IVisitor visit;
        public Dictionary<string, IVisitor> VisitorOptions { get; set; }
        public Circuit circuit;

        public Dictionary<string,bool> Inputs { get; set; }

        public MainWindow()
        {

            this.Items = new List<Component>();
            this.Inputs = new Dictionary<string, bool>();


            this.VisitorOptions = new Dictionary<string, IVisitor>();
            this.VisitorOptions.Add("Show connections", new Connections());
            this.VisitorOptions.Add("Print Output", new Displayer());

            InitializeComponent();

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
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

                circuit.Components.ForEach((comp) =>
                {
                    if(comp.ClassType == "Node")
                    {
                        
                    }
                });

             


                NodeList.ItemsSource = this.Items;
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
                circuit.Run(new Cleaner());
                circuit.PrintTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
