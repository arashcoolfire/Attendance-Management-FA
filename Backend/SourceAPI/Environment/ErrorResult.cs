using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApi.Environment
{
    public class RepResult<T>
    {
        private bool successed = false;
        public bool Successed
        {
            get
            {
                return successed;
            }
            set
            {
                successed = value;
            }
        }
        public string Message { get; set; }

        public T ResultObject { get; set; }
    }

    public class Result
    {
        public bool Occurred { get; set; }
        public string Message { get; set; }
    }
}
