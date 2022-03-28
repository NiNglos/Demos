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
    /// Логика взаимодействия для Practice_Min.xaml
    /// </summary>
    public partial class Practice_Min : Window
    {
        public Practice_Min()
        {
            InitializeComponent();

            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select Count(*) From Practice";
            command.Connection = myConnection;
            myConnection.Open();

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = Convert.ToInt16(reader[0].ToString());
            reader.Close();

            Random rand = new Random();
            int r = rand.Next(1, count + 1);
            command.CommandText = $"Select table_name From Practice Where id_question = {r}";
            reader = command.ExecuteReader();
            reader.Read();
            command.CommandText = $"Select * From {reader[0].ToString()}";
            reader.Close();



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




            dt_resourses.Columns.Add();
            for (int i = 0; i < n - 1; i++)
            {
                DataRow rowQu = dt_resourses.NewRow();
                dt_resourses.Rows.Add(rowQu);
            }
            for (int i = 0; i < n - 1; i++)
            {
                DataRow rowQue = dt_question.Rows[i];
                DataRow rowR = dt_resourses.Rows[i];
                rowR[0] = rowQue[m - 1];
            }
            gridResourses.AutoGenerateColumns = true;
            gridResourses.ItemsSource = dt_resourses.AsDataView();



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


        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
        public DataTable dt_question = new DataTable();
        public DataTable dt_answer = new DataTable();
        public DataTable dt_resourses = new DataTable();
        public DataTable dt_needs = new DataTable();
        static public int n, m;
        public int[,] dd;
        public int[,] aa;
        public int[,] cc;
        int index_i = 0, index_j = 0, x=1;
        int sum = 0;
        public int[,] index;

        private void btnStep1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtNeeds.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
          
            if (Convert.ToInt16(txtNeeds.Text) == 2)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep1_1.Visibility = Visibility.Collapsed;
                    txtStep1_2.Visibility = Visibility.Visible;
                    txt_s1.Text = "Верно рассчитав общее количество потребностей (" + 2 + ") и запасов, теперь вы можете ответить на следующий вопрос: является ли данная транспортная задача сбалансированной?";
                }
            }
            else
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void btnStep1_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (rbt1.IsChecked == false && rbt2.IsChecked == false)
            {
                MessageBox.Show("Вы не ответили на вопрос!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (rbt2.IsChecked == true)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep1_2.Visibility = Visibility.Collapsed;
                    txtStep2.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        private void btnStep2_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtNeeds_S2.Text == "" || txtResourses_S2.Text == "" || txtAll_S2.Text == "" || txtI_S2.Text == "" || txtJ_S2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int min = aa[0, 0], imin = 0, jmin = 0 ;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < m - 1; j++)
                    if (aa[i, j] < min)
                    {
                        min = aa[i, j];
                        imin = i;
                        jmin = j;
                    }
                        
            DataRow rowN = dt_needs.Rows[0];
            DataRow rowR = dt_resourses.Rows[imin];
            int N = Convert.ToInt16(rowN[jmin]), R = Convert.ToInt16(rowR[0]), A = N;
            if (A > R) A = R;
            if (Convert.ToInt16(txtNeeds_S2.Text) != N || Convert.ToInt16(txtResourses_S2.Text) != R || Convert.ToInt16(txtAll_S2.Text) != A || Convert.ToInt16(txtI_S2.Text) != imin + 1 || Convert.ToInt16(txtJ_S2.Text) != jmin + 1)
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep2.Visibility = Visibility.Collapsed;
                    txtStep2_1.Visibility = Visibility.Visible;
                    DataRow row_a = dt_answer.Rows[imin];
                    row_a[jmin] = A;
                    N -= A;
                    R -= A;
                    rowN[jmin] = N;
                    rowR[0] = R;
                    int[,] d = new int[n, m];
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                            if ((j == m - 1) || (i == n - 1))
                                d[i, j] = aa[i, j];
                    cc = d;
                }
            }
        }
        private void btnStep2_1_Ok_Click(object sender, RoutedEventArgs e)
        {

                    txtStep2_1.Visibility = Visibility.Collapsed;
                    txtStep2_2.Visibility = Visibility.Visible;

        }

        private void btnStep2_2_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtI_S2_2.Text == "" || txtJ_S2_2.Text == "" || txtNeeds_S2_2.Text == "" || txtResourses_S2_2.Text == "" || txtAll_S2_2.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (x==1) cc[index[0, x - 1], index[1, x - 1]] = Math.Min(aa[index[0, x - 1],m-1], aa[n-1, index[1, x - 1]]);
            DataRow rowN = dt_needs.Rows[0];
            DataRow rowR = dt_resourses.Rows[index[0,x]];
            int N = Convert.ToInt16(rowN[index[1,x]]), R = Convert.ToInt16(rowR[0]), A = N;
            if (A > R) A = R;
            if (Convert.ToInt16(txtI_S2_2.Text) == index[0,x]+1 && Convert.ToInt16(txtJ_S2_2.Text) == index[1,x]+1 && Convert.ToInt16(txtNeeds_S2_2.Text) == N && Convert.ToInt16(txtResourses_S2_2.Text) == R && Convert.ToInt16(txtAll_S2_2.Text) == A)
            {
                
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    DataRow row_a = dt_answer.Rows[index[0, x]];
                    row_a[index[1, x]] = A;
                    N -= A;
                    R -= A;
                    rowN[index[1, x]] = N;
                    rowR[0] = R;
                    cc[index[0, x], index[1, x]] = A;
                    int kol = 0;
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                            if (cc[i, j] != dd[i, j]) kol++;
                    if (kol==0)
                    {
                        txtStep2_2.Visibility = Visibility.Collapsed;
                        txtStep2_3.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        txtI_S2_2.Text = "";
                        txtJ_S2_2.Text = "";
                        txtResourses_S2_2.Text = "";
                        txtNeeds_S2_2.Text = "";
                        txtAll_S2_2.Text = "";
                        x++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }
        private void btnStep2_3_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep2_3.Visibility = Visibility.Collapsed;
            txtStep3.Visibility = Visibility.Visible;
        }
        private void btnStep3_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtS3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Convert.ToInt16(txtS3.Text) == sum)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно и научились решать транспортную задачу методом минимального элемента!", "Поздравляю", MessageBoxButton.OK, MessageBoxImage.Information))
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }




        private void txtNeeds_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void txtResources_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }
        private void gridAnswer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }
        



        

        

        private void HplStep1Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep1Theory.Visibility = Visibility.Visible;
        }

        private void HplStep2_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep2_2_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep3_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep3_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep1_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep1_2_Theory.Visibility = Visibility.Visible;
        }
        private void HplStep2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep2_Theory.Visibility = Visibility.Visible;
        }
    }
}
