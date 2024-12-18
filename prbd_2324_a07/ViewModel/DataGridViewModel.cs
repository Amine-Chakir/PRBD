using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;
using prbd_2324_a07.Model;
using prbd_2324_a07.View;
using PRBD_Framework;


namespace prbd_2324_a07.ViewModel
{
    public class DataGridViewModel : DialogViewModelBase<Tricount,PridContext>
    {


        private ObservableCollection<User> _allUsers;

        public ObservableCollection<User> AllUsers {
            get => _allUsers;
            set => SetProperty(ref _allUsers, value);
        }

        public ICommand showDetails { get; set; }

        private ObservableCollectionFast<Tricount> _tricounts = new();

        public ObservableCollectionFast<Tricount> Tricounts {
            get => _tricounts;
            set => SetProperty(ref _tricounts, value);
        }

        private ObservableCollectionFast<Operation> _operations = new();

        public ObservableCollectionFast<Operation> Operations {
            get => _operations;
            set => SetProperty(ref _operations, value);
        }

        private User _selectedUser;

        public User SelectedUser {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        public ICommand OpenDialog { get; set; } 

        private Tricount _selectedTricount;

        public Tricount SelectedTricount {
            get { return _selectedTricount; }
            set { SetProperty(ref _selectedTricount, value,()=>OnRefreshData()); }
        }
        public ICollectionView TrciountsView => Tricounts.GetCollectionView(nameof(DateTime), ListSortDirection.Descending);
        public DataGridViewModel() {

            AllUsers = new ObservableCollection<User>(
                Context.Users.OrderBy(u => u.Full_name).ToList()
                 );

            showDetails = new RelayCommand(showDetail,CanShowDetail);

            OpenDialog = new RelayCommand<Tricount>(OpenDialogMethod);

            RaisePropertyChanged();

        }
        private void OpenDialogMethod(Tricount tricount) {
            App.ShowDialog<TricountDialogViewModel, Tricount,PridContext>(tricount);
        }
        private bool CanShowDetail() {
            return SelectedUser != null;
        }

        private void showDetail() {

            Tricounts.RefreshFromModel(Tricount.GetTricountsByUser(SelectedUser.Id));
                   
            SelectedUser = null;
            OnRefreshData();
        }

       

        protected override void OnRefreshData() {
         
            RaisePropertyChanged(nameof(TrciountsView));
            if(SelectedTricount != null) {
                Operations.RefreshFromModel(Context.Operations.Where(o => o.Tricount == SelectedTricount.Id));
            }
        }
    }
}
