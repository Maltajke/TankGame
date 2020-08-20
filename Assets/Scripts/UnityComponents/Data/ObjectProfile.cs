using UnityEngine;

namespace MaltaTanks
{
    [CreateAssetMenu]
    public class ObjectProfile : ScriptableObject
    {
        public GameObject prefab;
        public PropertyComponent properties;
    }
}

