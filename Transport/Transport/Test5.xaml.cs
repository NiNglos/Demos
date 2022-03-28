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
    public partial class Test5 : Window
    {
        public Test5()
        {
            InitializeComponent();
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_4";
            command.Connection = myConnection;
            myConnection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int i=rand.Next(1,count+1);
            command.CommandText = $"Select * From Question_4 Where id_question = {i}";
            reader = command.ExecuteReader();
            reader.Read();
            answer = reader[2].ToString();
            txtblQestion.Text = reader[1].ToString() + "\n(кол-во баллов за задание - 3 балла)";
            reader.Close();
        }

        public string answer, answ;

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtbox.Text == "")
            {
                MessageBox.Show("Вы не ответили на вопрос!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MainWindow.answers[4, 0] = txtblQestion.Text;
            answ = txtbox.Text;
            MainWindow.answers[4, 1] = answ;
            if (answer.ToLower().IndexOf(answ.ToLower()) == -1) MainWindow.answers[4, 2] = "0";
            else MainWindow.answers[4, 2] = "3";
            this.Hide();
            Test6 test6 = new Test6();
            test6.Show();
        }
    }
    
    
}
