using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


[CreateAssetMenu()]
//Scriptable object per contenere le impostazioni audio durante il gioco
public class ActiveCharacter : ScriptableObject
{
    //Oggetti e relativi ID
    [SerializeField]
    private GameObject Eve;
    [SerializeField]
    private string EveId = "EVE";

    [SerializeField]
    private GameObject Crypto;
    [SerializeField]
    private string CryptoId = "CRYPTO";

    //riferimento all oggetto attivo ed il suo id
    private GameObject activeCharacter;
    private string activeCharacterName;

    //Ritorna il GameObject del player attivo in questo momento
    public GameObject GetActiveCharacter()
    {
        if (activeCharacter!=null)
        {
            return activeCharacter;
        }
        else
        {
            return Eve;
        }
    }

    //Ritorna l'ID del player attivo in questo momento
    public string GetActiveCharacterName()
    {
        if (activeCharacter != null)
        {
            return activeCharacterName;
        }
        else
        {
            return EveId;
        }
    }

    //Dato un'ID setta il game object relativo come attivo
    public void SetActiveCharacter(string characterID)
    {
        if (characterID.Equals(EveId)) //EVE
        {
            activeCharacter = Eve;
            activeCharacterName = EveId;
        }
        else if (characterID.Equals(CryptoId)) //CRYPTO
        {
            activeCharacter = Crypto;
            activeCharacterName = CryptoId;
        }
    }
}

