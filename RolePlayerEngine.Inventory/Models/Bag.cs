using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RolePlayerEngine.Inventory.Models
{
    [Serializable]
    public class Bag
    {
        private ICollection<InventoriedItem> _items;

        public Bag()
        {
        }

        public Bag(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; set; }

        public ICollection<InventoriedItem> Items
        {
            get { return _items ?? (_items = new Collection<InventoriedItem>()); }
            set { _items = value; }
        }
    }
}