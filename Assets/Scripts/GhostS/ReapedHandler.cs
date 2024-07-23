using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapedHandler : MonoBehaviour
{
    public int ID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ReapArea")
        {
            GameObject soul = this.gameObject;
            Parameters param = new Parameters();

            param.PutGameObjectExtra(GhostManager.SOUL, soul);

            EventBroadcaster.Instance.PostEvent(EventNames.Reap_Events.ON_REAP, param);

            param = new Parameters();
            param.PutExtra(ShineBehavior.ID, this.ID);
            EventBroadcaster.Instance.PostEvent(EventNames.Light_Events.ON_OUT, param);

            this.gameObject.GetComponent<Animator>().SetBool("isPossessed", false);

            //removing hitbox
            BoxCollider hitBox = this.gameObject.GetComponent<BoxCollider>();
            if (hitBox != null ) Destroy(hitBox);

            GhostManager.Instance.ReapAttempts++;

            Destroy(this);
        }
    }
}
