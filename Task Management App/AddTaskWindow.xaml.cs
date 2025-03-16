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

namespace Task_Management_App
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public Task NewTask { get; private set; }

        public AddTaskWindow()
        {
            InitializeComponent();
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Введіть назву задачі", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string description = DescriptionTextBox.Text.Trim();
            TaskStatus status = TaskStatus.ToDo;

            if (InProgressRadioButton.IsChecked == true)
                status = TaskStatus.InProgress;
            else if (DoneRadioButton.IsChecked == true)
                status = TaskStatus.Done;

            DateTime dueDate = DueDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);

            NewTask = new Task(title, description, status, dueDate);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
