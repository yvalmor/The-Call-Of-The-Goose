using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class SpawnObject : MonoBehaviour
    {
        public GameObject[] objects;
        private int min, max;

        private void Awake()
        {
            
        }

        private void Start()
        {
            int rand = Random.Range(min, max);
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }
    }
}
