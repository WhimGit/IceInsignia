using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static int attacked;
    public int partyMembers;
    public int enemiesLeft;
    public float timer = 0;
    public int stage;
    public GameObject Boss;
    public GameObject victoryScreen;
    public GameObject enemyTarget1;
    public GameObject enemyTarget1b;
    public GameObject enemyTarget2;
    public GameObject enemyTarget2b;

    public List<GameObject> enemies;

    public MouseManager mouseManager;

    // Start is called before the first frame update
    void Start()
    {
        attacked = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss == null)
        {
            Time.timeScale = 0;
            victoryScreen.SetActive(true);
            mouseManager.enabled = false;
        }
        if(attacked == partyMembers)
        {
            if (timer <= 0) //Sets a timer for a delay check that stops this from infinite looping
                timer = 1;

            timer -= Time.deltaTime;
            if(timer <= 0)   //Delay check for graying out last attacking pokemon and starting enemy turn
            {
                Debug.Log("Enemy Turn");
                attacked++;                 //Makes this if statement run only once
                mouseManager.selectedPokemon.GetComponent<WorldMovement>().sprite.color = Color.gray;
                mouseManager.selectedPokemon = null;
                EnemyTurn(stage);
            }
            
        }
    }

    void EnemyTurn(int stage)
    {
        switch (stage)
        {
            case 1:
                //code
                break;

            case 2:
                //code
                break;

            case 3:
                Boss.GetComponent<Animator>().SetBool("attacking", true);
                enemyTarget1.SetActive(!enemyTarget1.activeSelf);
                enemyTarget2.SetActive(!enemyTarget2.activeSelf);
                enemyTarget1b.SetActive(!enemyTarget1b.activeSelf);
                enemyTarget2b.SetActive(!enemyTarget2b.activeSelf);
                break;

            case 4:
                //code
                break;

            case 5:
                Boss.GetComponent<Animator>().SetBool("attacking", true);
                break;

            default:
                break;
        }
    }
}
