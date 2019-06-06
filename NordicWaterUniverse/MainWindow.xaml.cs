using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
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
using System.Threading;

namespace NordicWaterUniverse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CheckIn checkin = new CheckIn("Area 1");

        public MainWindow()
        {
            InitializeComponent();
            checkin.CheckIntoArea();
            
        }


        public void UpdateText()
        {
            //Add the input we got to a text field
            richTextBox1.Document.Blocks.Clear();
            richTextBox1.Document.Blocks.Add(new Paragraph(new Run(checkin.InputFromPort)));

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
