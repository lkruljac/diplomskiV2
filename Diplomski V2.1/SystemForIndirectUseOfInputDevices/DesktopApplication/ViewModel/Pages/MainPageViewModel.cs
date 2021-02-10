using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Common;
using System.Collections.ObjectModel;
using ViewModel.Controls;
using System.ComponentModel;
using System.Threading;

namespace ViewModel.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        #region Properites
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; RaisePropertyChangedEvent("Title"); }
        }

        private DeviceListViewModel _DeviceListVM;
        public DeviceListViewModel DeviceListVM
        {
            get { return _DeviceListVM; }
            set { _DeviceListVM = value; RaisePropertyChangedEvent("DeviceListVM"); }
        }


        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; RaisePropertyChangedEvent("Text"); }
        }

      

        #endregion


        #region Constructor(s)
        public MainPageViewModel(MainWindowViewModel ownerWindow) : base(ownerWindow)
        {
            DeviceListVM = new DeviceListViewModel();
        }
        #endregion

        #region Methods
        public override void EnterPage()
        {
            DeviceListVM.ListAllConnectedDevices();
            BgWorker = new BackgroundWorker();
            SetWorkerCallbacks(BgWorker);
            RunWorker(BgWorker, null);
        }
        #endregion



        #region Background worker implementation

        public BackgroundWorker BgWorker { get; set; }


        //Want this make somehow defult function - C# 8.0 enables it inside Interface, but...
        public void SetWorkerCallbacks(BackgroundWorker worker)
        {
            worker.DoWork += DoWork;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += OnWorkerFinish;
        }
        //Want this make somehow defult function - C# 8.0 enables it
        public void RunWorker(BackgroundWorker worker, object beginingArgs)
        {
            OnBegining(beginingArgs);
            worker.RunWorkerAsync();
        }


        public void OnBegining(object args)
        {
  
        }

        public void OnWorkerFinish(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        public void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = DeviceListVM.StreamText;
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                
                (sender as BackgroundWorker).ReportProgress(0, "Done.");
                Thread.Sleep(200);
            }
        }

        #endregion

    }
}
