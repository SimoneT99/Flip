using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe per gestire la distruzione di tutti gli oggetti che passano in un trigger, se hanno una componente Killable verranno trattati attraverso quella
public class KillSpecial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Killable toKill = other.transform.GetComponent<Killable>(); //prendiamo se ce l'ha la componente Killable
        if (toKill != null) //se toKill != null allora lo chiamiamo il metodo kill() che gestirà la morte dell'oggetto
        {
            toKill.kill();
        }
        else{
            Destroy(other.gameObject); // toKill == null distruggiamo normalmente l'oggetto
        }
    }
}
