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

namespace WpfApp3_krestiki
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn;
        private bool Game;

        public MainWindow()
        {
            InitializeComponent();
            BeginRestart();
        }

        private void BeginRestart()
        {
            Begin_restart.Content = "Начать";
            isPlayerXTurn = true;
            Game = true;

            
            InfoTextBlock.Text = "";

            
            foreach (var button in mainGrid.Children.OfType<Button>())
            {
                button.IsEnabled = false;
                button.Content = "";
            }

            
            Begin_restart.Content = "Начать заново";
            Begin_restart.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Game)
            {
                Button button = (Button)sender;

                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    button.Content = isPlayerXTurn ? "X" : "O";

                    if (CheckForWinner())
                    {
                        DisplayWinner();
                    }
                    else if (IsBoardFull())
                    {
                        DisplayTie();
                    }
                    else
                    {
                        isPlayerXTurn = !isPlayerXTurn;

                        if (!isPlayerXTurn)
                            MakeRobotMove();
                    }
                }
            }
        }

        private void Begin_restart_Click(object sender, RoutedEventArgs e)
        {
            BeginRestart();
           
            foreach (var button in mainGrid.Children.OfType<Button>())
            {
                button.IsEnabled = true;
            }
        }

        private bool CheckForWinner()
        {
           
            if (CheckLine(0, 1, 2) || CheckLine(3, 4, 5) || CheckLine(6, 7, 8) ||
                CheckLine(0, 3, 6) || CheckLine(1, 4, 7) || CheckLine(2, 5, 8) ||
                CheckLine(0, 4, 8) || CheckLine(2, 4, 6))
            {
                return true;
            }

            return false;
        }

        private void DisplayWinner()
        {
            InfoTextBlock.Text = (isPlayerXTurn ? "Крестиков" : "Ноликов") + " победа";
            Game = false;
        }

        private void DisplayTie()
        {
            InfoTextBlock.Text = "Ничья!";
            Game = false;
        }

        private void MakeRobotMove()
        {
            List<Button> emptyButtons = new List<Button>();

            foreach (var child in mainGrid.Children)
            {
                if (child is Button button && string.IsNullOrEmpty(button.Content.ToString()))
                {
                    emptyButtons.Add(button);
                }
            }

            if (emptyButtons.Count > 0)
            {
                Button randomButton = emptyButtons[new Random().Next(emptyButtons.Count)];
                randomButton.Content = "O";

                if (CheckForWinner())
                {
                    DisplayWinner();
                }
                else if (IsBoardFull())
                {
                    DisplayTie();
                }
                else
                {
                    isPlayerXTurn = true;
                }
            }
        }

        private bool CheckLine(int index1, int index2, int index3)
        {
            Button[] buttons = mainGrid.Children.OfType<Button>().ToArray();
            return buttons[index1].Content.ToString() == buttons[index2].Content.ToString() &&
                   buttons[index2].Content.ToString() == buttons[index3].Content.ToString() &&
                   !string.IsNullOrEmpty(buttons[index1].Content.ToString());
        }

        private bool IsBoardFull()
        {
            foreach (var child in mainGrid.Children)
            {
                if (child is Button button && string.IsNullOrEmpty(button.Content.ToString()))
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}