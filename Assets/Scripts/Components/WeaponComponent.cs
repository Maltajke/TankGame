using UnityEngine;

namespace MaltaTanks
{
    [System.Serializable]
    public struct WeaponComponent
    {
        public ForGunType gunType;
        public GameObject prefab;
        public Transform bulletStart;
        public int bulletSpeed;        
        public BulletComponent bulletType;
    }
}
