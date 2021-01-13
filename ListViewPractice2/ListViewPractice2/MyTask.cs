using System;
using System.Collections.Generic;

namespace ListViewPractice2
{
    public class MyTask : EqualityComparer<MyTask>
    {
        private string _task;
        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                if(value > 0 && value <= 10)
                {
                    _difficulty = value;
                }
                else
                {
                    throw new ArgumentException("Difficulty needs to be between 1 to 10");
                }
                
            }
        }

        private DateTime _due;
        public DateTime Due
        {
            get
            {
                return _due;
            }
            set
            {
                if(value is DateTime)
                {
                    _due = value;
                }
                
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(value.Equals(MyStatus.Pending.ToString()) || value.Equals(MyStatus.Done.ToString()))
                {
                    _status = value;
                }
                else
                {
                   throw new ArgumentException("status should be Done or Pending");
                }
                
            }
        }

        public MyTask(String task, int diff, DateTime date, string status)
        {
            Task = task;
            Difficulty = diff;
            Due = date;
            Status = status;
        }

        public override string ToString()
        {
            return $"{Task} by {Due:d} / difficulty.{Difficulty}, {Status}";
        }

        public string ToDataString()
        {
            return $"{Task};{Difficulty};{Due:d};{Status}";
        }

        public override bool Equals(MyTask x, MyTask y)
        {
            return x.Task == y.Task && x.Status == y.Status && x.Difficulty == y.Difficulty && x.Due == y.Due;
        }

        public override int GetHashCode(MyTask obj)
        {
            throw new NotImplementedException();
        }
    }
    

}

/*< Button x: Name = "btnExport" Content = "Export All to file ..." Grid.Column = "1" Grid.Row = "3"
                Width = "150" Height = "30" HorizontalAlignment = "Right" Margin = "5" />*/
