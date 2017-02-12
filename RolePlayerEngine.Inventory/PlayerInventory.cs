using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RolePlayerEngine.Core;
using RolePlayerEngine.Core.Events;
using RolePlayerEngine.Core.Events.EventArgs;
using RolePlayerEngine.Core.Extensions;
using RolePlayerEngine.Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace RolePlayerEngine.Inventory
{
    [Serializable]
    [AddComponentMenu("RolePlayerEngine/Inventory/PlayerInventory")]
    public class PlayerInventory : BaseBehavior
    {
        public ItemMount[] Mounts;
        private ICollection<Bag> _bags;

        public override void OnStart()
        {
            if (!Bags.Any()) Bags.Add(new Bag(30));
            InputEventManager.DoubleClick += HandleDoubleClick;
        }

        public ICollection<Bag> Bags => _bags ?? (_bags = new Collection<Bag>());
        public int ItemCount => Bags.Select(x => x.Items.Count).Sum();
        public int Limit => Bags.Select(x => x.Limit).Sum();
        public IEnumerable<InventoriedItem> AllItems => Bags.SelectMany(x => x.Items);

        public void PickUpItem(GameObject itemObject)
        {
            if (ItemCount >= Limit) throw new Exception("Your bags are full.");

            var itemComponent = itemObject.GetComponent<Item>();
            ResetItem(itemObject);

            if (AllItems.Any(x => x.Item.Name == itemComponent.Name))
            {
                AllItems.First(x => x.Item.Name == itemComponent.Name).Count++;
            }
            else
            {
                Bags.First().Items.Add(new InventoriedItem
                {
                    Item = itemComponent,
                    Count = 1
                });
                CreateInventoryImage(itemComponent);
            }

        }

        private static void ResetItem(GameObject itemObject)
        {
            Destroy(itemObject.GetComponent<Collider>());
            itemObject.transform.parent = null;
            itemObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            itemObject.SetActive(false);
        }

        private void CreateInventoryImage(Item item)
        {
            var icon = new GameObject();
            icon.AddComponent<Image>();
            icon.GetComponent<Image>().sprite = Sprite.Create(item.Icon,
                new Rect(0.0f, 0.0f, item.Icon.width, item.Icon.height), new Vector2(0.5f, 0.5f));
            icon.transform.SetParent(GameMaster.Instance.InventoryPanel.transform);

            icon.AddComponent<InventoryImage>();
            var component = icon.GetComponent<InventoryImage>();
            component.Item = item;
        }

        private void HandleDoubleClick(object sender, DoubleClickEventArgs e)
        {
            if (e.GameObject.HasComponent<Item>()) PickUpItem(e.GameObject);
            else if (e.GameObject.HasComponent<InventoryImage>()) EquipItem(e.GameObject);
        }

        public void EquipItem(GameObject itemObject)
        {
            var itemImage = itemObject.GetComponent<InventoryImage>();
            var item = itemImage.Item as WearableItem;
            if (item != null)
            {
                var mount = GetMount(item);
                var inventoryListing = AllItems.First(x => x.Item.Name == item.Name);
                if (inventoryListing.Count > 1) inventoryListing.Count--;
                else GetBagForItem(item).Items.Remove(inventoryListing);

                foreach (var currentItem in mount.GetChildrenWithComponent<WearableItem>())
                {
                    UnEquip(currentItem);
                }

                Destroy(item.gameObject.GetComponent<Collider>());
                item.gameObject.AttachToObject(mount, item.LocalRotation, item.LocalPosition);
                item.gameObject.layer = 2;
                item.gameObject.SetActive(true);
            }

            throw new Exception("This item cannot be equiped.");
        }

        private void UnEquip(GameObject item)
        {
            PickUpItem(item);
        }

        private GameObject GetMount(WearableItem item)
        {
            if (Mounts.Any(x => x.MountType == item.WearableLocation))
                return Mounts.First(x => x.MountType == item.WearableLocation).GameObject;

            throw new Exception("This mount has not been set up correctly: " + item.WearableLocation);
        }

        private Bag GetBagForItem(Item item)
        {
            return Bags.FirstOrDefault(x => x.Items.Any(entry => entry.Item.Name == item.Name));
        }
    }
}