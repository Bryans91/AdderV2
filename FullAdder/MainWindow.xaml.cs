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
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        public MainWindow()
        {
            InitializeComponent();


            FileParser fp = new FileParser();
            //Circuit circuit = fp.ParseCircuit("../../../Files/Circuit1_FullAdder.txt");
            Circuit circuit = fp.ParseCircuit("../../../Files/Circuit4_InfiniteLoop.txt");


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



            Console.Read();
        }
    }
}
