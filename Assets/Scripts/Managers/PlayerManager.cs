using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//La responsabilit� di questo manager è di gestire la vita del player, controllando quando muore gestendo l'eventuale respawn
public class PlayerManager : MonoBehaviour
{
    /*Singletone pattern:
    * -accesso globale
    * -controllo not null
    * -elimina duplicati
    */
    private static PlayerManager _playerManager;

    public static PlayerManager playerManager { get { return _playerManager; } }


    private void Awake()
    {
        if (_playerManager != null && _playerManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _playerManager = this;
        }
    }

    ///////////////////////////////////////////

    //Oggetto Player
    [SerializeField] private GameObject player;

    [SerializeField] private ActiveCharacter activeCharacter;

    //Id del player
    [SerializeField] private string playerID = "player";

    public void Start()
    {
        player = activeCharacter.GetActiveCharacter();
        LevelEventSystem.levelEventSystem.OnImDead += SomethingDied; // il metodo SomethingDied viene associato all'evento OnImDead, possiamo tenere d'occhio quindi la vita del player
        LevelEventSystem.levelEventSystem.OnPlayerSpawned += SomethingSpawned; // il metodo SomethingSpawned viene associato all'evento OnImDead, possiamo tenere d'occhio lo spawn
    }

    //Metodo legato all'evento OnImDead, se la stringa in input corrisponde all'ID del player allora il player è morto e dobbiamo agire di conseguenza
    public void SomethingDied(string theDead)
    {
        if (playerID.Equals(theDead))
        {
            LevelManagerBasic.levelManagerBasic.NotifyPlayerIsDead();
        }
    }

    //Token per gestire lo spawn del player
    private string playerToken;

    //metodo per lo spawn del player
    public void SpawnPlayer()
    {
        //Dobbiamo dire allo spawner di spawnare il player
        playerToken = Random.Range(0, 1000).ToString(); //generiamo un token per tenere traccia dello spawn
        LevelEventSystem.levelEventSystem.PlayerSpawn(player, playerToken); // chiamiamo l'evento PlayerSpawn, lo spawner è collegato allora il player verra spawnato
    }

    //metodo per tenere traccia dello spawn del player, legato all'evento on player spawned
    private void SomethingSpawned(GameObject objectReference, string token)
    {
        //se token è uguale a playerToken allora significa che la richiesta di spawnare il player è stata accettata, dobbiamo preparare la camera
        if (token.Equals(playerToken))
        {
            SetCamera(objectReference);
        }
    }

    //metodo ausigliari per settare la camera sul gameObject passato come argomento
    private void SetCamera(GameObject thePlayer)
    {
        CameraManager.cameraManager.SetThisAsFollow(thePlayer.transform); //Chiediamo al camera manager di settare il Follow della camera sul player
        CameraManager.cameraManager.SetThisAsTarget(thePlayer.transform.Find("CameraTarget")); //Chiediamo al camera manager di settare il Target della camera sul player
        Debug.Log("chiamando il centring");
        CameraManager.cameraManager.CenterCameraOnTarget(thePlayer.transform); //Chiediamo al camera manager di centrare la camera sul player
    }
}
