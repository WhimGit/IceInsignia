using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public MoveBase move;
    public DamageDetails damageDets;
    public bool reDamage = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.transform.name + " targeted.");
            damageDets = other.GetComponent<WorldMovement>().pokemon.TakeDamage(move);
            other.GetComponent<WorldMovement>().DisplayDmg(damageDets);
            other.GetComponent<WorldMovement>().UpdateHealth();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (reDamage)
        {
            Debug.Log(other.transform.name + " targeted.");
            damageDets = other.GetComponent<WorldMovement>().pokemon.TakeDamage(move);
            other.GetComponent<WorldMovement>().DisplayDmg(damageDets);
            other.GetComponent<WorldMovement>().UpdateHealth();
        }
    }
}
