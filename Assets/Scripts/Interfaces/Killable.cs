using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interfaccia per la gestione dell'uccisione di oggetti, se uno script la implementa quando verra chiamato il metodo kill verrà gestita la morte dell'oggetto con eventuali animazioni, delay, etc...
public interface Killable
{
    //Se chiamato uccide l'oggetto con lo script attaccato
    public void kill();
}

