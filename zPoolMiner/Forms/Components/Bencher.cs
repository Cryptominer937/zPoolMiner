using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zPoolMiner.Devices;

namespace zPoolMiner.Forms.Components
{
    public partial class Bencher : Component
    {
        private ComputeDevice d;
        private Queue<Algorithm> queue;
        public Bencher()
        {
            InitializeComponent();
        }

        public Bencher(ComputeDevice d, Queue<Algorithm> queue)
        {
            InitializeComponent();
            this.d = d;
            this.queue = queue;
        }

        public Bencher(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
