using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachGengAtk : MonoBehaviour
{
    public TurnManager turnManager;
    public MoveBase move;
    public GameObject mouseManager;
    public DamageDetails damageDets;
    public int enemy;
    private WorldMovement target;

    public void Attack()
    {
        if(enemy == 1)
        {
            target = turnManager.target;
        }
        else
        {
            target = turnManager.target2;
        }
        TurnManager.attacked = 0;
        mouseManager.SetActive(false);

        damageDets = target.pokemon.TakeDamage(move);
        target.DisplayDmg(damageDets);
        target.UpdateHealth();


        foreach (WorldMovement member in turnManager.party)
        {
            if (member != null)
            {
                member.hasAttacked = false;
                member.sprite.color = Color.white;
            }   
        }

        mouseManager.SetActive(true);
    }
}
