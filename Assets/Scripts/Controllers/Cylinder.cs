using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    //parametri settabili da editor
    [SerializeField] private float timeToKill = 3f; //tempo dopo il quale il barile scompare in caso di collisione
    [SerializeField] AudioClip CrashingBarrel; //clip audio da far partire in caso di collisione

    //serve per gestire la coroutine
    private IEnumerator killThis;

    //riferimento alla componente per emettere l'audio
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        killThis = KillThis();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //avviamo l'audio in qualsiasi caso
        _audioSource.PlayOneShot(CrashingBarrel, 0.5f);

        //andiamo a vedere se l'oggetto colpito può essere ucciso
        Killable toKill = collision.transform.GetComponent<Killable>();
        if (toKill!=null) //se toKill!=null allora l'oggetto può essere ucciso
        {
            toKill.kill(); //uccidiamo l'oggetto
            StartCoroutine(killThis); //avviamo la coroutine che gestirà la scomparsa del cilindro
        }
    }

    //coroutine per la gestione della scomparsa del barile dopo un certo intervallo di tempo
    private IEnumerator KillThis()
    {
        //aspettiamo timeToKill secondi
        yield return new WaitForSeconds(timeToKill);

        //distruggiamo l'oggetto
        Destroy(this.gameObject);
    }
}
