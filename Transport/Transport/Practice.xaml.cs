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
    /// Логика взаимодействия для Practice.xaml
    /// </summary>
    public partial class Practice : Window
    {
        public Practice()
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

            
        }

        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Resourses/Test.accdb");
        public DataTable dt_question = new DataTable();
        public DataTable dt_answer = new DataTable();
        public DataTable dt_resourses = new DataTable();
        public DataTable dt_needs = new DataTable();
        static public int n, m;
        public int[,] dd;
        private void btnStep1Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtNeeds.Text == "" || txtResources.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           
            
            if (txtNeeds.Text == "A" && txtResources.Text == "O")
            {
                if (MessageBoxResult.OK == MessageBox.Show("Вы ответили правильно, давайте продолжим!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information))
                {
                    txtStep1_1.Visibility = Visibility.Collapsed;
                    txtStep1_2.Visibility = Visibility.Visible;
                    txt_s1.Text = "Олично! Нашли глобальный минимум (O) и максимум (A), теперь вы можете перейти к следущему этапу!";
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
            MessageBox.Show("Данная задача была решена!", "Отлично", MessageBoxButton.OK, MessageBoxImage.Information);
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

 

  
        private void txtResources_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNeeds_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HplStep1_2_Theory_Click(object sender, RoutedEventArgs e)
        {
            gridStep1_2_Theory.Visibility = Visibility.Visible;
        }
      
    }
}
