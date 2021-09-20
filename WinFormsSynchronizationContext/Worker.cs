using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsSynchronizationContext
{
    public class Worker
    {
        private bool _cancelled = false;


        public void Cancel()
        {
            _cancelled = true;
        }

        public void Work(object param)
        {
        SynchronizationContext context =(SynchronizationContext)param;
            for (int i = 0; i < 100; i++)
            {
                if (_cancelled)
                    break;

                Thread.Sleep(50);
                context.Send(OnProgressChanged, i);
            }

            context.Send(OnWorkCompleted, _cancelled);

        }

        public void OnProgressChanged(object i)
        {
            if (ProcessChanged != null)
                ProcessChanged((int)i);

        }

        public void OnWorkCompleted(object cancelled)
        {
            if (WorkCompleted != null)
                WorkCompleted((bool)cancelled);
        }

        public event Action<int> ProcessChanged;
        public event Action<bool> WorkCompleted;

    }
}
