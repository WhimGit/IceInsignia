using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public MoveBase move;
    public DamageDetails damageDets;

    public bool machampTarget = false;
    public bool noivernTarget = false;
    public bool azumarillTarget = false;
    public bool bisharpTarget = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.transform.name + " targeted.");
            damageDets = other.GetComponent<WorldMovement>().pokemon.TakeDamage(move);
            other.GetComponent<WorldMovement>().DisplayDmg(damageDets);
            other.GetComponent<WorldMovement>().UpdateHealth();
            switch (other.transform.name)
            {
                case "Machamp":
                    machampTarget = true;
                    break;

                case "Noivern":
                    noivernTarget = true;
                    break;

                case "Azumarill":
                    azumarillTarget = true;
                    break;

                case "Bisharp":
                    bisharpTarget = true;
                    break;

                default:
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch (other.transform.name)
            {
                case "Machamp":
                    machampTarget = false;
                    break;

                case "Noivern":
                    noivernTarget = false;
                    break;

                case "Azumarill":
                    azumarillTarget = false;
                    break;

                case "Bisharp":
                    bisharpTarget = false;
                    break;

                default:
                    break;      
            }
        } 
    }
}
