#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Pic.DAL;
#endregion

namespace PicParam
{
    public partial class FormWorkerThreadTask : Form, IProcessingCallback
    {
        #region Constructor
        /// <summary>
        /// Class can only be instantiated through static method Execute 
        /// </summary>
        private FormWorkerThreadTask(TreeProcessingTask task)
        {
            InitializeComponent();
            _task = task;
        }
        /// <summary>
        /// Use this method to process any task
        /// </summary>
        public static void Execute(TreeProcessingTask task)
        {
            FormWorkerThreadTask form = new FormWorkerThreadTask(task);
            form.ShowDialog();
        }
        #endregion

        #region Form override
        protected override void OnLoad(EventArgs e)
        {
            // set caption
            Text = _task.Title;
            // start processing
            initEvent.Set();
            ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(ProcessTask), this);
            // base processing
            base.OnLoad(e);
        }
        #endregion

        #region Processing methods
        private void ProcessTask(object status)
        {
            IProcessingCallback callback = status as IProcessingCallback;
            callback.Begin();
            try
            {
                _task.Execute(callback);
            }
            catch (Exception ex)
            {
                if (null != callback)
                    callback.Error(string.Format("{0} failed with error: {1}", _task.Title, ex.Message));
            }
            // ending
            callback.End(); 
        }
        #endregion

        #region ITreeProcessingCallback implementation
        private int _errorCount = 0;
        public bool HadErrors
        {
            get { return _errorCount > 0; }
        }
        public void Begin()
        {
            initEvent.WaitOne();
            Invoke(new AppendTextInvoker(DoAppendText), new object[] { string.Format("{0} : begin...", _task.Title), Color.Black });
        }
        public void End()
        {
            initEvent.WaitOne();
            Invoke(new EndInvoker(DoEnd));
        }

        public void Info(string text)
        {
            initEvent.WaitOne();
            Invoke(new AppendTextInvoker(DoAppendText), new object[] { text, Color.Black });
        }
        public void Error(string text)
        {
            initEvent.WaitOne();
            Invoke(new ErrorIncrementInvoker(IncrementErrorCount));
            Invoke(new AppendTextInvoker(DoAppendText), new object[] { text, Color.Red });
        }
        public bool IsAborting { get { return false; } }

        private void DoEnd()
        {
            string text = string.Format("Process complete -- {0} errors", _errorCount);
            DoAppendText(text, HadErrors ? Color.Red : Color.Black);
            if (!HadErrors)
            {
                Thread.Sleep(1000);
                Close();
            }
        }
        private void DoAppendText(string text, Color textColor)
        {
            rtbOutput.ForeColor = textColor;
            rtbOutput.AppendText(text + System.Environment.NewLine);
            // scroll to last line...
            rtbOutput.SelectionLength = 0;
            rtbOutput.SelectionStart = rtbOutput.Text.Length;
            rtbOutput.ScrollToCaret();
        }

        private void IncrementErrorCount()
        {
            ++_errorCount;
        }
        #endregion

        #region Data members
        /// <summary>
        /// Task being performed
        /// </summary>
        private TreeProcessingTask _task;
        /// <summary>
        /// notifies one or more threads that an event has occured
        /// </summary>
        private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// delegate to be used in Control.Invoke() calls to append text in richTextControl
        /// </summary>
        public delegate void AppendTextInvoker(string text, Color textColor);
        /// <summary>
        /// delegate to be used in Control.Invoke() calls to initialize worker thread job
        /// </summary>
        public delegate void BeginInvoker();
        /// <summary>
        /// delegate to be used in Control.Invoke() calls to notify worker thread job end
        /// and close form
        /// </summary>
        public delegate void EndInvoker();
        /// <summary>
        /// delegate to be used in Control.Invoke() calls to increment error count
        /// </summary>
        public delegate void ErrorIncrementInvoker();
        #endregion
    }
}
