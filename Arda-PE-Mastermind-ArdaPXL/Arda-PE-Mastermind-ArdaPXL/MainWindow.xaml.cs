using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mastermind
{
    public partial class MainWindow : Window
    {
        private List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private List<string> code;
        DispatcherTimer timer;
        DateTime clicked;
        TimeSpan elapsedTime;
        //private List<string> attempts;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = DateTime.Now - clicked;  // Update elapsedTime correctly
            timeLabel.Text = $"{elapsedTime.Seconds}.{elapsedTime.Milliseconds.ToString().PadLeft(3, '0')} ";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime nu = DateTime.Now;
        }

        private void InitializeGame()
        {
            
            // Generate a random code
            Random rand = new Random();
            code = new List<string>();
            //attempts = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                //code.Add(attempts[rnd.Next(attempts.Count)]);
                code.Add(colors[rand.Next(colors.Count)]);
            }
            int attempts = rand.Next(0, 5);
            this.Title = $"Mastermind - Code: {string.Join(", ", code)} -- Pogingen: {attempts}";
            //"Mastermind - Code: " + string.Join(", ", code) + "pogingen:" + poging;




            // Populate ComboBoxes with color options
            ComboBox[] comboBoxes = { ComboBox1, ComboBox2, ComboBox3, ComboBox4 };
            for (int i = 0; i < comboBoxes.Length; i++)
            {
                comboBoxes[i].ItemsSource = colors;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            System.Windows.Controls.Label label = null;

            if (comboBox == ComboBox1) label = Label1;
            else if (comboBox == ComboBox2) label = Label2;
            else if (comboBox == ComboBox3) label = Label3;
            else if (comboBox == ComboBox4) label = Label4;

            if (label != null && comboBox.SelectedItem != null)
            {
                string selectedColor = comboBox.SelectedItem.ToString();
                label.Content = selectedColor;
                label.Background = GetColorBrush(selectedColor);
                label.BorderBrush = Brushes.Transparent; // Reset border color
            }
        }
        private Brush GetColorBrush(string colorName)
        {
            if (colorName == "Red") return Brushes.Red;
            if (colorName == "Yellow") return Brushes.Yellow;
            if (colorName == "Orange") return Brushes.Orange;
            if (colorName == "White") return Brushes.White;
            if (colorName == "Green") return Brushes.Green;
            if (colorName == "Blue") return Brushes.Blue;
            return Brushes.Transparent;
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox[] comboBoxes = { ComboBox1, ComboBox2, ComboBox3, ComboBox4 };
            List<string> selectedColors = new List<string>();

            clicked = DateTime.Now;
            timer.Start();

            for (int i = 0; i < comboBoxes.Length; i++)
            {
                if (comboBoxes[i].SelectedItem != null)
                {
                    selectedColors.Add(comboBoxes[i].SelectedItem.ToString());
                }
                else
                {
                    selectedColors.Add(null);
                }
            }
            System.Windows.Controls.Label[] labels = { Label1, Label2, Label3, Label4 };
            for (int i = 0; i < labels.Length; i++)
            {
                if (selectedColors[i] == code[i])
                {
                    labels[i].BorderBrush = Brushes.Green; // Correct color and position
                }
                else if (code.Contains(selectedColors[i]))
                {
                    labels[i].BorderBrush = Brushes.Orange; // Correct color, wrong position
                }
                else
                {
                    labels[i].BorderBrush = Brushes.DarkRed; // Incorrect color
                }
            }
        }



    }
}