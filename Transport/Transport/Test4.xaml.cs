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
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;

namespace Transport
{
    /// <summary>
    /// Логика взаимодействия для Test3.xaml
    /// </summary>
    public partial class Test4 : Window
    {
        public string answ="";

        public Test4()
        {
            InitializeComponent();
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_3";
            command.Connection = myConnection;
            myConnection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int i=rand.Next(1,count+1);
            command.CommandText = $"Select * From Question_3 Where id_question = {i}";
            reader = command.ExecuteReader();
            reader.Read();

            txtblQestion.Text = reader[1].ToString() + "\n(кол-во баллов за задание - 2 балла)";
            answ = reader[2].ToString();
            txtbl1.Text = reader[3].ToString();
            txtbl2.Text = reader[4].ToString();
            txtbl3.Text = reader[5].ToString();
            txtbl4.Text = reader[6].ToString();
            txtbl5.Text = reader[7].ToString();
            txtbl6.Text = reader[8].ToString();
            reader.Close();
        }

        
        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (chb1.IsChecked == false && chb2.IsChecked == false && chb3.IsChecked == false && chb4.IsChecked == false && chb5.IsChecked == false && chb6.IsChecked == false)
            {
                MessageBox.Show("Выберите варианты ответа!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MainWindow.answers[3, 0] = txtblQestion.Text;
            string answer = "", text = "";
            if (chb1.IsChecked == true)
            {
                answer = answer + "1; ";
                text = text + txtbl1.Text + "\n";
            }
            if (chb2.IsChecked == true)
            {
                answer = answer + "2; ";
                text = text + txtbl2.Text + "\n";
            }
            if (chb3.IsChecked == true)
            {
                answer = answer + "3; ";
                text = text + txtbl3.Text + "\n";
            }
            if (chb4.IsChecked == true)
            {
                answer = answer + "4; ";
                text = text + txtbl4.Text + "\n";
            }
            if (chb5.IsChecked == true)
            {
                answer = answer + "5; ";
                text = text + txtbl5.Text + "\n";
            }
            if (chb6.IsChecked == true)
            {
                answer = answer + "6; ";
                text = text + txtbl6.Text + "\n";
            }
            answer = answer.Remove(answer.Length-2);
            MainWindow.answers[3, 1] = text;
            if (answer == answ) MainWindow.answers[3, 2] = "2";
            else MainWindow.answers[3, 2] = "0";
            this.Hide();
            Test5 test5 = new Test5();
            test5.Show();
        }
    }
    
    
}
