using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NecrozmaCharge : MonoBehaviour
{
    public List<Tilemap> pillars = new List<Tilemap>();
    public List<int> selected = new List<int>();
    public List<WorldMovement> party = new List<WorldMovement>();
    public int pillarsLeft = 0;
    public int partyLeft = 0;
    public MoveBase move;
    public GameObject mouseManager;
    public DamageDetails damageDets;

    public void StopCharging()
    {
        GetComponent<Animator>().SetBool("moving", true);
    }

    public void ActivatePillars()
    {
        pillarsLeft = 0;
        foreach (Tilemap pillar in pillars)
        {
            if(pillar != null)
            {
                pillar.color = Color.magenta;
                pillarsLeft++;
            }
        }
    }

    public void DeactivatePillars()
    {
        foreach (Tilemap pillar in pillars)
        {
            if (pillar != null)
            {
                pillar.color = Color.white;
            }
        }
    }

    public void Attack()
    {
        TurnManager.attacked = 0;
        mouseManager.SetActive(false);
        selected = new List<int>();
        partyLeft = 0;
        foreach(WorldMovement member in party)
        {
            if(member != null)
            {
                partyLeft++;
                member.hasAttacked = false;
            }
        }

        if (partyLeft <= pillarsLeft)
        {
            foreach (WorldMovement member in party)
            {
                if (member != null)
                {
                    damageDets = member.pokemon.TakeDamage(move);
                    member.DisplayDmg(damageDets);
                    member.UpdateHealth();
                }
            }
        }
        else if (pillarsLeft >= 1)
        {
            for (int i = 0; i <= pillarsLeft; i++)
            {
                bool done = false;
                while (!done)
                {
                    int selector = Random.Range(0, 3 + 1);
                    if (party[selector] != null)
                    {
                        if (!selected.Contains(selector))
                        {
                            selected.Add(selector);
                            done = true;
                        }
                    }
                }
                
            }
            foreach (int q in selected)
            {
                damageDets = party[q].pokemon.TakeDamage(move);
                party[q].DisplayDmg(damageDets);
                party[q].UpdateHealth();
            }
        }
        else
        {
            int selector = Random.Range(0, 3 + 1);

            if(party[selector] != null)
            {
                damageDets = party[selector].pokemon.TakeDamage(move);
                party[selector].DisplayDmg(damageDets);
                party[selector].UpdateHealth();
                party[selector].hasAttacked = true;
                party[selector].sprite.color = Color.gray;
                TurnManager.attacked++;
            }
        }
        foreach(WorldMovement member in party)
        {
            if (member != null)
                if (member.hasAttacked == false)
                    member.sprite.color = Color.white;
        }
        mouseManager.SetActive(true);
    }
}
