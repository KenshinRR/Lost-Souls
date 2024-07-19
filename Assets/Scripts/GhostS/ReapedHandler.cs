using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapedHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ReapArea")
        {
            GhostManager.Instance.Possessed.Remove(this.gameObject);
            this.gameObject.GetComponent<Animator>().SetBool("isPossessed", false);

            //removing hitbox
            BoxCollider hitBox = this.gameObject.GetComponent<BoxCollider>();
            if (hitBox != null ) Destroy(hitBox);
                

            Destroy(this);
        }
    }
}
