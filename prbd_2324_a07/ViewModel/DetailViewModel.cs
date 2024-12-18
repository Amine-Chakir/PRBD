using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using prbd_2324_a07.Model;
using System.Collections.ObjectModel;

namespace prbd_2324_a07.ViewModel
{
    public class DetailViewModel : ViewModelCommon
    {

        private ObservableCollectionFast<User> _users ;

        public ObservableCollectionFast<User> Users {
            get => _users;
            set => SetProperty(ref _users, value);
        }
        private ObservableCollectionFast<TricountUserViewModel> _tricounts = new();

        public ObservableCollectionFast<TricountUserViewModel> Tricounts {
            get => _tricounts;
            set => SetProperty(ref _tricounts, value);
        }

        private Double _balanceUser;
        public Double BalanceUser {
            get => _balanceUser;
            set => SetProperty(ref _balanceUser, value);
        }


        private User _selectedUser;

        public User SelectedUser {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value, () => OnRefreshData()); }
        }

        public DetailViewModel() {

          Users =  new ObservableCollectionFast<User>(
                 Context.Users
             .OrderBy(u => u.Full_name));

            OnRefreshData();
        }

        
        protected override void OnRefreshData() {
            RaisePropertyChanged();
            if(SelectedUser != null) {

                var tricounts = Context.Tricounts.Where(t => t.Participants.Any(p => p.Id == SelectedUser.Id));

                Tricounts = new ObservableCollectionFast<TricountUserViewModel>(
                    tricounts.Select(t =>
                    new TricountUserViewModel() {
                        Title = t.Title,
                        Description= t.Description,
                        BalanceUser = t.GetBalanceByUser(SelectedUser.Id)
                    }
                        ));
                    
            }
        }


    }
}
