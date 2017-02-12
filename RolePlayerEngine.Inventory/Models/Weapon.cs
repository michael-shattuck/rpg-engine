using RolePlayerEngine.Inventory.Enums;
using UnityEngine;

namespace RolePlayerEngine.Inventory.Models
{
    [AddComponentMenu("RolePlayerEngine/Inventory/Wearables/Weapon")]
    public class Weapon : WearableItem
    {
        public Animation BattleStance;
        public Animation AttackAnimation;
        public float DamageModifier;
        public float Range;
        public DamageType DamageType;
    }
}