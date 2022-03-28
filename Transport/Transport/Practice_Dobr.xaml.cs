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
    public partial class Practice_Dobr : Window
    {
        public Practice_Dobr()
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
            {
                dt_answer.Columns.Add();
                dt_step2.Columns.Add();
            }
                
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt_answer.NewRow();
                dt_answer.Rows.Add(row);
                row = dt_step2.NewRow();
                dt_step2.Rows.Add(row);
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
            {
                DataRow rows = dt_question.Rows[i];
                DataRow row = dt_step2.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    row[j] = rows[j];
                }
            }

            //dt_step2 = dt_question;
            gridStep2.AutoGenerateColumns = true;
            gridStep2.ItemsSource = dt_step2.AsDataView();

            for (int j = 0; j < m - 1; j++)
                dt_needs.Columns.Add();
            DataRow rowN = dt_needs.NewRow();
            dt_needs.Rows.Add(rowN);
            DataRow rowNe = dt_needs.Rows[0];
            DataRow rowQ = dt_question.Rows[n - 1];
            for (int j = 0; j < m - 1; j++)
                rowNe[j] = rowQ[j];
            gridNeeds.AutoGenerateColumns = true;
            gridNeeds.ItemsSource = dt_needs.AsDataView();


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

        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
        public DataTable dt_question = new DataTable();
        public DataTable dt_step2 = new DataTable();
        public DataTable dt_answer = new DataTable();
        public DataTable dt_resourses = new DataTable();
        public DataTable dt_needs = new DataTable();
        static public int n, m;
        public int[,] dd;
        public int[,] aa;
        public int[,] cc;

        int sum = 257;
        public int[,] index;

        private void btnStep1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtNeeds.Text == "" || txtResources.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int sumResourses = 0, sumNeeds = 0;
            for (int i = 0; i < n; i++)
            {
                DataRow row = dt_question.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    if (i == n - 1 && j != m - 1) sumNeeds += (int)row[j];
                    if (j == m - 1 && i != n - 1) sumResourses +=(int)row[j];
                }
            }
            if (Convert.ToInt16(txtNeeds.Text) == sumNeeds && Convert.ToInt16(txtResources.Text) == sumResourses)
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep1_1.Visibility = Visibility.Collapsed;
                    txtStep1_2.Visibility = Visibility.Visible;
                    txt_s1.Text = "Верно рассчитав общее количество потребностей (" + sumNeeds + ") и запасов (" + sumResourses + "), теперь вы можете ответить на следующий вопрос: является ли данная транспортная задача сбалансированной?";
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
            if (txtI_1.Text == "" || txtJ_1.Text == "" || txtI_2.Text == "" || txtJ_2.Text == "" || txtI_3.Text == "" || txtJ_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtI_1.Text != "1" || txtJ_1.Text != "2")
                if (txtI_1.Text != "1" || txtJ_1.Text != "4")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txtI_2.Text != "2" || txtJ_2.Text != "2")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (txtI_3.Text != "3" || txtJ_3.Text != "3")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridS_2.Visibility = Visibility.Visible;
                btnStep2_Ok.Visibility = Visibility.Collapsed;
                btnStep2_1Ok.Visibility = Visibility.Visible;
                gridStep2.Visibility = Visibility.Collapsed;

                DataRow rows = dt_step2.Rows[0];
                for (int j = 0; j < m - 1; j++)
                    rows[j] = Convert.ToInt16(rows[j]) - 2;
                rows = dt_step2.Rows[1];
                for (int j = 0; j < m - 1; j++)
                    rows[j] = Convert.ToInt16(rows[j]) - 1;
                rows = dt_step2.Rows[2];
                for (int j = 0; j < m - 1; j++)
                    rows[j] = Convert.ToInt16(rows[j]) - 5;
                gridStep2_2.AutoGenerateColumns = true;
                gridStep2_2.ItemsSource = dt_step2.AsDataView();
            }
        }

        private void btnStep2_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtI2_1.Text == "" || txtJ2_1.Text == "" || txtI2_2.Text == "" || txtJ2_2.Text == "" || txtI2_3.Text == "" || txtJ2_3.Text == "" || txtI2_4.Text == "" || txtJ2_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtI2_1.Text != "1" || txtJ2_1.Text != "1" || txtI2_3.Text != "3" || txtJ2_3.Text != "3" || txtI2_4.Text != "1" || txtJ2_4.Text != "4")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtI2_2.Text != "1" || txtJ2_2.Text != "2" )
                if (txtI2_2.Text != "2" || txtJ2_2.Text != "2")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                for (int i = 0; i < n - 1; i++)
                {
                    DataRow rows = dt_step2.Rows[i];
                    rows[0] = Convert.ToInt16(rows[0]) - 2;
                }
                gridStep4.AutoGenerateColumns = true;
                gridStep4.ItemsSource = dt_step2.AsDataView();
                txtStep2.Visibility = Visibility.Collapsed;
                txtStep4.Visibility = Visibility.Visible;
            }
        }




        private void btnStep4_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtD_I_1.Text == "" || txtD_I_2.Text == "" || txtD_I_3.Text == "" || txtD_J_1.Text == "" || txtD_J_2.Text == "" || txtD_J_3.Text == "" || txtD_J_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtD_I_1.Text != "0" || txtD_I_2.Text != "38" || txtD_I_3.Text != "56" || txtD_J_1.Text != "56" || txtD_J_2.Text != "0" || txtD_J_3.Text != "32" || txtD_J_4.Text != "34")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                btnStep4_Ok.Visibility = Visibility.Collapsed;
                btnStep4_2Ok.Visibility = Visibility.Visible;
                gridDobr_1.Visibility = Visibility.Visible;
            }
        }

        private void btnStep4_2Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtD_1.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtD_1.Text != "56")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep4.Visibility = Visibility.Collapsed;
                txtStep5.Visibility = Visibility.Visible;
            }
        }


        private void btnStep5_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtI_S5.Text == "" || txtJ_S5.Text == "" || txtResourses_S5.Text == "" || txtNeeds_S5.Text == "" || txtAll_S5.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtI_S5.Text != "1" || txtJ_S5.Text != "1" || txtResourses_S5.Text != "13" || txtNeeds_S5.Text != "14" || txtAll_S5.Text != "13")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep5.Visibility = Visibility.Collapsed;
                txtStep6.Visibility = Visibility.Visible;
                DataRow row = dt_needs.Rows[0];
                row[0] = 1;
                row = dt_resourses.Rows[0];
                row[0] = 0;
                row = dt_answer.Rows[0];
                row[0] = 13;

            }
        }

        private void btnStep6_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep6.Visibility = Visibility.Collapsed;
            txtStep7.Visibility = Visibility.Visible;
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt_question.Rows[i];
                DataRow roww = dt_step2.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    roww[j] = rows[j];
                }
            }
            DataRow row = dt_step2.Rows[0];
            for (int j = 0; j < m; j++)
                row[j] = "";
            row = dt_step2.Rows[3];
            row[0] = 1;
            gridStep7.AutoGenerateColumns = true;
            gridStep7.ItemsSource = dt_step2.AsDataView();
        }

        private void btnStep7_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2I_2.Text == "" || txt2J_2.Text == "" || txt2I_3.Text == "" || txt2J_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2I_2.Text != "2" || txt2J_2.Text != "2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2I_3.Text != "3" || txt2J_3.Text != "3")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridS_7.Visibility = Visibility.Visible;
                btnStep7_Ok.Visibility = Visibility.Collapsed;
                btnStep7_1Ok.Visibility = Visibility.Visible;
                gridStep7.Visibility = Visibility.Collapsed;

                DataRow rows = dt_step2.Rows[1];
                for (int j = 0; j < m - 1; j++)
                    rows[j] = Convert.ToInt16(rows[j]) - 1;
                rows = dt_step2.Rows[2];
                for (int j = 0; j < m - 1; j++)
                    rows[j] = Convert.ToInt16(rows[j]) - 5;
                gridStep7_2.AutoGenerateColumns = true;
                gridStep7_2.ItemsSource = dt_step2.AsDataView();
            }
        }

        private void btnStep7_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2I2_1.Text == "" || txt2J2_1.Text == "" || txt2I2_2.Text == "" || txt2J2_2.Text == "" || txt2I2_3.Text == "" || txt2J2_3.Text == "" || txt2I2_4.Text == "" || txt2J2_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2I2_1.Text != "3" || txt2J2_1.Text != "1" || txt2I2_3.Text != "3" || txt2J2_3.Text != "3" || txt2I2_2.Text != "2" || txt2J2_2.Text != "2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt2I2_4.Text != "2" || txt2J2_4.Text != "4")
                if (txt2I2_4.Text != "3" || txt2J2_4.Text != "4")
                {
                    MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                for (int i = 1; i < n - 1; i++)
                {
                    DataRow rows = dt_step2.Rows[i];
                    rows[0] = Convert.ToInt16(rows[0]) - 6;
                    rows[3] = Convert.ToInt16(rows[3]) - 2;
                }
                gridStep8.AutoGenerateColumns = true;
                gridStep8.ItemsSource = dt_step2.AsDataView();
                txtStep7.Visibility = Visibility.Collapsed;
                txtStep8.Visibility = Visibility.Visible;
            }

        }

        private void btnStep8_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2D_I_2.Text == "" || txt2D_I_3.Text == "" || txt2D_J_1.Text == "" || txt2D_J_2.Text == "" || txt2D_J_3.Text == "" || txt2D_J_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2D_I_2.Text != "0" || txt2D_I_3.Text != "0" || txt2D_J_1.Text != "2" || txt2D_J_2.Text != "63" || txt2D_J_3.Text != "96" || txt2D_J_4.Text != "0")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                btnStep8_Ok.Visibility = Visibility.Collapsed;
                btnStep8_2Ok.Visibility = Visibility.Visible;
                grid2Dobr_1.Visibility = Visibility.Visible;
            }
        }

        private void btnStep8_2Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt2D_1.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt2D_1.Text != "96")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep8.Visibility = Visibility.Collapsed;
                txtStep9.Visibility = Visibility.Visible;
            }
        }

        private void btnStep9_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtI_S9.Text == "" || txtJ_S9.Text == "" || txtResourses_S9.Text == "" || txtNeeds_S9.Text == "" || txtAll_S9.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtI_S9.Text != "3" || txtJ_S9.Text != "3" || txtResourses_S9.Text != "28" || txtNeeds_S9.Text != "8" || txtAll_S9.Text != "8")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep9.Visibility = Visibility.Collapsed;
                txtStep10.Visibility = Visibility.Visible;
                DataRow row = dt_needs.Rows[0];
                row[2] = 0;
                row = dt_resourses.Rows[2];
                row[0] = 20;
                row = dt_answer.Rows[2];
                row[2] = 8;

            }
        }


        private void btnStep10_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep10.Visibility = Visibility.Collapsed;
            txtStep11.Visibility = Visibility.Visible;
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt_question.Rows[i];
                DataRow roww = dt_step2.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    roww[j] = rows[j];
                }
            }
            DataRow row = dt_step2.Rows[0];
            for (int j = 0; j < m; j++)
                row[j] = "";
            row = dt_step2.Rows[3];
            row[0] = 1;
            row[2] = "";
            row = dt_step2.Rows[2];
            row[m-1] = 20;
            row[2] = "";
            row = dt_step2.Rows[1];
            row[2] = "";


            gridStep11.AutoGenerateColumns = true;
            gridStep11.ItemsSource = dt_step2.AsDataView();
        }


        private void btnStep11_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3I_2.Text == "" || txt3J_2.Text == "" || txt3I_3.Text == "" || txt3J_3.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3I_2.Text != "2" || txt3J_2.Text != "2")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txt3I_3.Text != "3" || txt3J_3.Text != "4")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                gridS_11.Visibility = Visibility.Visible;
                btnStep11_Ok.Visibility = Visibility.Collapsed;
                btnStep11_1Ok.Visibility = Visibility.Visible;
                gridStep11.Visibility = Visibility.Collapsed;

                DataRow rows = dt_step2.Rows[1];
                for (int j = 0; j < m - 1; j++)
                    if (j!=2) rows[j] = Convert.ToInt16(rows[j]) - 1;
                rows = dt_step2.Rows[2];
                for (int j = 0; j < m - 1; j++)
                    if (j != 2) rows[j] = Convert.ToInt16(rows[j]) - 7;
                gridStep11_2.AutoGenerateColumns = true;
                gridStep11_2.ItemsSource = dt_step2.AsDataView();
            }
        }

        private void btnStep11_1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3I2_1.Text == "" || txt3J2_1.Text == "" || txt3I2_2.Text == "" || txt3J2_2.Text == "" || txt3I2_4.Text == "" || txt3J2_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3I2_1.Text != "3" || txt3J2_1.Text != "1" || txt3I2_2.Text != "2" || txt3J2_2.Text != "2" || txt3I2_4.Text != "3" || txt3J2_4.Text != "4")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                for (int i = 1; i < n - 1; i++)
                {
                    DataRow rows = dt_step2.Rows[i];
                    rows[0] = Convert.ToInt16(rows[0]) - 4;
                }
                gridStep12.AutoGenerateColumns = true;
                gridStep12.ItemsSource = dt_step2.AsDataView();
                txtStep11.Visibility = Visibility.Collapsed;
                txtStep12.Visibility = Visibility.Visible;
            }

        }

        private void btnStep12_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3D_I_2.Text == "" || txt3D_I_3.Text == "" || txt3D_J_1.Text == "" || txt3D_J_2.Text == "" || txt3D_J_4.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3D_I_2.Text != "38" || txt3D_I_3.Text != "20" || txt3D_J_1.Text != "4" || txt3D_J_2.Text != "21" || txt3D_J_4.Text != "34")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                btnStep12_Ok.Visibility = Visibility.Collapsed;
                btnStep12_2Ok.Visibility = Visibility.Visible;
                grid3Dobr_1.Visibility = Visibility.Visible;
            }
        }

        private void btnStep12_2Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt3D_1.Text == "")
            {
                MessageBox.Show("Вы не заполнили поле!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txt3D_1.Text != "38")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep12.Visibility = Visibility.Collapsed;
                txtStep13.Visibility = Visibility.Visible;
            }
        }


        private void btnStep13_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtI_S13.Text == "" || txtJ_S13.Text == "" || txtResourses_S13.Text == "" || txtNeeds_S13.Text == "" || txtAll_S13.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtI_S13.Text != "2" || txtJ_S13.Text != "2" || txtResourses_S13.Text != "19" || txtNeeds_S13.Text != "21" || txtAll_S13.Text != "19")
            {
                MessageBox.Show("Вы ответили неправильно, попробуйте дать ответ еще раз!\nПри необходимости воспользуйтесь Теоретической справкой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
            {
                txtStep13.Visibility = Visibility.Collapsed;
                txtStep14.Visibility = Visibility.Visible;
                DataRow row = dt_needs.Rows[0];
                row[1] = 2;
                row = dt_resourses.Rows[1];
                row[0] = 0;
                row = dt_answer.Rows[1];
                row[1] = 19;

            }
        }

        private void btnStep14_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep14.Visibility = Visibility.Collapsed;
            txtStep15.Visibility = Visibility.Visible;
            for (int i = 0; i < n; i++)
            {
                DataRow rows = dt_question.Rows[i];
                DataRow roww = dt_step2.Rows[i];
                for (int j = 0; j < m; j++)
                {
                    roww[j] = rows[j];
                }
            }
            DataRow row = dt_step2.Rows[0];
            for (int j = 0; j < m; j++)
                row[j] = "";
            row = dt_step2.Rows[1];
            for (int j = 0; j < m; j++)
                row[j] = "";
            row = dt_step2.Rows[3];
            row[0] = 1;
            row[0] = 2;
            row[2] = "";
            row = dt_step2.Rows[2];
            row[m - 1] = 20;
            row[2] = "";

            gridStep15.AutoGenerateColumns = true;
            gridStep15.ItemsSource = dt_step2.AsDataView();
        }

        private void btnStep15_Ok_Click(object sender, RoutedEventArgs e)
        {
            txtStep15.Visibility = Visibility.Collapsed;
            txtStep2_3.Visibility = Visibility.Visible;

            DataRow row = dt_needs.Rows[0];
            row[0] = 0;
            row[1] = 0;
            row[2] = 0;
            row[3] = 0;
            row = dt_resourses.Rows[2];
            row[0] = 0;
            row = dt_answer.Rows[2];
            row[0] = 1;
            row[1] = 2;
            row[3] = 17;
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
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно и научились решать транспортную задачу методом добротностей!", "Поздравляю", MessageBoxButton.OK, MessageBoxImage.Information))
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

        private void HplStep3_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep3_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep1_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep1_2_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep4_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep4_Theory.Visibility = Visibility.Visible;
        }

        private void HplStep5_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep5_Theory.Visibility = Visibility.Visible;
        }


        private void HplStep7_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep7_Theory.Visibility = Visibility.Visible;
        }



        private void HplStep8_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep8_Theory.Visibility = Visibility.Visible;
        }



        private void HplStep9_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep9_Theory.Visibility = Visibility.Visible;
        }



        private void HplStep11_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep11_Theory.Visibility = Visibility.Visible;
        }



        private void HplStep12_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep12_Theory.Visibility = Visibility.Visible;

        }



        private void HplStep13_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep13_Theory.Visibility = Visibility.Visible;
        }



        private void HplStep2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep2_Theory.Visibility = Visibility.Visible;
        }
    }
}
