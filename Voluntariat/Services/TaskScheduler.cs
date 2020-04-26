using System;
using System.Collections.Generic;
using System.Linq;

namespace Voluntariat.Services
{
    public interface ITaskScheduler
    {
        void RunBackgoundTasks();
    }

    public class TaskScheduler : ITaskScheduler
    {
        private readonly IVolunteerService _volunteerService;
        private readonly TimeSpan NoonTime = new TimeSpan(12, 0, 0);
        private readonly List<IScheduledTask> tasks = new List<IScheduledTask>();
        private readonly List<DayOfWeek> workDays;

        public TaskScheduler(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
            workDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        }

        public void RunBackgoundTasks()
        {
            DailyTask();
        }

        private void DailyTask()
        {
            var dailyTask = AddTask(workDays, async () =>
            {
                _volunteerService.DeleteUnaffiliatedVolunteers();
                _volunteerService.NotifyUnaffiliatedVolunteers();
            }, NoonTime);

            dailyTask.Start();
        }

        private IScheduledTask AddTask(List<DayOfWeek> workDays, Action action, TimeSpan runTime)
        {
            if (workDays.Any())
            {
                var taskConfig = new ScheduledTaskConfiguration { DaysOfWeek = workDays, RunTime = runTime };
                var newTask = new ScheduledTask { Configuration = taskConfig, Action = action };
                tasks.Add(newTask);

                return newTask;
            }

            return null;
        }
    }
}
