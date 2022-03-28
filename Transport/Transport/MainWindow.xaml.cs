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

namespace Transport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    

   
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }


        public static string[,] answers = new string[9,6];

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridTest.Visibility = Visibility.Visible;
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridAbout.Visibility = Visibility.Visible;
        }

        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Test1 test1 = new Test1();
            test1.Show();
        }

        private void btnTheory_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridTheory.Visibility = Visibility.Visible;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridOpenClosed.Visibility = Visibility.Visible;
        }

        private void ControlGridClose()
        {
            gridTheory.Visibility = Visibility.Collapsed;
            gridTest.Visibility = Visibility.Collapsed;
            gridAbout.Visibility = Visibility.Collapsed;
            gridOpenClosed.Visibility = Visibility.Collapsed;
            gridAlgorithm.Visibility = Visibility.Collapsed;
            gridTransportProblem.Visibility = Visibility.Collapsed;
            gridMathModel.Visibility = Visibility.Collapsed;
            gridOptimalPlan.Visibility = Visibility.Collapsed;
            gridOptimalPlan.Visibility = Visibility.Collapsed;
            
            gridNorthWest.Visibility = Visibility.Collapsed;
            gridMinimum.Visibility = Visibility.Collapsed;
            gridDobrotnosti.Visibility = Visibility.Collapsed;
  
            gridPractice.Visibility = Visibility.Collapsed;
            gridMain.Visibility = Visibility.Collapsed;
        }

        private void HplAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridAlgorithm.Visibility = Visibility.Visible;
        }

        private void HplMathModel_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridMathModel.Visibility = Visibility.Visible;
        }

        private void HplTransport_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridTransportProblem.Visibility = Visibility.Visible;
        }

        private void HplOptimal_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridOptimalPlan.Visibility = Visibility.Visible;
        }

       

        private void HplNorthWest_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridNorthWest.Visibility = Visibility.Visible;
        }

        private void HplMinimal_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridMinimum.Visibility = Visibility.Visible;
        }

        private void HplDobrotnosti_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridDobrotnosti.Visibility = Visibility.Visible;
        }





        private void btnPractice_Click(object sender, RoutedEventArgs e)
        {
            ControlGridClose();
            gridPractice.Visibility = Visibility.Visible;
        }

        private void PracticeHplNorthWest_Click(object sender, RoutedEventArgs e)
        {
            Practice practice = new Practice();
            practice.Show();
        }

        private void PracticeHplMinimal_Click(object sender, RoutedEventArgs e)
        {
            Practice_Min practice = new Practice_Min();
            practice.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Enter enter = new Enter();
            enter.Show();
        }

        private void PracticeHplRaspredel_Click(object sender, RoutedEventArgs e)
        {
            Practice_Rasp practice_Rasp = new Practice_Rasp();
            practice_Rasp.Show();
        }

        private void PracticeHplPotenzial_Click(object sender, RoutedEventArgs e)
        {
            Practice_Pot practice_Pot = new Practice_Pot();
            practice_Pot.Show();
        }


    }

}
