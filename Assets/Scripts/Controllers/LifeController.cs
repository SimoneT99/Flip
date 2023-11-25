using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe che implementa l'interfaccia Killable per gestire la morte del personaggio
public class LifeController : MonoBehaviour, Killable
{
    [SerializeField] private string ID = "abc";

    //riferimenti alle componenti con cui interagire
    [SerializeField] private AudioSource source; //per l'audio 
    [SerializeField] private CharacterAnimation characterAnimation; // per l'animazione di morte
    [SerializeField] private BodyController bodyController; //per bloccare il movimento in caso di morte (altrimenti si potrebbe muovere il personaggio durnte l'animazione di morte)

    //Metodo dell'interfaccia Killable, se chiamato gestisce la morte del personaggio
    public void kill()
    {
        StartCoroutine(Die()); //avviamo la coroutine
    }

    //coroutine per la gestione della morte del personaggio
    private IEnumerator Die()
    {
        Debug.Log("Im dead, ID:" + ID);
        characterAnimation.PlayDeathAnimation(); //chiediamo al gestore dell'animazione di eseguire l'animazione di morte
        bodyController.LockMovement(); //chiediamo al BodyController di bloccare il movimento del personaggio
        source.Play();  //avviamo l'effetto sonoro
        yield return new WaitForSeconds(2f); //aspettiamo qualche secondo
        Destroy(this.gameObject); //distruggiamo l'oggetto
        LevelEventSystem.levelEventSystem.ImDead(ID); //avvisiamo attraverso l'evento ImDead che questo personaggio è morto
    }
}
