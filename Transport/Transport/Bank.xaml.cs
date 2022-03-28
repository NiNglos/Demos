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
    /// Логика взаимодействия для Bank.xaml
    /// </summary>
    public partial class Bank : Window
    {
        public Bank()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void btnEx_1_2_Click(object sender, RoutedEventArgs e)
        {
            closes();
            gridEx_1_2.Visibility = Visibility.Visible;
        }

        private void btnEx_3_Click(object sender, RoutedEventArgs e)
        {
            closes();
            gridEx_3.Visibility = Visibility.Visible;
        }

        private void btnEx_4_Click(object sender, RoutedEventArgs e)
        {
            closes();
            gridEx_4.Visibility = Visibility.Visible;
        }

        private void btnEx_5_6_Click(object sender, RoutedEventArgs e)
        {
            closes();
            gridEx_5_6.Visibility = Visibility.Visible;
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            n = 4; m = 4;
            for (int j = 0; j < m; j++)
                dt.Columns.Add();
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
            }
            gridTable_5_6.AutoGenerateColumns = true;
            gridTable_5_6.ItemsSource = dt.AsDataView();
        }

        private void btnEx_7_8_Click(object sender, RoutedEventArgs e)
        {
            closes();
            gridEx_7_8.Visibility = Visibility.Visible;
            dt.Clear();
            dt.Columns.Clear();
            dt.Rows.Clear();
            n = 4;m = 4;
            for (int j = 0; j < m; j++)
                dt.Columns.Add();
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
            }
            gridTable_7_8.AutoGenerateColumns = true;
            gridTable_7_8.ItemsSource = dt.AsDataView();
        }
        
        private void closes()
        {
            gridEx_1_2.Visibility = Visibility.Collapsed;
            gridEx_3.Visibility = Visibility.Collapsed;
            gridEx_4.Visibility = Visibility.Collapsed;
            gridEx_5_6.Visibility = Visibility.Collapsed;
            gridEx_7_8.Visibility = Visibility.Collapsed;
        }

        private void btnEx_1_2_OK_Click(object sender, RoutedEventArgs e)
        {
            if (txtEx_1_2_Text.Text == "" || txtEx_1_2_Answer_1.Text == "" || txtEx_1_2_Answer_2.Text == "" || txtEx_1_2_Answer_3.Text == "" || txtEx_1_2_Answer_4.Text == "" || cmbEx_1_2_Topic.SelectedIndex < 0 || cmbEx_1_2_CorrectAnswer.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            OleDbCommand command = new OleDbCommand();
            if (cmbEx_1_2_Topic.SelectedIndex == 0) command.CommandText = $"INSERT INTO Question_1 (text_question, correct_answer, answer_1, answer_2, answer_3, answer_4) VALUES ( '{txtEx_1_2_Text.Text}', '{cmbEx_1_2_CorrectAnswer.SelectedIndex + 1}', '{txtEx_1_2_Answer_1.Text}', '{txtEx_1_2_Answer_2.Text}', '{txtEx_1_2_Answer_3.Text}', '{txtEx_1_2_Answer_4.Text}')";
            else command.CommandText = command.CommandText = $"INSERT INTO Question_2 (text_question, correct_answer, answer_1, answer_2, answer_3, answer_4) VALUES ( '{txtEx_1_2_Text.Text}', {cmbEx_1_2_CorrectAnswer.SelectedIndex + 1}, '{txtEx_1_2_Answer_1.Text}', '{txtEx_1_2_Answer_2.Text}', '{txtEx_1_2_Answer_3.Text}', '{txtEx_1_2_Answer_4.Text}')";
            command.Connection = myConnection;
            myConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Вопрос успешно добавлен в базу данных!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

            myConnection.Close();
        }
        private void btnEx_3_OK_Click(object sender, RoutedEventArgs e)
        {
            string txt = "";
            if (chk_1.IsChecked == true) txt += "1; ";
            if (chk_2.IsChecked == true) txt += "2; ";
            if (chk_3.IsChecked == true) txt += "3; ";
            if (chk_4.IsChecked == true) txt += "4; ";
            if (chk_5.IsChecked == true) txt += "5; ";
            if (chk_6.IsChecked == true) txt += "6; ";
            txt = txt.Remove(txt.Length - 2);
            if (txtEx_3_Text.Text == "" || txtEx_3_Answer_1.Text == "" || txtEx_3_Answer_2.Text == "" || txtEx_3_Answer_3.Text == "" || txtEx_3_Answer_4.Text == "" || txtEx_3_Answer_5.Text == "" || txtEx_3_Answer_6.Text == "" || txt == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.CommandText = $"INSERT INTO Question_3 (text_question, correct_answers, answer_1, answer_2, answer_3, answer_4, answer_5, answer_6) VALUES ( '{txtEx_3_Text.Text}', '{txt}', '{txtEx_3_Answer_1.Text}', '{txtEx_3_Answer_2.Text}', '{txtEx_3_Answer_3.Text}', '{txtEx_3_Answer_4.Text}', '{txtEx_3_Answer_5.Text}', '{txtEx_3_Answer_6.Text}')";
            command.Connection = myConnection;
            myConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Вопрос успешно добавлен в базу данных!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

            myConnection.Close();
        }


        private void btnEx_4_OK_Click(object sender, RoutedEventArgs e)
        {
            if (txtEx_4_Text.Text == "" || txtEx_4_Answer.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.CommandText = $"INSERT INTO Question_4 (text_question, correct_answer) VALUES ( '{txtEx_4_Text.Text}', '{txtEx_4_Answer.Text}')";
            command.Connection = myConnection;
            myConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Вопрос успешно добавлен в базу данных!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

            myConnection.Close();
        }

        private void btnEx_5_OK_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            DataRow row = dt.Rows[n-1];
            row[m - 1] = 0;
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt.Rows[i];
                for (int j = 0; j < m; j++)
                    if (rows[j].ToString() == "") k++;
            }
            if (k>0)
            {
                MessageBox.Show("Вы не заполнили все ячейки!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_5_6";
            command.Connection = myConnection;
            myConnection.Open();

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();
            string columns = "";
            for (int j = 0; j < m; j++)
                columns += $"[{j+1}] INT, ";
            columns = columns.Remove(columns.Length - 2);
            command.CommandText = $"CREATE TABLE Q_5_6_{count+1} ({columns})";
            command.ExecuteNonQuery();

            string values = "";
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt.Rows[i];
                values = "";
                for (int j = 0; j < m; j++)
                    values += $"{rows[j].ToString()}, ";
                values = values.Remove(values.Length - 2);
                command.CommandText = $"INSERT INTO Q_5_6_{count + 1}  VALUES ({values})";
                command.ExecuteNonQuery();
            }

            command.CommandText = $"INSERT INTO Question_5_6 (table_name) VALUES ( 'Q_5_6_{count+1}')";
            command.ExecuteNonQuery();
            MessageBox.Show("Вопрос успешно добавлен в базу данных!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            myConnection.Close();
        }

        private void btnEx_7_8_OK_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            DataRow row = dt.Rows[n - 1];
            row[m - 1] = 0;
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt.Rows[i];
                for (int j = 0; j < m; j++)
                    if (rows[j].ToString() == "") k++;
            }
            if (k > 0)
            {
                MessageBox.Show("Вы не заполнили все ячейки!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Question_7_8";
            command.Connection = myConnection;
            myConnection.Open();

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();
            string columns = "";
            for (int j = 0; j < m; j++)
                columns += $"[{j + 1}] INT, ";
            columns = columns.Remove(columns.Length - 2);
            command.CommandText = $"CREATE TABLE Q_7_8_{count + 1} ({columns})";
            command.ExecuteNonQuery();

            string values = "";
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt.Rows[i];
                values = "";
                for (int j = 0; j < m; j++)
                    values += $"{rows[j].ToString()}, ";
                values = values.Remove(values.Length - 2);
                command.CommandText = $"INSERT INTO Q_7_8_{count + 1}  VALUES ({values})";
                command.ExecuteNonQuery();
            }

            command.CommandText = $"INSERT INTO Question_7_8 (table_name) VALUES ( 'Q_7_8_{count + 1}')";
            command.ExecuteNonQuery();
            MessageBox.Show("Вопрос успешно добавлен в базу данных!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            myConnection.Close();
        }


        static public int n=4, m=4;
        static public DataTable dt = new DataTable();



        //При нажатии кнопки Добавить строку
        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            n++;

        }

        //При нажатии копки Удалить строку
        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (n == 2)
            {
                MessageBox.Show("Вы не можете сделать количество строк меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt.Rows.RemoveAt(n - 1);
            n--;

        }

        //При нажатии кнопки Добавить столбец
        private void btnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            dt.Columns.Add();
            m++;
            gridTable_5_6.ItemsSource = dt.AsDataView();
        }



        private void gridAnswer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }


        //При нажатии кнопки Удалить столбец
        private void btnDeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if (m == 2)
            {
                MessageBox.Show("Вы не можете сделать количество столбцов меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt.Columns.RemoveAt(m - 1);
            m--;
            gridTable_5_6.ItemsSource = dt.AsDataView();
        }

        //При нажатии кнопки Добавить строку
        private void btnAddRow1_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            n++;

        }

        //При нажатии копки Удалить строку
        private void btnDeleteRow1_Click(object sender, RoutedEventArgs e)
        {
            if (n == 2)
            {
                MessageBox.Show("Вы не можете сделать количество строк меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt.Rows.RemoveAt(n - 1);
            n--;

        }

        //При нажатии кнопки Добавить столбец
        private void btnAddColumn1_Click(object sender, RoutedEventArgs e)
        {
            dt.Columns.Add();
            m++;
            gridTable_7_8.ItemsSource = dt.AsDataView();
        }



        //При нажатии кнопки Удалить столбец
        private void btnDeleteColumn1_Click(object sender, RoutedEventArgs e)
        {
            if (m == 2)
            {
                MessageBox.Show("Вы не можете сделать количество столбцов меньше двух!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            dt.Columns.RemoveAt(m - 1);
            m--;
            gridTable_7_8.ItemsSource = dt.AsDataView();
        }

        private void cmbEx_1_2_Topic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
    }
}
