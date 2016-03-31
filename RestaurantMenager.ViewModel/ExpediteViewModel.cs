using RestaurantManager.Models;
using RestaurantMenager.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RestaurantManager.ViewModels
{
    public class ExpediteViewModel : ViewModel
    {
        public ExpediteViewModel()
        {
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            DeleteAllOrdersCommand = new DelegateCommand<object>(DeleteAllOrders);
        }
        protected override void OnDataLoaded()
        {
            OrderItems = new ObservableCollection<Order>(base.Repository.Orders);
        }
        private ObservableCollection<Order> _orderItems;
        public ObservableCollection<Order> OrderItems
        {
            get { return _orderItems; }
            set
            {
                if (_orderItems != value)
                {
                    this._orderItems = value;
                    this.NotifyPropertyChanged();

                }
            }
        }

        public ICommand DeleteOrderCommand { get; private set; }
        private async void DeleteOrder(Order parameter)
        {
            RestaurantContext repo = await RestaurantContextFactory.GetRestaurantContextAsync();
            repo.Orders.Remove(parameter);
            OnDataLoaded();
        }

        public ICommand DeleteAllOrdersCommand { get; private set; }
        private async void DeleteAllOrders(object parameter)
        {
            RestaurantContext repo = await RestaurantContextFactory.GetRestaurantContextAsync();
            repo.Orders.Clear();
            OnDataLoaded();
        }
    }
}
