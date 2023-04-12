using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAttacking : MonoBehaviour
{
    public void stopAttacking()
    {
        //WorldMovement.attacking = false;
        GetComponent<Animator>().SetBool("attacking", false);
        GetComponent<Animator>().SetBool("moving", false);
    }
}
