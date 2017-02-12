using System;
using RolePlayerEngine.Core;
using UnityEngine;

namespace RolePlayerEngine.Inventory.Models
{
    [Serializable]
    public abstract class Item : BaseBehavior
    {
        public string Name;
        public string Description;
        public Texture2D Icon;

        public void Awake()
        {
            Debug.Log("Adding Collider to: " + gameObject.name);
            gameObject.tag = "Item";
            gameObject.AddComponent<SphereCollider>();
        }
    }
}