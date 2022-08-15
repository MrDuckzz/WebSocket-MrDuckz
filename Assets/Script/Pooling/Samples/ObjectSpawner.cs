using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pooling
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private EzPoolObject m_Prefab = default;

        [SerializeField]
        private Vector3 m_SpawnPosition = default;

        [SerializeField]
        // private float m_RandomRadius = default;


        public void Spawn()
        {
            Vector3 position = m_SpawnPosition;
            m_Prefab.Rent(position, Random.rotation, null);
        }
    }
}