using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] int count = 3;
    [SerializeField] float scaleMultiplier = 3;
    [SerializeField] float positionBounds = 10;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefabToSpawn, new Vector3(Random.Range(-positionBounds, positionBounds), 0, Random.Range(-positionBounds, positionBounds)), Random.rotation, transform) as GameObject;
            newObject.transform.localScale = Vector3.one * Random.Range(1f, scaleMultiplier);
        }
    }
}
