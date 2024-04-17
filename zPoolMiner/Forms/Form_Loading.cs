using System;
using System.Drawing;
using System.Windows.Forms;
using zPoolMiner.Interfaces;
using zPoolMiner.Utils;

namespace zPoolMiner
{
    public partial class Form_Loading : Form, IMessageNotifier, IMinerUpdateIndicator
    {
        public interface IAfterInitializationCaller
        {
            void AfterLoadComplete();
        }

        private int LoadCounter;
        private int TotalLoadSteps = 12;
        private readonly IAfterInitializationCaller AfterInitCaller;

        // init loading stuff
        public Form_Loading(IAfterInitializationCaller initCaller, string loadFormTitle, string startInfoMsg, int totalLoadSteps)
        {
            InitializeComponent();

            label_LoadingText.Text = loadFormTitle;
            label_LoadingText.Location = new Point((Size.Width - label_LoadingText.Size.Width) / 2, label_LoadingText.Location.Y);

            AfterInitCaller = initCaller;

            TotalLoadSteps = totalLoadSteps;
            // progressBar1.Maximum = TotalLoadSteps;
            progressBar1.Maximum = TotalLoadSteps;
            // progressBar1.Value = 0;
            progressBar1.Value = 0;

            SetInfoMsg(startInfoMsg);
        }

        // download miners constructor
        private MinersDownloader _minersDownloader;

        public Form_Loading(MinersDownloader minersDownloader)
        {
            InitializeComponent();
            label_LoadingText.Location = new Point((Size.Width - label_LoadingText.Size.Width) / 2, label_LoadingText.Location.Y);
            _minersDownloader = minersDownloader;
        }

        public void IncreaseLoadCounterAndMessage(string infoMsg)
        {
            SetInfoMsg(infoMsg);
            IncreaseLoadCounter();
        }

        public void SetProgressMaxValue(int maxValue)
        {
            // progressBar1.Maximum = maxValue;
            progressBar1.Maximum = maxValue;
        }

        public void SetInfoMsg(string infoMsg) => LoadText.Text = infoMsg;

        public void IncreaseLoadCounter()
        {
            LoadCounter++;
            // progressBar1.Value = LoadCounter;
            progressBar1.Value = LoadCounter;
            Update();

            if (LoadCounter >= TotalLoadSteps)
            {
                AfterInitCaller.AfterLoadComplete();
                Close();
                Dispose();
            }
        }

        public void FinishLoad()
        {
            while (LoadCounter < TotalLoadSteps)
                IncreaseLoadCounter();
        }

        public void SetValueAndMsg(int setValue, string infoMsg)
        {
            SetInfoMsg(infoMsg);
            // progressBar1.Value = setValue;
            progressBar1.Value = setValue;
            Update();
            /*if (progressBar1.Value >= progressBar1.Maximum)
            {
                Close();
                Dispose();
            }*/
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                Close();
                Dispose();
            }
        }

        #region IMessageNotifier

        public void SetMessage(string infoMsg) => SetInfoMsg(infoMsg);

        public void SetMessageAndIncrementStep(string infoMsg)
        {
            IncreaseLoadCounterAndMessage(infoMsg);
        }

        #endregion IMessageNotifier

        #region IMinerUpdateIndicator

        public void SetMaxProgressValue(int max)
        {
            Invoke((MethodInvoker)delegate
            {
                // this.progressBar1.Maximum = max;
                progressBar1.Maximum = max;
                // this.progressBar1.Value = 0;
                progressBar1.Value = 0;
            });
        }

        public void SetProgressValueAndMsg(int value, string msg)
        {
            /*if (value <= this.progressBar1.Maximum)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.progressBar1.Value = value;
                    this.LoadText.Text = msg;
                    this.progressBar1.Invalidate();
                    this.LoadText.Invalidate();
                });
            }*/
            if (value <= progressBar1.Maximum)
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = value;
                    LoadText.Text = msg;
                    progressBar1.Invalidate();
                    LoadText.Invalidate();
                });
            }
        }

        public void SetTitle(string title)
        {
            Invoke((MethodInvoker)delegate
            {
                label_LoadingText.Text = title;
            });
        }

        public void FinishMsg(bool ok)
        {
            Invoke((MethodInvoker)delegate
            {
                if (ok) label_LoadingText.Text = "Init Finished!";
                else label_LoadingText.Text = "Init Failed!";

                System.Threading.Thread.Sleep(1000);
                Close();
            });
        }

        #endregion IMinerUpdateIndicator

        private void Form_Loading_Shown(object sender, EventArgs e)
        {
            if (_minersDownloader != null)
            {
                _minersDownloader.Start(this);
            }
        }
    }
}