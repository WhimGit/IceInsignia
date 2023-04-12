using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool hasTarget = false;
    public EnemyInfo enemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.transform.parent.name + " targeted.");
            enemy = other.GetComponent<EnemyInfo>();
            hasTarget = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log(other.transform.parent.name + " untargeted.");
            hasTarget = false;
            enemy = null;
        }
    }
}
