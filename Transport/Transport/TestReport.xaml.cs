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
    public partial class TestReport : Window
    {
        public TestReport()
        {
            InitializeComponent();

            int point = 0;
            point = Convert.ToInt16(MainWindow.answers[1, 2]) + Convert.ToInt16(MainWindow.answers[2, 2]) + Convert.ToInt16(MainWindow.answers[3, 2]) + Convert.ToInt16(MainWindow.answers[4, 2]) + Convert.ToInt16(MainWindow.answers[5, 4]) + Convert.ToInt16(MainWindow.answers[5, 5]) + Convert.ToInt16(MainWindow.answers[6, 1]);
            txtFIO.Text = "Студент: " + MainWindow.answers[0, 0] + "\nГруппа: " + MainWindow.answers[0, 1]  + "\nКоличество баллов: " + point;

            

            txtQuestions.Text += "\n\nЗадание 1\n\n" + MainWindow.answers[1, 0];
            if (MainWindow.answers[1, 2] == "1") txtQuestions.Text += "\n\nДан верный ответ:\n" + MainWindow.answers[1, 1];
            else txtQuestions.Text += "\n\nДан неверный ответ:\n" + MainWindow.answers[1, 1];

            txtQuestions.Text += "\n\nЗадание 2\n\n" + MainWindow.answers[2, 0];
            if (MainWindow.answers[2, 2] == "1") txtQuestions.Text += "\n\nДан верный ответ:\n" + MainWindow.answers[2, 1];
            else txtQuestions.Text += "\n\nДан неверный ответ:\n" + MainWindow.answers[2, 1];

            txtQuestions.Text += "\n\nЗадание 3\n\n" + MainWindow.answers[3, 0];
            if (MainWindow.answers[3, 2] == "2") txtQuestions.Text += "\n\nДан верный ответ:\n" + MainWindow.answers[3, 1];
            else txtQuestions.Text += "\n\nДан неверный ответ:\n" + MainWindow.answers[3, 1];

            txtQuestions.Text += "\n\nЗадание 4\n\n" + MainWindow.answers[4, 0];
            if (MainWindow.answers[4, 2] == "3") txtQuestions.Text += "\n\nДан верный ответ:\n" + MainWindow.answers[4, 1];
            else txtQuestions.Text += "\n\nДан неверный ответ:\n" + MainWindow.answers[4, 1];

            txtQuestions.Text += "\n\nЗадание 5\n\n" + MainWindow.answers[5, 0];
        
            if (MainWindow.answers[5, 4] == "1") txtQuestion_5.Text += "Дан верный ответ:\n" + MainWindow.answers[5, 2];
            else txtQuestion_5.Text += "Дан неверный ответ:\n" + MainWindow.answers[5, 2];
            if (MainWindow.answers[5, 5] == "1") txtQuestion_5.Text += "\n\nВерно расчитано кол-во ресурсов:\n" + MainWindow.answers[5, 3];
            else txtQuestion_5.Text += "\n\nНеверно расчитано кол-во ресурсов:\n" + MainWindow.answers[5, 3];
            int mark = 0;
            if (point < 10)
            {
                txtMark.Text = "2 (неудовлетворительно)";
                mark = 2;
            }
            if (point > 10 && point < 15)
            {
                txtMark.Text = "3 (удовлетворительно)";
                mark = 3;
            }
            if (point > 15 && point < 20)
            {
                txtMark.Text = "4 (хорошо)";
                mark = 4;
            }
            if (point > 20)
            {
                txtMark.Text = "5 (отлично)";
                mark = 5;
            }

            DateTime date = DateTime.Now;
            string format = "dd.mm.yyyy hh:mm:ss";

            OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
            OleDbCommand command = new OleDbCommand();
            command.CommandText = $"INSERT INTO Student (FIO, [group], mark, point) VALUES ('{MainWindow.answers[0, 0]}','{MainWindow.answers[0, 1]}', {mark}, {point})";
            command.Connection = myConnection;
            myConnection.Open();
            command.ExecuteNonQuery();
            myConnection.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Application.Current.MainWindow.Show();
        }

    }
    
    
}
