using System;
using System.Collections.Generic;
using System.Timers;

namespace Voluntariat.Services
{
    public interface IScheduledTask
    {
        ScheduledTaskConfiguration Configuration { get; set; }

        Action Action { get; set; }

        void Start();

        void Remove();
    }

    public class ScheduledTask : IDisposable, IScheduledTask
    {
        public ScheduledTaskConfiguration Configuration { get; set; }
        public Action Action { get; set; }

        private Timer timer;

        public void Start()
        {
            timer = new Timer();
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Interval = ComputeTimerInterval(Configuration.RunTime);
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //set next run time 
            timer.Enabled = false;
            timer.Interval = ComputeTimerInterval(Configuration.RunTime);
            timer.Enabled = true;
            //run task

            if (Configuration.DaysOfWeek.Contains(DateTime.Now.DayOfWeek))
                Action();
        }

        private double ComputeTimerInterval(TimeSpan time)
        {
            DateTime now = DateTime.Now;
            DateTime notificationTime = (now.Date + time);

            var timeTilTomorrow = TimeSpan.FromDays(1);
            if (now.TimeOfDay < notificationTime.TimeOfDay)
            {
                return (notificationTime.TimeOfDay - now.TimeOfDay).TotalMilliseconds;
            }
            else
            {
                return timeTilTomorrow.TotalMilliseconds - (now.TimeOfDay - notificationTime.TimeOfDay).TotalMilliseconds;
            }
        }


        public void Dispose()
        {
            timer.Enabled = false;
            timer.Dispose();
        }

        public void Remove()
        {
            this.Dispose();
        }
    }

    public class ScheduledTaskConfiguration
    {
        public TimeSpan IntervalMilliseconds { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public TimeSpan RunTime { get; set; }
    }
}
