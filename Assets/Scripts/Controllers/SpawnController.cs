using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //riferimento al transform del punto nel quale il player va spawnato
    [SerializeField] private Transform _spawnPoint;
    private void Start()
    {
        LevelEventSystem.levelEventSystem.OnPlayerSpawn += spawnPlayer; //il metodo spawn player viene associato all'evento OnPlayerSpawn
    }
    
    //Se chiamato l'oggetto player viene spawnato
    private void spawnPlayer(GameObject player, string token) 
    {
        GameObject newPlayer = Instantiate(player, _spawnPoint.position, _spawnPoint.rotation); //spawniamo il player
        LevelEventSystem.levelEventSystem.PlayerSpawned(newPlayer, token); // notifichiamo che il player è stato spawnato attraverso il metodo PlayerSpawned() dell'EventSystem
    }
}
