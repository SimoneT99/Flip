using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeadZoneTrigger : MonoBehaviour
    {
        public string playerLayer = "Player";
        private void OnTriggerEnter(Collider other)
        {
            Killable toKill = other.GetComponent<Killable>();
            //se qualcosa di killabile entra lo uccidiamo
            if(toKill != null)
            {
                toKill.kill();
            }
        }
    }
}