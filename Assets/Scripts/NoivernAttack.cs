using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoivernAttack : MonoBehaviour
{
    public WorldMovement machamp;
    public MoveBase move;
    public GameObject mouseManager;
    public DamageDetails damageDets;

    public void Attack()
    {
        TurnManager.attacked = 0;
        mouseManager.SetActive(false);

        damageDets = machamp.pokemon.TakeDamage(move);
        machamp.DisplayDmg(damageDets);
        machamp.UpdateHealth();

        machamp.hasAttacked = false;
        machamp.sprite.color = Color.white;
        
        mouseManager.SetActive(true);
    }
}
