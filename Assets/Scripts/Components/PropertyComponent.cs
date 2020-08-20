using System.Collections.Generic;
using UnityEngine;

namespace MaltaTanks 
{
    [System.Serializable]
    public struct PropertyComponent
    {
        public float speed;
        public float armor;
        public float startHealth;
        [HideInInspector]
        public float health;
        public List<WeaponComponent> weapons;
        [HideInInspector]
        public WeaponComponent currentWeapon;
    }
}