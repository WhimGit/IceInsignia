using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static int attacked;
    public int partyMembers;
    public int enemiesLeft;
    public float timer = 1;
    public int stage;
    public GameObject Boss;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public DamageDetails damageDets;

    public WorldMovement target;
    public WorldMovement target2;
    public List<GameObject> enemies;
    public List<WorldMovement> party;

    public MouseManager mouseManager;

    // Start is called before the first frame update
    void Start()
    {
        attacked = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(partyMembers == 0)
        {
            Time.timeScale = 0;
            defeatScreen.SetActive(true);
            mouseManager.enabled = false;
        }
        if (Boss == null || enemiesLeft == 0)
        {
            Time.timeScale = 0;
            victoryScreen.SetActive(true);
            mouseManager.enabled = false;
        }
        if(attacked == partyMembers)
        {
            if (timer <= -1) //Sets a timer for a delay check that stops this from infinite looping
                timer = 1;

            timer -= Time.deltaTime;
            if(timer <= 0)   //Delay check for graying out last attacking pokemon and a further delay for starting enemy turn
            {
                Debug.Log("Enemy Turn");
                if(mouseManager.selectedPokemon != null)
                {
                    mouseManager.selectedPokemon.GetComponent<WorldMovement>().sprite.color = Color.gray;
                    mouseManager.selectedPokemon = null;
                    mouseManager.selectedSprite.sprite = null;
                    mouseManager.moveName.text = "";
                    mouseManager.moveRange.sprite = null;
                    mouseManager.moveType.sprite = null;
}
                if(timer <= -1)
                {
                    attacked++;                 //Breaks out of this if statement tree
                    EnemyTurn(stage);
                }
            }
            
        }
    }

    void EnemyTurn(int stage)
    {
        switch (stage)
        {
            case 1:
                var pos = party[0].gameObject.transform.position;
                var targPos = Boss.transform.position;

                if (pos.y <= 2)
                {
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveY", -1);
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveX", 0);
                    targPos.y = pos.y + 2;
                    targPos.x = pos.x;
                    Boss.transform.position = targPos;
                    Boss.GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                }
                else if (pos.x <= 2.5)
                {
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveX", -1);
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                    targPos.y = pos.y;
                    targPos.x = pos.x + 2;
                    Boss.transform.position = targPos;
                    Boss.GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                }
                else
                {
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveX", 1);
                    Boss.GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                    targPos.y = pos.y;
                    targPos.x = pos.x - 2;
                    Boss.transform.position = targPos;
                    Boss.GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                }

                break;

            case 2:
                if(enemies[0] != null)
                {
                    target = null;
                    while(target == null)
                    {
                        target = party[Random.Range(0, 1 + 1)];
                    }
                    
                    var pos2 = target.gameObject.transform.position;
                    var northTarg = enemies[0].transform.position;
                    var eastTarg = enemies[0].transform.position;
                    var westTarg = enemies[0].transform.position;
                    northTarg.y = pos2.y + 1;
                    northTarg.x = pos2.x;
                    eastTarg.y = pos2.y;
                    eastTarg.x = pos2.x + 1;
                    westTarg.y = pos2.y;
                    westTarg.x = pos2.x - 1;

                    bool northValid = true;
                    bool eastValid = true;
                    bool westValid = true;

                    if (party[0] != null)
                    {
                        if (northTarg == party[0].transform.position)
                            northValid = false;
                        if (eastTarg == party[0].transform.position)
                            eastValid = false;
                        if (westTarg == party[0].transform.position)
                            westValid = false;
                    }
                    if (party[1] != null)
                    {
                        if (northTarg == party[1].transform.position)
                            northValid = false;
                        if (eastTarg == party[1].transform.position)
                            eastValid = false;
                        if (westTarg == party[1].transform.position)
                            westValid = false;
                    }
                    if (enemies[1] != null)
                    {
                        if (northTarg == enemies[1].transform.position)
                            northValid = false;
                        if (eastTarg == enemies[1].transform.position)
                            eastValid = false;
                        if (westTarg == enemies[1].transform.position)
                            westValid = false;
                    }


                    if (pos2.y <= 2.5 && northValid)
                    {
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveY", -1);
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveX", 0);
                        enemies[0].transform.position = northTarg;
                        enemies[0].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                    else if (pos2.x <= 3.5 && eastValid)
                    {
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveX", -1);
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                        enemies[0].transform.position = eastTarg;
                        enemies[0].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                    else if (westValid)
                    {
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveX", 1);
                        enemies[0].GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                        enemies[0].transform.position = westTarg;
                        enemies[0].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                }
                if (enemies[1] != null) 
                {
                    target2 = null;
                    while (target2 == null)
                    {
                        target2 = party[Random.Range(0, 1 + 1)];
                    }

                    var pos3 = target2.gameObject.transform.position;
                    var northTarg2 = enemies[1].transform.position;
                    var eastTarg2 = enemies[1].transform.position;
                    var westTarg2 = enemies[1].transform.position;
                    northTarg2.y = pos3.y + 1;
                    northTarg2.x = pos3.x;
                    eastTarg2.y = pos3.y;
                    eastTarg2.x = pos3.x + 1;
                    westTarg2.y = pos3.y;
                    westTarg2.x = pos3.x - 1;

                    bool northValid2 = true;
                    bool eastValid2 = true;
                    bool westValid2 = true;

                    if (party[0] != null)
                    {
                        if (northTarg2 == party[0].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == party[0].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == party[0].transform.position)
                            westValid2 = false;
                    }
                    if (party[1] != null)
                    {
                        if (northTarg2 == party[1].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == party[1].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == party[1].transform.position)
                            westValid2 = false;
                    }
                    if (enemies[0] != null)
                    {
                        if (northTarg2 == enemies[0].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == enemies[0].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == enemies[0].transform.position)
                            westValid2 = false;
                    }



                    if (pos3.y <= 2.5 && northValid2)
                    {
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveY", -1);
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveX", 0);
                        enemies[1].transform.position = northTarg2;
                        enemies[1].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                    else if (pos3.x <= 3.5 && eastValid2)
                    {
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveX", -1);
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                        enemies[1].transform.position = eastTarg2;
                        enemies[1].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                    else if (westValid2)
                    {
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveX", 1);
                        enemies[1].GetComponent<WorldMovement>().anim.SetFloat("moveY", 0);
                        enemies[1].transform.position = westTarg2;
                        enemies[1].GetComponent<WorldMovement>().anim.SetBool("attacking", true);
                    }
                }
                
                break;

            case 3:
                Boss.GetComponent<Animator>().SetBool("attacking", true);
                if (enemies[1].GetComponentInChildren<EnemyTarget>().machampTarget || enemies[2].GetComponentInChildren<EnemyTarget>().machampTarget)
                {
                    damageDets = party[0].pokemon.TakeDamage(enemies[2].GetComponentInChildren<EnemyTarget>().move);
                    party[0].DisplayDmg(damageDets);
                    party[0].UpdateHealth();
                }
                if (enemies[1].GetComponentInChildren<EnemyTarget>().noivernTarget || enemies[2].GetComponentInChildren<EnemyTarget>().noivernTarget)
                {
                    damageDets = party[1].pokemon.TakeDamage(enemies[2].GetComponentInChildren<EnemyTarget>().move);
                    party[1].DisplayDmg(damageDets);
                    party[1].UpdateHealth();
                }
                if (enemies[1].GetComponentInChildren<EnemyTarget>().azumarillTarget || enemies[2].GetComponentInChildren<EnemyTarget>().azumarillTarget)
                {
                    damageDets = party[2].pokemon.TakeDamage(enemies[2].GetComponentInChildren<EnemyTarget>().move);
                    party[2].DisplayDmg(damageDets);
                    party[2].UpdateHealth();
                }
                if (enemies[1].GetComponentInChildren<EnemyTarget>().bisharpTarget || enemies[2].GetComponentInChildren<EnemyTarget>().bisharpTarget)
                {
                    damageDets = party[3].pokemon.TakeDamage(enemies[2].GetComponentInChildren<EnemyTarget>().move);
                    party[3].DisplayDmg(damageDets);
                    party[3].UpdateHealth();
                }
                break;

            case 4:
                if (enemies[0] != null)
                {
                    target = null;
                    while (target == null)
                    {
                        target = party[Random.Range(0, 2 + 1)];
                    }

                    var pos2 = target.gameObject.transform.position;
                    var northTarg = enemies[0].transform.position;
                    var eastTarg = enemies[0].transform.position;
                    var westTarg = enemies[0].transform.position;
                    northTarg.y = pos2.y + 1;
                    northTarg.x = pos2.x;
                    eastTarg.y = pos2.y;
                    eastTarg.x = pos2.x + 1;
                    westTarg.y = pos2.y;
                    westTarg.x = pos2.x - 1;

                    bool northValid = true;
                    bool eastValid = true;
                    bool westValid = true;

                    if (party[0] != null)
                    {
                        if (northTarg == party[0].transform.position)
                            northValid = false;
                        if (eastTarg == party[0].transform.position)
                            eastValid = false;
                        if (westTarg == party[0].transform.position)
                            westValid = false;
                    }
                    if (party[1] != null)
                    {
                        if (northTarg == party[1].transform.position)
                            northValid = false;
                        if (eastTarg == party[1].transform.position)
                            eastValid = false;
                        if (westTarg == party[1].transform.position)
                            westValid = false;
                    }
                    if (party[2] != null)
                    {
                        if (northTarg == party[2].transform.position)
                            northValid = false;
                        if (eastTarg == party[2].transform.position)
                            eastValid = false;
                        if (westTarg == party[2].transform.position)
                            westValid = false;
                    }
                    if (enemies[1] != null)
                    {
                        if (northTarg == enemies[1].transform.position)
                            northValid = false;
                        if (eastTarg == enemies[1].transform.position)
                            eastValid = false;
                        if (westTarg == enemies[1].transform.position)
                            westValid = false;
                    }


                    if (pos2.y <= 3.5 && northValid)
                    {
                        enemies[0].GetComponent<Animator>().SetFloat("moveY", -1);
                        enemies[0].GetComponent<Animator>().SetFloat("moveX", 0);
                        enemies[0].transform.position = northTarg;
                        enemies[0].GetComponent<Animator>().SetBool("attacking", true);
                    }
                    else if (pos2.x <= 8.5 && eastValid)
                    {
                        enemies[0].GetComponent<Animator>().SetFloat("moveX", -1);
                        enemies[0].GetComponent<Animator>().SetFloat("moveY", 0);
                        enemies[0].transform.position = eastTarg;
                        enemies[0].GetComponent<Animator>().SetBool("attacking", true);
                    }
                    else if (westValid)
                    {
                        enemies[0].GetComponent<Animator>().SetFloat("moveX", 1);
                        enemies[0].GetComponent<Animator>().SetFloat("moveY", 0);
                        enemies[0].transform.position = westTarg;
                        enemies[0].GetComponent<Animator>().SetBool("attacking", true);
                    }
                }

                if (enemies[1] != null)
                {
                    target2 = null;
                    while (target2 == null)
                    {
                        target2 = party[Random.Range(0, 2 + 1)];
                    }

                    var pos3 = target2.gameObject.transform.position;
                    var northTarg2 = enemies[1].transform.position;
                    var eastTarg2 = enemies[1].transform.position;
                    var westTarg2 = enemies[1].transform.position;
                    northTarg2.y = pos3.y + 1;
                    northTarg2.x = pos3.x;
                    eastTarg2.y = pos3.y;
                    eastTarg2.x = pos3.x + 1;
                    westTarg2.y = pos3.y;
                    westTarg2.x = pos3.x - 1;

                    bool northValid2 = true;
                    bool eastValid2 = true;
                    bool westValid2 = true;

                    if (party[0] != null)
                    {
                        if (northTarg2 == party[0].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == party[0].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == party[0].transform.position)
                            westValid2 = false;
                    }
                    if (party[1] != null)
                    {
                        if (northTarg2 == party[1].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == party[1].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == party[1].transform.position)
                            westValid2 = false;
                    }
                    if (enemies[0] != null)
                    {
                        if (northTarg2 == enemies[0].transform.position)
                            northValid2 = false;
                        if (eastTarg2 == enemies[0].transform.position)
                            eastValid2 = false;
                        if (westTarg2 == enemies[0].transform.position)
                            westValid2 = false;
                    }



                    if (pos3.y <= 3.5 && northValid2)
                    {
                        enemies[1].GetComponent<Animator>().SetFloat("moveY", -1);
                        enemies[1].GetComponent<Animator>().SetFloat("moveX", 0);
                        enemies[1].transform.position = northTarg2;
                        enemies[1].GetComponent<Animator>().SetBool("attacking", true);
                    }
                    else if (pos3.x <= 8.5 && eastValid2)
                    {
                        enemies[1].GetComponent<Animator>().SetFloat("moveX", -1);
                        enemies[1].GetComponent<Animator>().SetFloat("moveY", 0);
                        enemies[1].transform.position = eastTarg2;
                        enemies[1].GetComponent<Animator>().SetBool("attacking", true);
                    }
                    else if (westValid2)
                    {
                        enemies[1].GetComponent<Animator>().SetFloat("moveX", 1);
                        enemies[1].GetComponent<Animator>().SetFloat("moveY", 0);
                        enemies[1].transform.position = westTarg2;
                        enemies[1].GetComponent<Animator>().SetBool("attacking", true);
                    }
                }

                break;

            case 5:
                Boss.GetComponent<Animator>().SetBool("attacking", true);
                break;

            default:
                break;
        }
    }
}
