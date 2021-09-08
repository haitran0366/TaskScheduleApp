using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TaskScheduleApp.Models;
using Xamarin.Forms;

namespace TaskScheduleApp.ViewModel
{
    public class TaskViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskName"));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        private DateTime dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DueDate"));
            }
        }

        private List<TaskModel> taskList;
        public List<TaskModel> TaskList
        {
            get { return taskList; }
            set
            {

                taskList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
            }
        }
        public Command cmdAddTask { get; set; }
        public TaskViewModel()
        {
            cmdAddTask = new Command(AddTask);
            getTask();
        }

        private async void AddTask(object obj)
        {
            var r = await App.TaskDatabase.SaveTaskAsync(new TaskModel
            {
                TaskName = TaskName,
                Description = Description,
                DueDate = DueDate
            });

            getTask();
        }
        public async void getTask()
        {
            TaskList = await App.TaskDatabase.GetTaskAsync();
        }
    }
}
