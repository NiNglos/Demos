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
    /// Логика взаимодействия для Test2.xaml
    /// </summary>
    public partial class Test2 : Window
    {

        public string answer;

        public Test2()
        {
            InitializeComponent();
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_1";
            command.Connection = myConnection;
            myConnection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int i=rand.Next(1,count+1);
            command.CommandText = $"Select * From Question_1 Where id_question = {i}";
            reader = command.ExecuteReader();
            reader.Read();

            txtblQestion.Text = reader[1].ToString() + "\n(кол-во баллов за задание - 1 балл)";
            int answ = Convert.ToInt16(reader[2]);
            answer = reader[answ+2].ToString();
            txtbl1.Text = reader[3].ToString();
            txtbl2.Text = reader[4].ToString();
            txtbl3.Text = reader[5].ToString();
            txtbl4.Text = reader[6].ToString();
            reader.Close();
        }

        

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (rbt1.IsChecked == false && rbt2.IsChecked == false && rbt3.IsChecked == false && rbt4.IsChecked == false)
            {
                MessageBox.Show("Выберите вариант ответа!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MainWindow.answers[1, 0] = txtblQestion.Text;
            string txt = "";
            if (rbt1.IsChecked == true) txt = txtbl1.Text;
            if (rbt2.IsChecked == true) txt = txtbl2.Text;
            if (rbt3.IsChecked == true) txt = txtbl3.Text;
            if (rbt4.IsChecked == true) txt = txtbl4.Text;
            MainWindow.answers[1, 1] = txt;
            if (answer == txt)
            { MainWindow.answers[1, 2] = "1"; }
            else MainWindow.answers[1, 2] = "0";
            this.Hide();
            Test3 test3 = new Test3();
            test3.Show();
        }
    }
    
    
}
