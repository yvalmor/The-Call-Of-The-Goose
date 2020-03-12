using UnityEngine;

namespace Level
{
    public class SpawnObject : MonoBehaviour
    {
        public GameObject[] objects;

        private void Start()
        {
            int rand = Random.Range(0, objects.Length);
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }
    }
}
