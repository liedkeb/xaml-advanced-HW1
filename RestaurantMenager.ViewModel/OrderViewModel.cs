using RestaurantManager.Models;
using RestaurantMenager.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace RestaurantManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {       
        public OrderViewModel()
        {
            this.CurrentlySelectedMenuItems = new ObservableCollection<MenuItem>();
            AddToOrderCommand = new DelegateCommand<object>(AddToOrder);
            SubmitOrderCommand = new DelegateCommand<object>(SubmitOrder);
        }
        protected override void OnDataLoaded()
        {
            this.MenuItems = base.Repository.StandardMenuItems;

        }

        private List<MenuItem> _menuItems = null;
        public List<MenuItem> MenuItems
        {
            get
            {
                return this._menuItems;
            }
            set
            {
                if (this._menuItems!=value)
                {
                    this._menuItems = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<MenuItem> CurrentlySelectedMenuItems { get; private set; }
        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                NotifyPropertyChanged();
            }
        }
        private string _specialRequests;
        public string SpecialRequests
        {
            get { return _specialRequests; }
            set
            {
                if (_specialRequests != value)
                {
                    _specialRequests = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand AddToOrderCommand { get; private set; }
        public ICommand SubmitOrderCommand { get; private set; }

        private void AddToOrder (object parameter)
        {
            if (SelectedMenuItem!=null)
                this.CurrentlySelectedMenuItems.Add(this.SelectedMenuItem);
        }
        private async void SubmitOrder(object parameter)
        {
            if (CurrentlySelectedMenuItems.Count > 0)
            {
                RestaurantContext repo = await RestaurantContextFactory.GetRestaurantContextAsync();
                repo.Orders.Add(new Order
                {
                    Items = CurrentlySelectedMenuItems.ToList(),
                    Complete = false,
                    Id = repo.Orders.Count,
                    Table = repo.Tables.First(),
                    Expedite = false,
                    SpecialRequests = this.SpecialRequests

                });
                CurrentlySelectedMenuItems.Clear();
                SpecialRequests = "";
                new Windows.UI.Popups.MessageDialog("Order has been submitted!").ShowAsync();
            }
        }
    }
}
