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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_Management_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board _mainBoard;

        public MainWindow()
        {
            InitializeComponent();
            _mainBoard = new Board("головна дошка");
        }

        private void AddTaskListButton_Click(object sender, RoutedEventArgs e)
        {
            string listName = AddTaskListTB.Text.Trim();
            if (string.IsNullOrEmpty(listName)) return;

            _mainBoard.AddList(listName);
            TaskList newTaskList = _mainBoard.GetList(listName);

            Border taskListBorder = new Border
            {
                BorderBrush = Brushes.DarkGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
                Background = Brushes.WhiteSmoke,
                VerticalAlignment = VerticalAlignment.Top,
                Tag = newTaskList
            };

            StackPanel taskListPanel = new StackPanel
            {
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 200
            };

            Grid topPanel = new Grid
            {
                Width = 200
            };

            topPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            topPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            topPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            TextBlock title = new TextBlock
            {
                Text = listName,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center
            };

            Button addTaskButton = new Button();

            Button deleteListButton = new Button
            {
                Content = "-",
                Width = 25,
                Height = 25,
                Margin = new Thickness(0, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            deleteListButton.Click += (s, args) => DeleteTaskList(taskListBorder);
            addTaskButton.Click += (s, args) => AddTaskButton_Click(s, args, newTaskList, taskListPanel);

            Grid.SetColumn(title, 0);
            Grid.SetColumn(addTaskButton, 1);
            Grid.SetColumn(deleteListButton, 2);

            topPanel.Children.Add(title);
            topPanel.Children.Add(addTaskButton);
            topPanel.Children.Add(deleteListButton);

            taskListPanel.Children.Add(topPanel);

            taskListBorder.Child = taskListPanel;
            TaskListsPanel.Children.Add(taskListBorder);
            AddTaskListTB.Text = "";
        }

        private void DeleteTaskList(Border taskListBorder)
        {
            if (MessageBox.Show("Ви впевнені, що хочете видалити цей список задач?",
                                "Підтвердження видалення",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskList taskList = (TaskList)taskListBorder.Tag;

                _mainBoard.RemoveList(taskList.Title);

                TaskListsPanel.Children.Remove(taskListBorder);
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e, TaskList taskList, StackPanel taskListPanel)
        {
            
        }

        private void AddTaskVisual(Task task, StackPanel taskListPanel)
        {
            Border taskBorder = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 5, 0, 0),
                Padding = new Thickness(5),
                Background = GetTaskStatusColor(task.Status),
                Tag = task
            };

            StackPanel taskPanel = new StackPanel();

            TextBlock titleBlock = new TextBlock
            {
                Text = task.Title,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap
            };
            taskPanel.Children.Add(titleBlock);

            if (!string.IsNullOrEmpty(task.Description))
            {
                TextBlock descBlock = new TextBlock
                {
                    Text = task.Description,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                taskPanel.Children.Add(descBlock);
            }

            TextBlock dateBlock = new TextBlock
            {
                Text = $"Термін: {task.DueDate?.ToString("dd.MM.yyyy")}",
                Margin = new Thickness(0, 5, 0, 0)
            };
            taskPanel.Children.Add(dateBlock);

            StackPanel buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Button updateButton = new Button
            {
                Content = "Оновити",
                Width = 70,
                Margin = new Thickness(0, 0, 5, 0)
            };
            

            Button deleteButton = new Button
            {
                Content = "Видалити",
                Width = 70
            };
            deleteButton.Click += (s, args) => DeleteTask(taskBorder, taskListPanel);

            buttonsPanel.Children.Add(updateButton);
            buttonsPanel.Children.Add(deleteButton);

            taskPanel.Children.Add(buttonsPanel);
            taskBorder.Child = taskPanel;

            taskListPanel.Children.Add(taskBorder);
        }

        

        private void UpdateTaskVisual(Border taskBorder, Task task)
        {
            StackPanel taskPanel = (StackPanel)taskBorder.Child;

            TextBlock titleBlock = (TextBlock)taskPanel.Children[0];
            titleBlock.Text = task.Title;

            int descIndex = 1;
            if (taskPanel.Children.Count > 3)
            {
                if (!string.IsNullOrEmpty(task.Description))
                {
                    TextBlock descBlock = (TextBlock)taskPanel.Children[descIndex];
                    descBlock.Text = task.Description;
                }
                else
                {
                    taskPanel.Children.RemoveAt(descIndex);
                }
            }
            else if (!string.IsNullOrEmpty(task.Description))
            {
                TextBlock descBlock = new TextBlock
                {
                    Text = task.Description,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                taskPanel.Children.Insert(descIndex, descBlock);
            }

            int dateIndex = taskPanel.Children.Count - 2;
            TextBlock dateBlock = (TextBlock)taskPanel.Children[dateIndex];
            dateBlock.Text = $"Термін: {task.DueDate?.ToString("dd.MM.yyyy")}";

            taskBorder.Background = GetTaskStatusColor(task.Status);
        }

        private void DeleteTask(Border taskBorder, StackPanel taskListPanel)
        {
            if (MessageBox.Show("Ви впевнені, що хочете видалити цю задачу?",
                                "Підтвердження видалення",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Task task = (Task)taskBorder.Tag;
                TaskList parentList = FindParentTaskList(taskBorder);

                if (parentList != null)
                {
                    parentList.RemoveTask(task.Id);
                }

                taskListPanel.Children.Remove(taskBorder);
            }
        }

        private TaskList FindParentTaskList(Border taskBorder)
        {
            DependencyObject current = taskBorder;

            while (current != null)
            {
                StackPanel panel = current as StackPanel;
                if (panel != null)
                {
                    DependencyObject parent = VisualTreeHelper.GetParent(panel);
                    if (parent is Border border && border.Tag is TaskList taskList)
                    {
                        return taskList;
                    }
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        private SolidColorBrush GetTaskStatusColor(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.ToDo:
                    return new SolidColorBrush(Colors.LightYellow);
                case TaskStatus.InProgress:
                    return new SolidColorBrush(Colors.LightBlue);
                case TaskStatus.Done:
                    return new SolidColorBrush(Colors.LightGreen);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }
    }
}
