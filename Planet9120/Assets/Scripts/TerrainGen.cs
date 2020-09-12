using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Completed
{
    public class TerrainGen : MonoBehaviour
    {
        
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count (int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int Columns = 8;
        public int rows = 8;

        public GameObject[] groundTiles;
        public GameObject[] Resources;

        private Transform TerrainHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void InitialiseList()
        {
            gridPositions.Clear();
            for(int x = 1; x < Columns - 1; x++)
            {
                for(int y = 1; y < rows -1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        void TerrainSetup()
        {
            TerrainHolder = new GameObject("Land").transform;
            for(int x = -1; x < Columns + 1; x++)
            {
                for(int y = -1; y < rows + 1; y++)
                {
                    GameObject toInstanitate = groundTiles[Random.Range(0, groundTiles.Length)];
                    GameObject instance = Instantiate(toInstanitate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(TerrainHolder);
                }
            }
        }

        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }

        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for(int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }



    }

}

