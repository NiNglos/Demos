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
    public partial class Test7 : Window
    {
        public Test7()
        {
            InitializeComponent();

            dt_a_1.Clear();
            dt_q_1.Clear();
            dt_a_1.Columns.Clear();
            dt_a_1.Rows.Clear();

            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_5_6";
            command.Connection = myConnection;
            myConnection.Open();

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int r=rand.Next(1,count+1);
            command.CommandText = $"Select table_name From Question_5_6 Where id_question = {r}";
            reader = command.ExecuteReader();
            reader.Read();
            command.CommandText = $"Select * From {reader[0].ToString()}";
            reader.Close();

            dt_q_1.Load(command.ExecuteReader());
            gridExample.AutoGenerateColumns = true;
            gridExample.ItemsSource = dt_q_1.AsDataView();

            n = dt_q_1.Rows.Count;
            nn = n;
            m = dt_q_1.Columns.Count;
            mm = m;
            
      
            for (int j = 0; j < m; j++)
                dt_a_1.Columns.Add();
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt_a_1.NewRow();
                dt_a_1.Rows.Add(row);
            }
            gridAnswer.AutoGenerateColumns = true;
            gridAnswer.ItemsSource = dt_a_1.AsDataView();

        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");

        static public int n, m, nn, mm;
        static public DataTable dt_q_1 = new DataTable();
        static public DataTable dt_a_1 = new DataTable();



        //При нажатии кнопки Добавить строку
        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = dt_a_1.NewRow();
            dt_a_1.Rows.Add(row);
            nn++;
            
        }

        //При нажатии копки Удалить строку
        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (nn == 2)
            {
                MessageBox.Show("Вы не можете сделать количество строк меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt_a_1.Rows.RemoveAt(nn-1);
            nn--;
            
        }
        
        //При нажатии кнопки Добавить столбец
        private void btnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            dt_a_1.Columns.Add();
            mm++;
            gridAnswer.ItemsSource = dt_a_1.AsDataView();
        }



        private void gridAnswer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }


        //При нажатии кнопки Удалить столбец
        private void btnDeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if (mm == 2)
            {
                MessageBox.Show("Вы не можете сделать количество столбцов меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt_a_1.Columns.RemoveAt(mm-1);
            mm--;
            gridAnswer.ItemsSource = dt_a_1.AsDataView();
        }
        
        //При нажатии кнопки Продолжить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Transport.MainWindow.answers[6, 0] = "Сбалансируйте транспортную задачу.\n(количествово баллов за задание - 4 балла)";
            Optimize();
            this.Hide();
            Test8 test8 = new Test8();
            test8.Show();
        }

        public void Optimize()
        {
            int i, j, sum1 = 0, sum2 = 0, n1 = n, m1 = m;
            int[,] a = new int[n, m];
            for (i = 0; i < n; i++)
            {
                DataRow row = dt_q_1.Rows[i];
                for (j = 0; j < m; j++)
                {
                    a[i, j] = (int)row[j];
                }
            }
            
            for (i = 0; i < n1 - 1; i++)
                sum1 += a[i, m1 - 1];
            for (j = 0; j < m1 - 1; j++)
                sum2 += a[n1 - 1, j];
            if (sum1 < sum2)
            {
                int[,] b = new int[n1 + 1, m1];
                for (i = 0; i < n1; i++)
                    for (j = 0; j < m1; j++)
                        if (i == n1 - 1)
                        {
                            b[i + 1, j] = a[i, j];
                            b[i, j] = 0;
                        }
                        else b[i, j] = a[i, j];
                b[n1 - 1, m1 - 1] = sum2 - sum1;
                n1++;
                if (dt_a_1.Rows.Count != n1 || dt_a_1.Columns.Count != m1) MainWindow.answers[6, 1] = "0";
                else
                {
                    int[,] c = new int[n1,m1];
                    for (i = 0; i < n1; i++)
                    {
                        DataRow row = dt_a_1.Rows[i];
                        for (j = 0; j < m1; j++)
                        {
                            if (i == n1 - 1 && j == m1 - 1) row[j] = 0;
                            c[i, j] = Convert.ToInt16(row[j]);
                        }
                    }
                    int kol = 0;
                    for (i = 0; i < n1; i++)
                        for (j = 0; j < m1; j++)
                            if (c[i,j] != b[i,j]) { kol++; break; }
                    if (kol==0) MainWindow.answers[6, 1] = "4";
                    else MainWindow.answers[6, 1] = "0";
                }
            }
            if (sum1 > sum2)
            {
                int[,] b = new int[n1, m1 + 1];
                for (j = 0; j < m1; j++)
                    for (i = 0; i < n1; i++)
                        if (j == m1 - 1)
                        {
                            b[i, j + 1] = a[i, j];
                            b[i, j] = 0;
                        }
                        else b[i, j] = a[i, j];
                b[n1 - 1, m1 - 1] = sum1 - sum2;
                m1++;
                if (dt_a_1.Rows.Count != n1 || dt_a_1.Columns.Count != m1) MainWindow.answers[6, 1] = "0";
                else
                {
                    int[,] c = new int[n1, m1];
                    for (i = 0; i < n1; i++)
                    {
                        DataRow row = dt_a_1.Rows[i];
                        for (j = 0; j < m1; j++)
                        {
                            if (i == n1 - 1 & j == m1 - 1) row[j] = 0;
                            c[i, j] = Convert.ToInt16(row[j]);
                        }
                    } 
                    int kol = 0;
                    for (i = 0; i < n1; i++)
                        for (j = 0; j < m1; j++)
                            if (c[i, j] != b[i, j]) { kol++; break; }
                    if (kol == 0) MainWindow.answers[6, 1] = "4";
                    else MainWindow.answers[6, 1] = "0";
                }
            }


        }

    }


}
