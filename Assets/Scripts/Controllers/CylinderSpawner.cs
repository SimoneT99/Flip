using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSpawner : MonoBehaviour
{
    
    [SerializeField] private Object cylinder; //l'oggetto da spawnare
    [SerializeField] public float delay; //il delay tra un barile e l'altro
    private Transform _transform; //riferimento al transform dello spawner

    public void Start()
    {
        _transform = GetComponent<Transform>();
        StartCoroutine("SpawnCylinder"); //avviamo la coroutine per lo spawn
    }

    private IEnumerator SpawnCylinder()
    {
        while (true) //lo spawn andrà avanti all'infinito
        {
            Instantiate(cylinder, _transform.position, _transform.rotation); //istanziamo il cilinro
            yield return new WaitForSeconds(delay); //aspettiamo delay tempo prima di spawnarne un'altro
        }
    }
}
