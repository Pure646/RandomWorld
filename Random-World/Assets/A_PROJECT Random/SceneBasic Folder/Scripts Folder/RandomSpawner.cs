using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class RandomSpawner : MonoBehaviour
    {
        // 일정 범위 안에서
        //  => 랜덤한 위치에 생성
        //  => 랜덤한 위치는 항상 발판 위에 있어야 한다.

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
