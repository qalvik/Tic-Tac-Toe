using System;
using System.Collections.Generic;
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


namespace TicTac
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool turn = true; // true = X turn
        int turn_count = 0;

        public MainWindow()
        {
            Button btn = new Button();
            InitializeComponent();
            // a1.PerformClick();
       
         } 
   
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made by Chris Merritt (https://www.youtube.com/watch?v=p3gYVcggQOU) and Samuel", "Tic Tac Toe");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (turn)
                button.Content = "X";
            else
                button.Content = "O";

            turn = !turn;
            button.IsEnabled = false;
            turn_count++;

            CheckForWinner();
        }
        
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CheckForWinner()
        {
            bool winner = false;

            if ((a1.Content == a2.Content) && (a2.Content == a3.Content) && !a1.IsEnabled)
            winner = true;
            else if ((b1.Content == b2.Content) && (b2.Content == b3.Content) && !b1.IsEnabled) 
            winner = true;
            else if ((c1.Content == c2.Content) && (c2.Content == c3.Content) && !c1.IsEnabled) 
            winner = true;

            else if ((a1.Content == b1.Content) && (b1.Content == c1.Content) && !a1.IsEnabled)
                winner = true;
            else if ((a2.Content == b2.Content) && (b2.Content == c2.Content) && !a2.IsEnabled)
                winner = true;
            else if ((a3.Content == b3.Content) && (b3.Content == c3.Content) && !a3.IsEnabled)
                winner = true;

            else if ((a1.Content == b2.Content) && (b2.Content == c3.Content) && !a1.IsEnabled)
                winner = true;
            else if ((a3.Content == b2.Content) && (b2.Content == c1.Content) && !a3.IsEnabled)
                winner = true;

            if (winner)
            {
                string player = "";
                if (turn)
                    player = "O";
                else
                    player = "X";
                MessageBox.Show(player + " won!");

                DisableButtons();
            }
            else
            {
               if(turn_count == 9)
                {
                    MessageBox.Show("It's a draw");
                    DisableButtons();
                }
            
              
            }
           
        }
        private void DisableButtons()
        {
            try
            {
                foreach (Control ctr in ticGrid.Children)
                {

                    Button b = (Button)ctr;
                    b.IsEnabled = false;
                    aiButton.IsEnabled = false;
                }
            }
            catch { }

     }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            turn = true;
            turn_count = 0;
           
            aiButton.IsEnabled = true;

            try
            {
                foreach (Control ctr in ticGrid.Children)
                {
                    Button b = (Button)ctr;
                    b.IsEnabled = true;
                    b.Content = "";
                }
            }
            catch { }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
          /*
            Random rand = new Random();
            int index = rand.Next(result.Count);
            string randomString = result[index];

            foreach (Control ctrl in ticGrid.Children)
            {
                if (ctrl is Button && ctrl.Name == randomString)
                {
                    btn = (Button)(ctrl);
                }
            }

            btn.PerformClick();
            */
        }

        private void AiButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            Button btn = new Button();
            List<string> assingedButtonNames = new List<string> { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" };
            List<string> buttonNames = new List<string>();

            try // Add already clicked buttons to a list
            { 
                foreach (Button button in ticGrid.Children)
                {
                    if (button.Content.ToString() == "X" | button.Content.ToString() == "O")
                    {
                        string buttonName = button.Name;
                        buttonNames.Add(button.Name.ToString());
                    }     
                }
            }
            catch{ }

            if (turn_count < 4 ) // First "AI" turn in a random corner
            {
                foreach (Control ctrl in ticGrid.Children)
                {
                    List<string> edgeButtons = new List<string> { "a1", "a3", "c1", "c3" };
                    var newButtons = edgeButtons.Except(buttonNames).ToList();

                    int index = rand.Next(newButtons.Count);
                    string randomString = newButtons[index];

                    if (ctrl is Button && ctrl.Name == randomString)
                    {
                        btn = (Button)(ctrl);
                    }
                }        
            }
             else // Random button clicks
             {
                 var result = assingedButtonNames.Except(buttonNames).ToList();

                 int index1 = rand.Next(result.Count);
                 string randomString1 = result[index1];

                 foreach (Control ctrl in ticGrid.Children)
                 {
                     if (ctrl is Button && ctrl.Name == randomString1)
                     {
                         btn = (Button)(ctrl);
                     }
                 }
             }

            btn.PerformClick();
        }
       
    }

    public static class MyExt
    {
        public static void PerformClick(this Button btn)
        {
            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }

}
