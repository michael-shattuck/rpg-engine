using RolePlayerEngine.Inventory.Enums;
using UnityEngine;

namespace RolePlayerEngine.Inventory.Models
{
    public abstract class WearableItem : Item
    {
        public WearableLocation WearableLocation;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
    }
}