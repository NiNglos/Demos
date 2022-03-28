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
    public partial class Practice_Rasp : Window
    {
        public Practice_Rasp()
        {
            InitializeComponent();

            OleDbCommand command = new OleDbCommand();
            command.CommandText = $"Select * From P_1";
            command.Connection = myConnection;
            myConnection.Open();
            dt_question.Load(command.ExecuteReader());
      

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


            int[,] indexx = new int[2, m + n];
            double min = a[0, 0];

            int imin = 0, jmin = 0, k = 0,x=0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if ((j == m - 1) || (i == n - 1))
                        d[i, j] = a[i, j];

            do
            {
                for (int i = 0; i < n - 1; i++)
                    if (d[i, m - 1] == 0) continue;
                    else
                        for (int j = 0; j < m - 1; j++)
                            if (d[n - 1, j] == 0) continue;
                            else
                            {
                                min = a[i, j];
                                imin = i;
                                jmin = j;
                                break;
                            }
                for (int i = 0; i < n - 1; i++)
                    if (d[i, m - 1] == 0) continue;
                    else
                        for (int j = 0; j < m - 1; j++)
                            if (d[n - 1, j] == 0) continue;
                            else
                            {
                                if (a[i, j] < min)
                                {
                                    min = a[i, j];
                                    imin = i;
                                    jmin = j;
                                }
                            }
                indexx[0, x] = imin;
                indexx[1, x] = jmin;
                if (d[n - 1, jmin] < d[imin, m - 1])
                {
                    d[imin, jmin] = d[n - 1, jmin];
                    d[imin, m - 1] -= d[n - 1, jmin];
                    d[n - 1, jmin] = 0;
                }
                else
                {
                    d[imin, jmin] = d[imin, m - 1];
                    d[n - 1, jmin] -= d[imin, m - 1];
                    d[imin, m - 1] = 0;
                }
                k = 0;
                for (int i = 0; i < n - 1; i++)
                    if (d[i, m - 1] != 0) k++;
                for (int j = 0; j < m - 1; j++)
                    if (d[n - 1, j] != 0) k++;
                x++;
            } while (k != 0);

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
            if (txt_1_1_1.Text == "" || txt_1_1_2.Text == "" || txt_1_2_1.Text == "" || txt_1_2_2.Text == "" || txt_1_3_1.Text == "" || txt_1_3_2.Text == "" || txt_1_4_1.Text == "" || txt_1_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2_1_1.Text == "" || txt_2_1_2.Text == "" || txt_2_2_1.Text == "" || txt_2_2_2.Text == "" || txt_2_3_1.Text == "" || txt_2_3_2.Text == "" || txt_2_4_1.Text == "" || txt_2_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_3_1_1.Text == "" || txt_3_1_2.Text == "" || txt_3_2_1.Text == "" || txt_3_2_2.Text == "" || txt_3_3_1.Text == "" || txt_3_3_2.Text == "" || txt_3_4_1.Text == "" || txt_3_4_2.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_4_1_1.Text == "" || txt_4_1_2.Text == "" || txt_4_2_1.Text == "" || txt_4_2_2.Text == "" || txt_4_3_1.Text == "" || txt_4_3_2.Text == "" || txt_4_4_1.Text == "" || txt_4_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_5_1_1.Text == "" || txt_5_1_2.Text == "" || txt_5_2_1.Text == "" || txt_5_2_2.Text == "" || txt_5_3_1.Text == "" || txt_5_3_2.Text == "" || txt_5_4_1.Text == "" || txt_5_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_6_1_1.Text == "" || txt_6_1_2.Text == "" || txt_6_2_1.Text == "" || txt_6_2_2.Text == "" || txt_6_3_1.Text == "" || txt_6_3_2.Text == "" || txt_6_4_1.Text == "" || txt_6_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt_1_1_1.Text + txt_1_1_2.Text + txt_1_2_1.Text + txt_1_2_2.Text + txt_1_3_1.Text + txt_1_3_2.Text + txt_1_4_1.Text + txt_1_4_2.Text != "11143431")
                if (txt_1_1_1.Text + txt_1_1_2.Text + txt_1_2_1.Text + txt_1_2_2.Text + txt_1_3_1.Text + txt_1_3_2.Text + txt_1_4_1.Text + txt_1_4_2.Text != "11313414")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_2_1_1.Text + txt_2_1_2.Text + txt_2_2_1.Text + txt_2_2_2.Text + txt_2_3_1.Text + txt_2_3_2.Text + txt_2_4_1.Text + txt_2_4_2.Text != "13143433")
                if (txt_2_1_1.Text + txt_2_1_2.Text + txt_2_2_1.Text + txt_2_2_2.Text + txt_2_3_1.Text + txt_2_3_2.Text + txt_2_4_1.Text + txt_2_4_2.Text != "13333414")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте второй контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_3_1_1.Text + txt_3_1_2.Text + txt_3_2_1.Text + txt_3_2_2.Text + txt_3_3_1.Text + txt_3_3_2.Text + txt_3_4_1.Text + txt_3_4_2.Text != "212212143431")
                if (txt_3_1_1.Text + txt_3_1_2.Text + txt_3_2_1.Text + txt_3_2_2.Text + txt_3_3_1.Text + txt_3_3_2.Text + txt_3_4_1.Text + txt_3_4_2.Text != "213134141222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третий контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_4_1_1.Text + txt_4_1_2.Text + txt_4_2_1.Text + txt_4_2_2.Text + txt_4_3_1.Text + txt_4_3_2.Text + txt_4_4_1.Text + txt_4_4_2.Text != "232212143433")
                if (txt_4_1_1.Text + txt_4_1_2.Text + txt_4_2_1.Text + txt_4_2_2.Text + txt_4_3_1.Text + txt_4_3_2.Text + txt_4_4_1.Text + txt_4_4_2.Text != "233334141222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_5_1_1.Text + txt_5_1_2.Text + txt_5_2_1.Text + txt_5_2_2.Text + txt_5_3_1.Text + txt_5_3_2.Text + txt_5_4_1.Text + txt_5_4_2.Text != "24221214")
                if (txt_5_1_1.Text + txt_5_1_2.Text + txt_5_2_1.Text + txt_5_2_2.Text + txt_5_3_1.Text + txt_5_3_2.Text + txt_5_4_1.Text + txt_5_4_2.Text != "24141222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_6_1_1.Text + txt_6_1_2.Text + txt_6_2_1.Text + txt_6_2_2.Text + txt_6_3_1.Text + txt_6_3_2.Text + txt_6_4_1.Text + txt_6_4_2.Text != "32341412")
                if (txt_6_1_1.Text + txt_6_1_2.Text + txt_6_2_1.Text + txt_6_2_2.Text + txt_6_3_1.Text + txt_6_3_2.Text + txt_6_4_1.Text + txt_6_4_2.Text != "32121434")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестой контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txt_1_1.Text == "" ||  txt_1_2.Text == ""  || txt_1_3.Text == "" || txt_1_4.Text == "" || txt_1_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_2_1.Text == ""  || txt_2_2.Text == "" || txt_2_3.Text == "" ||  txt_2_4.Text == "" || txt_2_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_3_1.Text == "" || txt_3_2.Text == "" || txt_3_3.Text == "" || txt_3_4.Text == "" || txt_3_5.Text == ""  || txt_3_6.Text == "" || txt_3_7.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_4_1.Text == "" || txt_4_2.Text == "" || txt_4_3.Text == "" || txt_4_4.Text == "" || txt_4_5.Text == "" || txt_4_6.Text == "" || txt_4_7.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_5_1.Text == "" || txt_5_2.Text == "" || txt_5_3.Text == "" || txt_5_4.Text == "" || txt_5_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt_6_1.Text == "" || txt_6_2.Text == "" || txt_6_3.Text == "" || txt_6_4.Text == "" || txt_6_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt_1_1.Text + txt_1_2.Text + txt_1_3.Text + txt_1_4.Text + txt_1_5.Text != "42711-2")
                if (txt_1_1.Text + txt_1_2.Text + txt_1_3.Text + txt_1_4.Text + txt_1_5.Text != "41172-2")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_2_1.Text + txt_2_2.Text + txt_2_3.Text + txt_2_4.Text + txt_2_5.Text != "62756")
                if (txt_2_1.Text + txt_2_2.Text + txt_2_3.Text + txt_2_4.Text + txt_2_5.Text != "65726")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_3_1.Text + txt_3_2.Text + txt_3_3.Text + txt_3_4.Text + txt_3_5.Text + txt_3_6.Text + txt_3_7.Text != "91227114")
                if (txt_3_1.Text + txt_3_2.Text + txt_3_3.Text + txt_3_4.Text + txt_3_5.Text + txt_3_6.Text + txt_3_7.Text != "91172214")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_4_1.Text + txt_4_2.Text + txt_4_3.Text + txt_4_4.Text + txt_4_5.Text + txt_4_6.Text + txt_4_7.Text != "131227514")
                if (txt_4_1.Text + txt_4_2.Text + txt_4_3.Text + txt_4_4.Text + txt_4_5.Text + txt_4_6.Text + txt_4_7.Text != "135722114")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_5_1.Text + txt_5_2.Text + txt_5_3.Text + txt_5_4.Text + txt_5_5.Text != "31222")
                if (txt_5_1.Text + txt_5_2.Text + txt_5_3.Text + txt_5_4.Text + txt_5_5.Text != "32212")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt_6_1.Text + txt_6_2.Text + txt_6_3.Text + txt_6_4.Text + txt_6_5.Text != "87221")
                if (txt_6_1.Text + txt_6_2.Text + txt_6_3.Text + txt_6_4.Text + txt_6_5.Text != "82271")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txtOtr.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtOtr.Text != "1")
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

            if (txt_a_1_1.Text + txt_a_1_2.Text + txt_a_1_3.Text != "0-1111")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_2_1.Text + txt_a_2_2.Text + txt_a_2_3.Text != "-11-110")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_3_1.Text + txt_a_3_2.Text + txt_a_3_3.Text != "6-1117")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt_a_4_1.Text + txt_a_4_2.Text + txt_a_4_3.Text != "-14-11-3")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep2.Visibility = Visibility.Collapsed;
                DataRow rows = dt_answer.Rows[0];
                rows[0] = 11;
                rows[3] = "";
                rows = dt_answer.Rows[2];
                rows[0] = 3;
                rows[3] = 17;
                txtStep2_1.Visibility = Visibility.Visible;
            }

        }

        private void HplStep3Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep3Theory.Visibility = Visibility.Visible;
        }

        private void btnStep2_1_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep2_1.Visibility = Visibility.Collapsed;
            txtStep3.Visibility = Visibility.Visible;
        }

        private void btnStep3Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2_1_1_1.Text == "" || txt2_1_1_2.Text == "" || txt2_1_2_1.Text == "" || txt2_1_2_2.Text == "" || txt2_1_3_1.Text == "" || txt2_1_3_2.Text == "" || txt2_1_4_1.Text == "" || txt2_1_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_2_1_1.Text == "" || txt2_2_1_2.Text == "" || txt2_2_2_1.Text == "" || txt2_2_2_2.Text == "" || txt2_2_3_1.Text == "" || txt2_2_3_2.Text == "" || txt2_2_4_1.Text == "" || txt2_2_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_3_1_1.Text == "" || txt2_3_1_2.Text == "" || txt2_3_2_1.Text == "" || txt2_3_2_2.Text == "" || txt2_3_3_1.Text == "" || txt2_3_3_2.Text == "" || txt2_3_4_1.Text == "" || txt2_3_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_4_1_1.Text == "" || txt2_4_1_2.Text == "" || txt2_4_2_1.Text == "" || txt2_4_2_2.Text == "" || txt2_4_3_1.Text == "" || txt2_4_3_2.Text == "" || txt2_4_4_1.Text == "" || txt2_4_4_2.Text == "" || txt2_4_5_1.Text == "" || txt2_4_5_2.Text == "" || txt2_4_6_1.Text == "" || txt2_4_6_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_5_1_1.Text == "" || txt2_5_1_2.Text == "" || txt2_5_2_1.Text == "" || txt2_5_2_2.Text == "" || txt2_5_3_1.Text == "" || txt2_5_3_2.Text == "" || txt2_5_4_1.Text == "" || txt2_5_4_2.Text == "" || txt2_5_5_1.Text == "" || txt2_5_5_2.Text == "" || txt2_5_6_1.Text == "" || txt2_5_6_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_6_1_1.Text == "" || txt2_6_1_2.Text == "" || txt2_6_2_1.Text == "" || txt2_6_2_2.Text == "" || txt2_6_3_1.Text == "" || txt2_6_3_2.Text == "" || txt2_6_4_1.Text == "" || txt2_6_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt2_1_1_1.Text + txt2_1_1_2.Text + txt2_1_2_1.Text + txt2_1_2_2.Text + txt2_1_3_1.Text + txt2_1_3_2.Text + txt2_1_4_1.Text + txt2_1_4_2.Text != "13113133")
                if (txt2_1_1_1.Text + txt2_1_1_2.Text + txt2_1_2_1.Text + txt2_1_2_2.Text + txt2_1_3_1.Text + txt2_1_3_2.Text + txt2_1_4_1.Text + txt2_1_4_2.Text != "13333111")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_2_1_1.Text + txt2_2_1_2.Text + txt2_2_2_1.Text + txt2_2_2_2.Text + txt2_2_3_1.Text + txt2_2_3_2.Text + txt2_2_4_1.Text + txt2_2_4_2.Text != "14113134")
                if (txt2_2_1_1.Text + txt2_2_1_2.Text + txt2_2_2_1.Text + txt2_2_2_2.Text + txt2_2_3_1.Text + txt2_2_3_2.Text + txt2_2_4_1.Text + txt2_2_4_2.Text != "14343111")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте второй контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_3_1_1.Text + txt2_3_1_2.Text + txt2_3_2_1.Text + txt2_3_2_2.Text + txt2_3_3_1.Text + txt2_3_3_2.Text + txt2_3_4_1.Text + txt2_3_4_2.Text  != "21221211")
                if (txt2_3_1_1.Text + txt2_3_1_2.Text + txt2_3_2_1.Text + txt2_3_2_2.Text + txt2_3_3_1.Text + txt2_3_3_2.Text + txt2_3_4_1.Text + txt2_3_4_2.Text  != "21111222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третий контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_4_1_1.Text + txt2_4_1_2.Text + txt2_4_2_1.Text + txt2_4_2_2.Text + txt2_4_3_1.Text + txt2_4_3_2.Text + txt2_4_4_1.Text + txt2_4_4_2.Text + txt2_4_5_1.Text + txt2_4_5_2.Text + txt2_4_6_1.Text + txt2_4_6_2.Text != "232212113133")
                if (txt2_4_1_1.Text + txt2_4_1_2.Text + txt2_4_2_1.Text + txt2_4_2_2.Text + txt2_4_3_1.Text + txt2_4_3_2.Text + txt2_4_4_1.Text + txt2_4_4_2.Text + txt2_4_5_1.Text + txt2_4_5_2.Text + txt2_4_6_1.Text + txt2_4_6_2.Text != "233331111222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_5_1_1.Text + txt2_5_1_2.Text + txt2_5_2_1.Text + txt2_5_2_2.Text + txt2_5_3_1.Text + txt2_5_3_2.Text + txt2_5_4_1.Text + txt2_5_4_2.Text + txt2_5_5_1.Text + txt2_5_5_2.Text + txt2_5_6_1.Text + txt2_5_6_2.Text != "242212113134")
                if (txt2_5_1_1.Text + txt2_5_1_2.Text + txt2_5_2_1.Text + txt2_5_2_2.Text + txt2_5_3_1.Text + txt2_5_3_2.Text + txt2_5_4_1.Text + txt2_5_4_2.Text + txt2_5_5_1.Text + txt2_5_5_2.Text + txt2_5_6_1.Text + txt2_5_6_2.Text != "243431111222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_6_1_1.Text + txt2_6_1_2.Text + txt2_6_2_1.Text + txt2_6_2_2.Text + txt2_6_3_1.Text + txt2_6_3_2.Text + txt2_6_4_1.Text + txt2_6_4_2.Text != "32311112")
                if (txt2_6_1_1.Text + txt2_6_1_2.Text + txt2_6_2_1.Text + txt2_6_2_2.Text + txt2_6_3_1.Text + txt2_6_3_2.Text + txt2_6_4_1.Text + txt2_6_4_2.Text != "32121131")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестой контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txt2_1_1.Text == "" || txt2_1_2.Text == "" || txt2_1_3.Text == "" || txt2_1_4.Text == "" || txt2_1_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_2_1.Text == "" || txt2_2_2.Text == "" || txt2_2_3.Text == "" || txt2_2_4.Text == "" || txt2_2_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_3_1.Text == "" || txt2_3_2.Text == "" || txt2_3_3.Text == "" || txt2_3_4.Text == "" || txt2_3_5.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_4_1.Text == "" || txt2_4_2.Text == "" || txt2_4_3.Text == "" || txt2_4_4.Text == "" || txt2_4_5.Text == "" || txt2_4_6.Text == "" || txt2_4_7.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_5_1.Text == "" || txt2_5_2.Text == "" || txt2_5_3.Text == "" || txt2_5_4.Text == "" || txt2_5_5.Text == "" || txt2_5_6.Text == "" || txt2_5_7.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2_6_1.Text == "" || txt2_6_2.Text == "" || txt2_6_3.Text == "" || txt2_6_4.Text == "" || txt2_6_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt2_1_1.Text + txt2_1_2.Text + txt2_1_3.Text + txt2_1_4.Text + txt2_1_5.Text != "641158")
                if (txt2_1_1.Text + txt2_1_2.Text + txt2_1_3.Text + txt2_1_4.Text + txt2_1_5.Text != "651148")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_2_1.Text + txt2_2_2.Text + txt2_2_3.Text + txt2_2_4.Text + txt2_2_5.Text != "241172")
                if (txt2_2_1.Text + txt2_2_2.Text + txt2_2_3.Text + txt2_2_4.Text + txt2_2_5.Text != "271142")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_3_1.Text + txt2_3_2.Text + txt2_3_3.Text + txt2_3_4.Text + txt2_3_5.Text  != "91246")
                if (txt2_3_1.Text + txt2_3_2.Text + txt2_3_3.Text + txt2_3_4.Text + txt2_3_5.Text != "94216")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_4_1.Text + txt2_4_2.Text + txt2_4_3.Text + txt2_4_4.Text + txt2_4_5.Text + txt2_4_6.Text + txt2_4_7.Text != "1312411516")
                if (txt2_4_1.Text + txt2_4_2.Text + txt2_4_3.Text + txt2_4_4.Text + txt2_4_5.Text + txt2_4_6.Text + txt2_4_7.Text != "1351142116")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_5_1.Text + txt2_5_2.Text + txt2_5_3.Text + txt2_5_4.Text + txt2_5_5.Text + txt2_5_6.Text + txt2_5_7.Text != "31241174")
                if (txt2_5_1.Text + txt2_5_2.Text + txt2_5_3.Text + txt2_5_4.Text + txt2_5_5.Text + txt2_5_6.Text + txt2_5_7.Text != "37114214")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt2_6_1.Text + txt2_6_2.Text + txt2_6_3.Text + txt2_6_4.Text + txt2_6_5.Text != "81142-1")
                if (txt2_6_1.Text + txt2_6_2.Text + txt2_6_3.Text + txt2_6_4.Text + txt2_6_5.Text != "82411-1")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridStep3_Kontur.Visibility = Visibility.Collapsed;
                btnStep3_1Ok.Visibility = Visibility.Collapsed;
                gridStep3_End.Visibility = Visibility.Visible;
                btnStep3_2Ok.Visibility = Visibility.Visible;
            }

        }

        private void btnStep3_2Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2Otr.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2Otr.Text != "6")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep3.Visibility = Visibility.Collapsed;
                    txtStep4.Visibility = Visibility.Visible;
                }


            }
        }

        private void btnStep4_Ok_Click(object sender, RoutedEventArgs e)
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

            if (txt2_a_1_1.Text + txt2_a_1_2.Text + txt2_a_1_3.Text != "0-22")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_2_1.Text + txt2_a_2_2.Text + txt2_a_2_3.Text != "-3-2-1")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_3_1.Text + txt2_a_3_2.Text + txt2_a_3_3.Text != "11-213")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2_a_4_1.Text + txt2_a_4_2.Text + txt2_a_4_3.Text != "-2-20")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep4.Visibility = Visibility.Collapsed;
                DataRow rows = dt_answer.Rows[0];
                rows[0] = 13;
                rows[1] = "";
                rows = dt_answer.Rows[2];
                rows[0] = 1;
                rows[1] = 2;
                txtStep4_1.Visibility = Visibility.Visible;
            }
        }

        private void HplStep4_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep4_Theory.Visibility = Visibility.Visible;
        }

        private void btnStep4_1_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep4_1.Visibility = Visibility.Collapsed;
            txtStep5.Visibility = Visibility.Visible;
        }

        private void btnStep4Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3_1_1_1.Text == "" || txt3_1_1_2.Text == "" || txt3_1_2_1.Text == "" || txt3_1_2_2.Text == "" || txt3_1_3_1.Text == "" || txt3_1_3_2.Text == "" || txt3_1_4_1.Text == "" || txt3_1_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_2_1_1.Text == "" || txt3_2_1_2.Text == "" || txt3_2_2_1.Text == "" || txt3_2_2_2.Text == "" || txt3_2_3_1.Text == "" || txt3_2_3_2.Text == "" || txt3_2_4_1.Text == "" || txt3_2_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_3_1_1.Text == "" || txt3_3_1_2.Text == "" || txt3_3_2_1.Text == "" || txt3_3_2_2.Text == "" || txt3_3_3_1.Text == "" || txt3_3_3_2.Text == "" || txt3_3_4_1.Text == "" || txt3_3_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_4_1_1.Text == "" || txt3_4_1_2.Text == "" || txt3_4_2_1.Text == "" || txt3_4_2_2.Text == "" || txt3_4_3_1.Text == "" || txt3_4_3_2.Text == "" || txt3_4_4_1.Text == "" || txt3_4_4_2.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_5_1_1.Text == "" || txt3_5_1_2.Text == "" || txt3_5_2_1.Text == "" || txt3_5_2_2.Text == "" || txt3_5_3_1.Text == "" || txt3_5_3_2.Text == "" || txt3_5_4_1.Text == "" || txt3_5_4_2.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_6_1_1.Text == "" || txt3_6_1_2.Text == "" || txt3_6_2_1.Text == "" || txt3_6_2_2.Text == "" || txt3_6_3_1.Text == "" || txt3_6_3_2.Text == "" || txt3_6_4_1.Text == "" || txt3_6_4_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt3_1_1_1.Text + txt3_1_1_2.Text + txt3_1_2_1.Text + txt3_1_2_2.Text + txt3_1_3_1.Text + txt3_1_3_2.Text + txt3_1_4_1.Text + txt3_1_4_2.Text != "12113132")
                if (txt3_1_1_1.Text + txt3_1_1_2.Text + txt3_1_2_1.Text + txt3_1_2_2.Text + txt3_1_3_1.Text + txt3_1_3_2.Text + txt3_1_4_1.Text + txt3_1_4_2.Text != "12323111")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_2_1_1.Text + txt3_2_1_2.Text + txt3_2_2_1.Text + txt3_2_2_2.Text + txt3_2_3_1.Text + txt3_2_3_2.Text + txt3_2_4_1.Text + txt3_2_4_2.Text != "13113133")
                if (txt3_2_1_1.Text + txt3_2_1_2.Text + txt3_2_2_1.Text + txt3_2_2_2.Text + txt3_2_3_1.Text + txt3_2_3_2.Text + txt3_2_4_1.Text + txt3_2_4_2.Text != "13333111")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте второй контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_3_1_1.Text + txt3_3_1_2.Text + txt3_3_2_1.Text + txt3_3_2_2.Text + txt3_3_3_1.Text + txt3_3_3_2.Text + txt3_3_4_1.Text + txt3_3_4_2.Text != "14113134")
                if (txt3_3_1_1.Text + txt3_3_1_2.Text + txt3_3_2_1.Text + txt3_3_2_2.Text + txt3_3_3_1.Text + txt3_3_3_2.Text + txt3_3_4_1.Text + txt3_3_4_2.Text != "14343111")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третий контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_4_1_1.Text + txt3_4_1_2.Text + txt3_4_2_1.Text + txt3_4_2_2.Text + txt3_4_3_1.Text + txt3_4_3_2.Text + txt3_4_4_1.Text + txt3_4_4_2.Text  != "21223231")
                if (txt3_4_1_1.Text + txt3_4_1_2.Text + txt3_4_2_1.Text + txt3_4_2_2.Text + txt3_4_3_1.Text + txt3_4_3_2.Text + txt3_4_4_1.Text + txt3_4_4_2.Text  != "21313222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_5_1_1.Text + txt3_5_1_2.Text + txt3_5_2_1.Text + txt3_5_2_2.Text + txt3_5_3_1.Text + txt3_5_3_2.Text + txt3_5_4_1.Text + txt3_5_4_2.Text != "23223233")
                if (txt3_5_1_1.Text + txt3_5_1_2.Text + txt3_5_2_1.Text + txt3_5_2_2.Text + txt3_5_3_1.Text + txt3_5_3_2.Text + txt3_5_4_1.Text + txt3_5_4_2.Text != "23333222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятый контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_6_1_1.Text + txt3_6_1_2.Text + txt3_6_2_1.Text + txt3_6_2_2.Text + txt3_6_3_1.Text + txt3_6_3_2.Text + txt3_6_4_1.Text + txt3_6_4_2.Text != "24223234")
                if (txt3_6_1_1.Text + txt3_6_1_2.Text + txt3_6_2_1.Text + txt3_6_2_2.Text + txt3_6_3_1.Text + txt3_6_3_2.Text + txt3_6_4_1.Text + txt3_6_4_2.Text != "24343222")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестой контур.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridStep5_Algebra.Visibility = Visibility.Visible;
                btnStep5_1Ok.Visibility = Visibility.Visible;
                btnStep5Ok.Visibility = Visibility.Collapsed;
            }
        }


        private void btnStep5_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3_1_1.Text == "" || txt3_1_2.Text == "" || txt3_1_3.Text == "" || txt3_1_4.Text == "" || txt3_1_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_2_1.Text == "" || txt3_2_2.Text == "" || txt3_2_3.Text == "" || txt3_2_4.Text == "" || txt3_2_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_3_1.Text == "" || txt3_3_2.Text == "" || txt3_3_3.Text == "" || txt3_3_4.Text == "" || txt3_3_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_4_1.Text == "" || txt3_4_2.Text == "" || txt3_4_3.Text == "" || txt3_4_4.Text == "" || txt3_4_5.Text == "" )
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_5_1.Text == "" || txt3_5_2.Text == "" || txt3_5_3.Text == "" || txt3_5_4.Text == "" || txt3_5_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3_6_1.Text == "" || txt3_6_2.Text == "" || txt3_6_3.Text == "" || txt3_6_4.Text == "" || txt3_6_5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txt3_1_1.Text + txt3_1_2.Text + txt3_1_3.Text + txt3_1_4.Text + txt3_1_5.Text != "241181")
                if (txt3_1_1.Text + txt3_1_2.Text + txt3_1_3.Text + txt3_1_4.Text + txt3_1_5.Text != "281141")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте первую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_2_1.Text + txt3_2_2.Text + txt3_2_3.Text + txt3_2_4.Text + txt3_2_5.Text != "641158")
                if (txt3_2_1.Text + txt3_2_2.Text + txt3_2_3.Text + txt3_2_4.Text + txt3_2_5.Text != "651148")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте вторую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_3_1.Text + txt3_3_2.Text + txt3_3_3.Text + txt3_3_4.Text + txt3_3_5.Text != "241172")
                if (txt3_3_1.Text + txt3_3_2.Text + txt3_3_3.Text + txt3_3_4.Text + txt3_3_5.Text != "271142")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте третью сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_4_1.Text + txt3_4_2.Text + txt3_4_3.Text + txt3_4_4.Text + txt3_4_5.Text  != "918115")
                if (txt3_4_1.Text + txt3_4_2.Text + txt3_4_3.Text + txt3_4_4.Text + txt3_4_5.Text  != "911815")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте четвертую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_5_1.Text + txt3_5_2.Text + txt3_5_3.Text + txt3_5_4.Text + txt3_5_5.Text  != "1318515")
                if (txt3_5_1.Text + txt3_5_2.Text + txt3_5_3.Text + txt3_5_4.Text + txt3_5_5.Text  != "1358115")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте пятую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txt3_6_1.Text + txt3_6_2.Text + txt3_6_3.Text + txt3_6_4.Text + txt3_6_5.Text != "31873")
                if (txt3_6_1.Text + txt3_6_2.Text + txt3_6_3.Text + txt3_6_4.Text + txt3_6_5.Text != "37813")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПерепроверьте шестую сумму.\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep5.Visibility = Visibility.Collapsed;
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
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно и научились решать транспортную задачу распределительным методом!", "Поздравляю", MessageBoxButton.OK, MessageBoxImage.Information))
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
