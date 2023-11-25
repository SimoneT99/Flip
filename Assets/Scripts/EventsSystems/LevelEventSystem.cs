using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

/**
 * Classe che fornisce gli eventi che possono accadere durante un livello
 */
public class LevelEventSystem : MonoBehaviour
{
    /*Singletone pattern:
     * -accesso globale
     * -controllo not null
     * -elimina duplicati
     */
    private static LevelEventSystem _levelEventSystem;

    public static LevelEventSystem levelEventSystem { get { return _levelEventSystem; } }


    private void Awake()
    {
        if (_levelEventSystem != null && _levelEventSystem != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _levelEventSystem = this;
        }
    }

    /**
     * Evento per il raggiungimento del traguardo
     */
    public event Action OnEndLineTriggerEnter;
    public void EndLineTriggerEnter()
    {
        if (OnEndLineTriggerEnter != null)
        {
            OnEndLineTriggerEnter();
        }
    }


    /**
     * Evento per la morte del giocatore
     */
    public event Action OnPlayerDead;
    public void PlayerDead()
    {
        if (OnPlayerDead != null)
        {
            OnPlayerDead();
        }
    }

    /**
     * Evento per lo spawn del giocatore
     */
    public event Action<GameObject, string> OnPlayerSpawn;
    public void PlayerSpawn(GameObject player, String token)
    {
        if (OnPlayerSpawn != null)
        {
            OnPlayerSpawn(player, token);
        }
    }

     /**
     * Evento per quando il personaggio è spawnato
     */
    public event Action<GameObject, string> OnPlayerSpawned;
    public void PlayerSpawned(GameObject player, String token)
    {
        if (OnPlayerSpawned != null)
        {
            OnPlayerSpawned(player, token);
        }
    }


    /**
     * Evento per quando qualcosa muore nel gioco
     */
    public event Action<string> OnImDead;
    public void ImDead(string id)
    {
        if (OnImDead != null)
        {
            OnImDead(id);
        }
    }

    
}
