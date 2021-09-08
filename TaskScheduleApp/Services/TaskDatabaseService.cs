using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskScheduleApp.Models;

namespace TaskScheduleApp.Services
{
    public class TaskDatabaseService
    {
        readonly SQLiteAsyncConnection _database;

        public TaskDatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskModel>().Wait();
        }

        public Task<List<TaskModel>> GetTaskAsync()
        {
            return _database.Table<TaskModel>().OrderByDescending(x => x.DueDate).ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskModel taskModel)
        {
            return _database.InsertAsync(taskModel);

        }

    }
}
