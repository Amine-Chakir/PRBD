using prbd_2324_a07.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_a07.ViewModel
{
    public class TricountDialogViewModel : DialogViewModelBase<Tricount, PridContext>
    {

        private string _title;

        public string Title {

            get { return _title; }
            set { SetProperty(ref _title, value); }
            
            }

        public TricountDialogViewModel(Tricount tricount) {
            
            Title = tricount.Title;
            RaisePropertyChanged();
            OnRefreshData();
        }

        protected override void OnRefreshData() {

            RaisePropertyChanged();
        }
    }
}
