using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FinishLineTrigger : MonoBehaviour
    {
        public LayerMask playerLayer;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.layer.ToString());
            Debug.Log(playerLayer.ToString());
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                //usiamo il metodo del GameEvents per attivare l'evento
                Debug.Log("player etered ending line");
                LevelEventSystem.levelEventSystem.EndLineTriggerEnter();
            }          
        }
    }
}