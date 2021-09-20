using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSynchronizationContext
{
    public partial class Form1 : Form
    {
        private Worker _worker;
        private SynchronizationContext _context;

        public Form1()
        {
            InitializeComponent();

            btnStart.Click += buttonStart_Click;
            btnStop.Click += buttonStop_Click;
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = SynchronizationContext.Current;

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (_worker != null)
            {
                _worker.Cancel();

            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _worker = new Worker();
            _worker.ProcessChanged += workerProcessChanged;
            _worker.WorkCompleted += _workerWorkCompleted;
            btnStart.Enabled = false;

            //_worker.Work();    //Синхронно 

            Thread thread = new Thread(_worker.Work);   //Асинхронного 
            thread.Start(_context);

        }


        private void _workerWorkCompleted(bool cancelled)
        {
            
                string message = cancelled ? "Процесс отменен" : "Процесс завершен!";
                MessageBox.Show(message);
                btnStart.Enabled = true;

        }

        private void workerProcessChanged(int progress)
        {
                progressBar1.Value = progress + 1;
                progressBar1.Value = progress;
       
        }
    }
}
