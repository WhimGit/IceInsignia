using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public GameObject parentObject;
    public Pokemon thisEnemy;
    public Text hpText;

    public Text effectiveness;
    public Text damage;

    private IEnumerator coroutine;

    void Start()
    {
        thisEnemy.HP = thisEnemy._base.MaxHp;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        hpText.text = thisEnemy.HP + "/" + thisEnemy.MaxHp;
    }

    public void DisplayDmg(DamageDetails details)
    {
        switch (details.TypeEffectiveness)
        {
            case 0.25f:
                effectiveness.color = Color.blue;
                break;

            case 0.5f:
                effectiveness.color = Color.cyan;
                break;

            case 1f:
                effectiveness.color = Color.white;
                break;

            case 2f:
                effectiveness.color = Color.red;
                break;

            case 4f:
                effectiveness.color = Color.magenta;
                break;

            default:
                effectiveness.color = Color.gray;
                break;
        }
        effectiveness.text = "x" + details.TypeEffectiveness;
        damage.text = "-" + details.Damage;
        StartCoroutine(EmptyDmg());
    }
    
    public IEnumerator EmptyDmg()
    {
        yield return new WaitForSeconds(1f);
        effectiveness.text = "";
        damage.text = "";
    }
    
}
