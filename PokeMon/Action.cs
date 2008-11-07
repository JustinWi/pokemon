using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PokeMon
{
    class Action
    {
        public Action(ITask task, TimeSpan taskInterval) : this(task, taskInterval, null)
        {
        }

        public Action(ITask task, TimeSpan taskInterval, List<Notifier> notifiers)
        {
            this.task = task;
            this.taskInterval = taskInterval;

            this.notifiers = notifiers;
        }

        public void TakeAction(Object stateInfo)
        {
            // If we're going to go over the number of max results, clear one out
            if (resultHistory.Count == MaxResults)
            {
                resultHistory.RemoveAt(MaxResults - 1);
            }

            // Perform the task and add it's result to the front of the list
            resultHistory.Insert(0, task.Perform());

            // Invoke the necessary notifiers
            ProcessResult();
        }

        protected void ProcessResult()
        {
            foreach (Notifier notifier in Notifiers)
            {
                if (notifier.MeetsThreshold(LastResult.Value))
                {
                    notifier.Notify(LastResult);
                }
            }
        }

        public void AddNotifier(Notifier notifier)
        {
            if (notifiers == null)
            {
                notifiers = new List<Notifier>();
            }

            notifiers.Add(notifier);
        }

        private Timer timer;
        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        private TimeSpan taskInterval;
        public TimeSpan TaskInterval
        {
            get { return taskInterval; }
        }

        private ITask task;
        public ITask Task
        {
            get { return task; }
        }

        private List<Notifier> notifiers = new List<Notifier>();
        public List<Notifier> Notifiers
        {
            get { return notifiers; }
        }

        private List<Result> resultHistory = new List<Result>();
        public List<Result> ResultHistory
        {
            get { return resultHistory; }
        }

        public Result LastResult
        {
            get 
            {
                if (resultHistory != null && resultHistory.Count > 0)
                {
                    return resultHistory[0];
                }
                else
                {
                    return null;
                }
            }
        }

        const int MaxResults = 10;
    }
}
