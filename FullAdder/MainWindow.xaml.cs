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

        public MainWindow()
        {

            this.Items = new List<Component>();


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
                Circuit circuit = fp.ParseCircuit(filename);
                //Circuit circuit = fp.ParseCircuit("../../../Files/Circuit1_FullAdder.txt");
                //Circuit circuit = fp.ParseCircuit("../../../Files/Circuit4_InfiniteLoop.txt");
                this.Items.AddRange(circuit.Components);
                NodeList.ItemsSource = this.Items;
           

                try
                {
                    circuit.Run(new Connections());
                    circuit.Run(new Cleaner());
                    circuit.Run(new Validator());
                    circuit.Run(new Cleaner());
                    circuit.Run(new Displayer());
                    circuit.PrintTime();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                Console.WriteLine("ended");

            }

            Console.Read();
        }
    }
}
