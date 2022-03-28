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
    /// Логика взаимодействия для Practice_Rasp.xaml
    /// </summary>
    public partial class Practice_Pot : Window
    {
        public Practice_Pot()
        {
            InitializeComponent();

            OleDbCommand command = new OleDbCommand();
            command.CommandText = $"Select * From P_1";
            command.Connection = myConnection;
            myConnection.Open();
            dt_question.Load(command.ExecuteReader());
            gridExample.AutoGenerateColumns = true;
            gridExample.ItemsSource = dt_question.AsDataView();


            n = dt_question.Rows.Count;
            m = dt_question.Columns.Count;

            for (int j = 0; j < m; j++)
                dt_answer.Columns.Add();
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt_answer.NewRow();
                dt_answer.Rows.Add(row);
            }
            for (int i = 0; i < n; i++)
            {
                DataRow row_q = dt_question.Rows[i];
                DataRow row_a = dt_answer.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    if (i == n - 1 || j == m - 1) row_a[j] = row_q[j];
                }
            }
            gridAnswer.AutoGenerateColumns = true;
            gridAnswer.ItemsSource = dt_answer.AsDataView();


            int[,] a = new int[n, m];
            int[,] d = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt_question.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    a[i, j] = (int)rows[j];
                }
            }


            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if ((j < m - 1) && (i < n - 1))
                        d[i, j] = 0;
                    else d[i, j] = a[i, j];
            int min = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (d[i, m - 1] > 0 && d[n - 1, j] > 0)
                    {
                        min = Math.Min(d[i, m - 1], d[n - 1, j]);
                        d[i, j] = min;
                        d[i, m - 1] -= min;
                        d[n - 1, j] -= min;
                    }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if ((j == m - 1) || (i == n - 1))
                        d[i, j] = a[i, j];

            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt_answer.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    if (d[i, j] == 0) rows[j] = "";
                    else rows[j] = d[i, j];
                }
            }
        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
        public DataTable dt_question = new DataTable();
        public DataTable dt_answer = new DataTable();
        public DataTable dt_optimal = new DataTable();
        static public int n, m;
        int sum = 257;

        private void gridAnswer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void grid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(e.Text == "-" || char.IsDigit(e.Text, 0));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void HplStep1Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep1Theory.Visibility = Visibility.Visible;
        }

        private void HplStep2_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            //gridStep2_2_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep3_Theory_Click(object sender, RoutedEventArgs e)
        {
            //gridStep3_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep1_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            //gridStep1_2_Theory.Visibility = Visibility.Visible;
        }

        private void btnStep1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_a_1_1.Text == "" || txt_a_1_2.Text == "" || txt_a_1_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_a_2_1.Text == "" || txt_a_2_2.Text == "" || txt_a_2_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_a_3_1.Text == "" || txt_a_3_2.Text == "" || txt_a_3_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_a_4_1.Text == "" || txt_a_4_2.Text == "" || txt_a_4_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_a_5_1.Text == "" || txt_a_5_2.Text == "" || txt_a_5_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_a_6_1.Text == "" || txt_a_6_2.Text == "" || txt_a_6_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt_a_1_1.Text + txt_a_1_2.Text + txt_a_1_3.Text != "404")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал первого столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_2_1.Text + txt_a_2_2.Text + txt_a_2_3.Text != "945")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал второй строки.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_3_1.Text + txt_a_3_2.Text + txt_a_3_3.Text != "15-4")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал второго столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_4_1.Text + txt_a_4_2.Text + txt_a_4_3.Text != "8-412")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третьей строки.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_5_1.Text + txt_a_5_2.Text + txt_a_5_3.Text != "512-7")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал третьего столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_6_1.Text + txt_a_6_2.Text + txt_a_6_3.Text != "712-5")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал четвертого столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridStep1_Algebra.Visibility = Visibility.Visible;
                btnStep1_1Ok.Visibility = Visibility.Visible;
                btnStep1Ok.Visibility = Visibility.Collapsed;
            }

        }

        private void btnStep1_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_1_2.Text == ""  || txt_1_3.Text == "" || txt_1_4.Text == "" || txt_1_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2_2.Text == "" || txt_2_3.Text == "" ||  txt_2_4.Text == "" || txt_2_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_3_2.Text == "" || txt_3_3.Text == "" || txt_3_4.Text == "" || txt_3_5.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_4_2.Text == "" || txt_4_3.Text == "" || txt_4_4.Text == "" || txt_4_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_5_2.Text == "" || txt_5_3.Text == "" || txt_5_4.Text == "" || txt_5_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_6_2.Text == "" || txt_6_3.Text == "" || txt_6_4.Text == "" || txt_6_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt_1_2.Text + txt_1_3.Text + txt_1_4.Text + txt_1_5.Text != "0-42-6")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_2_2.Text + txt_2_3.Text + txt_2_4.Text + txt_2_5.Text != "0-76-13")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if ( txt_3_2.Text + txt_3_3.Text + txt_3_4.Text + txt_3_5.Text != "0-52-7")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_4_2.Text + txt_4_3.Text + txt_4_4.Text + txt_4_5.Text != "5-713-15")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_5_2.Text + txt_5_3.Text + txt_5_4.Text + txt_5_5.Text != "5-53-3")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if ( txt_6_2.Text + txt_6_3.Text + txt_6_4.Text + txt_6_5.Text != "124115")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridStep1_Kontur.Visibility = Visibility.Collapsed;
                btnStep1_1Ok.Visibility = Visibility.Collapsed;
                gridStep1_End.Visibility = Visibility.Visible;
                btnStep1_2Ok.Visibility = Visibility.Visible;
            }

        }

        private void btnStep1_2Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtOtr_1.Text == "" || txtOtr_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtOtr_1.Text != "3" || txtOtr_2.Text != "1")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep1.Visibility = Visibility.Collapsed;
                    txtStep2.Visibility = Visibility.Visible;
                }


            }

        }

        private void HplStep2_Theory_Click_1(object sender, RoutedEventArgs e)
        {
            gridStep2_Theory.Visibility = Visibility.Visible;
        }

        private void btnStep2_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_1_1_1.Text == "" || txt_1_1_2.Text == "" || txt_1_2_1.Text == "" || txt_1_2_2.Text == "" || txt_1_3_1.Text == "" || txt_1_3_2.Text == "" || txt_1_4_1.Text == "" || txt_1_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (txt_1_1_1.Text + txt_1_1_2.Text + txt_1_2_1.Text + txt_1_2_2.Text + txt_1_3_1.Text + txt_1_3_2.Text + txt_1_4_1.Text + txt_1_4_2.Text != "31212232")
                if (txt_1_1_1.Text + txt_1_1_2.Text + txt_1_2_1.Text + txt_1_2_2.Text + txt_1_3_1.Text + txt_1_3_2.Text + txt_1_4_1.Text + txt_1_4_2.Text != "31322221")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                btnStep2_Ok.Visibility = Visibility.Collapsed;
                gridS2_2.Visibility = Visibility.Visible;
                btnStep2_2Ok.Visibility = Visibility.Visible;
            }
        }

        private void btnStep2_2Ok_Click(object sender, RoutedEventArgs e)
        {

            if (txt2_a_1_1.Text == "" || txt2_a_1_2.Text == "" || txt2_a_1_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_a_2_1.Text == "" || txt2_a_2_2.Text == "" || txt2_a_2_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_a_3_1.Text == "" || txt2_a_3_2.Text == "" || txt2_a_3_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_a_4_1.Text == "" || txt2_a_4_2.Text == "" || txt2_a_4_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt2_a_1_1.Text + txt2_a_1_2.Text + txt2_a_1_3.Text != "0-11")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_2_1.Text + txt2_a_2_2.Text + txt2_a_2_3.Text != "-1-10")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_3_1.Text + txt2_a_3_2.Text + txt2_a_3_3.Text != "18-119")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_4_1.Text + txt2_a_4_2.Text + txt2_a_4_3.Text != "-3-1-2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep2.Visibility = Visibility.Collapsed;
                DataRow rows = dt_answer.Rows[1];
                rows[0] = "";
                rows[1] = 19;
                rows = dt_answer.Rows[2];
                rows[0] = 1;
                rows[1] = 2;
                txtStep2_1.Visibility = Visibility.Visible;
            }
        }

        private void btnStep2_1_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep2_1.Visibility = Visibility.Collapsed;
            txtStep3.Visibility = Visibility.Visible;
        }




        private void HplStep3Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep3Theory.Visibility = Visibility.Visible;
        }

        private void btnStep3Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_2a_1_1.Text == "" || txt_2a_1_2.Text == "" || txt_2a_1_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2a_2_1.Text == "" || txt_2a_2_2.Text == "" || txt_2a_2_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2a_3_1.Text == "" || txt_2a_3_2.Text == "" || txt_2a_3_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2a_4_1.Text == "" || txt_2a_4_2.Text == "" || txt_2a_4_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2a_5_1.Text == "" || txt_2a_5_2.Text == "" || txt_2a_5_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2a_6_1.Text == "" || txt_2a_6_2.Text == "" || txt_2a_6_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt_2a_1_1.Text + txt_2a_1_2.Text + txt_2a_1_3.Text != "404")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал первого столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_2a_2_1.Text + txt_2a_2_2.Text + txt_2a_2_3.Text != "1147")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал второй строки.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_2a_3_1.Text + txt_2a_3_2.Text + txt_2a_3_3.Text != "871")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал второго столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_2a_4_1.Text + txt_2a_4_2.Text + txt_2a_4_3.Text != "110")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третьей строки.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_2a_5_1.Text + txt_2a_5_2.Text + txt_2a_5_3.Text != "57-2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал третьего столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_2a_6_1.Text + txt_2a_6_2.Text + txt_2a_6_3.Text != "770")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте потенциал четвертого столбца.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridStep3_Algebra.Visibility = Visibility.Visible;
                btnStep3_1Ok.Visibility = Visibility.Visible;
                btnStep3Ok.Visibility = Visibility.Collapsed;
            }
        }

        private void btnStep3_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2_1_2.Text == "" || txt2_1_3.Text == "" || txt2_1_4.Text == "" || txt2_1_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_2_2.Text == "" || txt2_2_3.Text == "" || txt2_2_4.Text == "" || txt2_2_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_3_2.Text == "" || txt2_3_3.Text == "" || txt2_3_4.Text == "" || txt2_3_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_4_2.Text == "" || txt2_4_3.Text == "" || txt2_4_4.Text == "" || txt2_4_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_5_2.Text == "" || txt2_5_3.Text == "" || txt2_5_4.Text == "" || txt2_5_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_6_2.Text == "" || txt2_6_3.Text == "" || txt2_6_4.Text == "" || txt2_6_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt2_1_2.Text + txt2_1_3.Text + txt2_1_4.Text + txt2_1_5.Text != "012-1")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_2_2.Text + txt2_2_3.Text + txt2_2_4.Text + txt2_2_5.Text != "0-26-8")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_3_2.Text + txt2_3_3.Text + txt2_3_4.Text + txt2_3_5.Text != "002-2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_4_2.Text + txt2_4_3.Text + txt2_4_4.Text + txt2_4_5.Text != "049-5")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_5_2.Text + txt2_5_3.Text + txt2_5_4.Text + txt2_5_5.Text != "0-213-15")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_6_2.Text + txt2_6_3.Text + txt2_6_4.Text + txt2_6_5.Text != "003-3")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестую оценку.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep3.Visibility = Visibility.Collapsed;
                    txtStep6.Visibility = Visibility.Visible;
                }

        }

       

        private void btnStep6_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep7.Visibility = Visibility.Visible;
            txtStep6.Visibility = Visibility.Collapsed;
        }

        private void HplStep7_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep7_Theory.Visibility = Visibility.Visible;
        }

        private void btnStep7_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtS7.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Convert.ToInt16(txtS7.Text) == sum)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно и научились решать транспортную задачу методом потенциалов!", "Поздравляю", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    this.Close();
                    Application.Current.MainWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        private void HplStep2_Theory_Click(object sender, RoutedEventArgs e)
        {
            //gridStep2_Theory.Visibility = Visibility.Visible;
        }

    }
}
