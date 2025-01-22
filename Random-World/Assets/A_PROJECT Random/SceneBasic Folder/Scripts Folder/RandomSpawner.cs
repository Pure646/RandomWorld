using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class RandomSpawner : MonoBehaviour
    {
        // ���� ���� �ȿ���
        //  => ������ ��ġ�� ����
        //  => ������ ��ġ�� �׻� ���� ���� �־�� �Ѵ�.

        //public Vector3 pivot;
        //public float radius;

        public BoxCollider boundary;
        public GameObject spawnObject;

        private void Start()
        {
            spawnObject.gameObject.SetActive(false);
            RandomSpawn();
        }

        public void RandomSpawn()
        {
            for (int i = 0; i < 10; i++)
            {
                //Vector2 randomCircle = Random.insideUnitCircle * radius;
                //Vector3 randomPosition = pivot + new Vector3(randomCircle.x, 0, randomCircle.y);

                float x = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
                float z = Random.Range(boundary.bounds.min.z, boundary.bounds.max.z);
                Vector3 randomPosition = new Vector3(x, 0, z);

                GameObject newSpawnObject = Instantiate(spawnObject);
                newSpawnObject.gameObject.SetActive(true);
                newSpawnObject.transform.position = randomPosition;
            }
        }
    }
}
