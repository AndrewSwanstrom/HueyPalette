using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")) {
            Debug.Log("break");
            animator.SetBool("IsBroken", true);
            Invoke("DestroyThis", 0.34f);
        }
    }

    void DestroyThis(){
        Destroy(this.gameObject);
    }
}
