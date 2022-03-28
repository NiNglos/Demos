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
    public partial class Test6 : Window
    {
        public Test6()
        {
            InitializeComponent();
            dt_q_1.Clear();

            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_5_6";
            command.Connection = myConnection;
            myConnection.Open();

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int r = rand.Next(1, count + 1);
            command.CommandText = $"Select table_name From Question_5_6 Where id_question = {r}";
            reader = command.ExecuteReader();
            reader.Read();
            command.CommandText = $"Select * From {reader[0].ToString()}";
            reader.Close();

            dt_q_1.Load(command.ExecuteReader());
          
           
            int i, j, sum1 = 0, sum2 = 0, n = dt_q_1.Rows.Count, m = dt_q_1.Columns.Count;
            int[,] a = new int[n, m];
            for (i = 0; i < n; i++)
            {
                DataRow row = dt_q_1.Rows[i];
                for (j = 0; j < m; j++)
                {
                    a[i, j] = (int)row[j];
                }
            }

            for (i = 0; i < n - 1; i++)
                sum1 += a[i, m - 1];
            for (j = 0; j < m - 1; j++)
                sum2 += a[n - 1, j];
            if (sum1 >= sum2) cost = sum1 - sum2;
            else cost = sum2 - sum1;
            if (cost == 0) answ = "да";
            else answ = "нет";
        }
        public string answ;
        public int cost;
        static public DataTable dt_q_1 = new DataTable();

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmb.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите вариант ответа!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtbl.Text == "")
            {
                MessageBox.Show("Вы не заполнили стоимость перевозки!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MainWindow.answers[5, 0] = "Сбалансирована ли данная транспортная задача?\n(количествово баллов за задание - 2 балла)\nЕсли задача не сбалансирована, напишите какое количество товара надо назначить фиктивному поставщику/потребителю.";
            MainWindow.answers[5, 2] = cmb.Text;
            MainWindow.answers[5, 3] = txtbl.Text;
            if (cmb.Text == answ) MainWindow.answers[5, 4] = "1";
            else MainWindow.answers[5, 4] = "0";
            if (Convert.ToInt16(txtbl.Text) == cost) MainWindow.answers[5, 5] = "1";
            else MainWindow.answers[5, 5] = "0";
            this.Hide();
            TestReport test = new TestReport();
            test.Show();
        }

        private void txtbl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }
    }
    
    
}
