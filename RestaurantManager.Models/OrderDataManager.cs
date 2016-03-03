using System.Collections.Generic;

namespace RestaurantManager.Models
{
    public class OrderDataManager : DataManager
    {       
        protected override void OnDataLoaded()
        {
            this.MenuItems = base.Repository.StandardMenuItems;

            this.CurrentlySelectedMenuItems = new List<MenuItem>
            {
                this.MenuItems[3],
                this.MenuItems[5]
            };
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
                    this.OnPropertyChanged();
                }
            }
        }
        private List<MenuItem> _currentlySelectedMenuItems = null;
        public List<MenuItem> CurrentlySelectedMenuItems
        {
            get
            {
                return this._currentlySelectedMenuItems;
            }
            set
            {
                if (_currentlySelectedMenuItems!=value)
                {
                    this._currentlySelectedMenuItems = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
