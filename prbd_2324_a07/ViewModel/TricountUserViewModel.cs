using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_a07.ViewModel
{
    public class TricountUserViewModel : ViewModelCommon
    {

        private double _balanceUser;


        public double BalanceUser {

            get { return _balanceUser; }

            set { SetProperty(ref _balanceUser, value); }

        }


        private string _title;


        public string Title {

            get { return _title; }

            set { SetProperty(ref _title, value); }

        }


        private string _description;


        public string Description {

            get { return _description; }

            set { SetProperty(ref _description, value); }

        }
        public TricountUserViewModel() { }
    }
}
