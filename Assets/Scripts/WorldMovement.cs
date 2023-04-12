using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldMovement : MonoBehaviour
{
    public float speed;
    public LayerMask solidObjectLayer;
    public GameObject targetSquare;

    public bool rangedTargeting;
    public bool hasAttacked;
    private bool moving;
    private Vector2 userInput;

    private Animator anim;
    public SpriteRenderer sprite;
    public Pokemon pokemon;
    public MoveBase move;
    public DamageDetails damageDets;

    public Text hpText;
    public Text effectiveness;
    public Text damage;

    public TurnManager turnManager;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pokemon.HP = pokemon._base.MaxHp;
        UpdateHealth();
        this.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!moving)
        {
            userInput.x = Input.GetAxisRaw("Horizontal");
            userInput.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetBool("attacking", true);
                hasAttacked = true;
                GetComponent<WorldMovement>().enabled = false;
                if (targetSquare.GetComponent<Target>().hasTarget)
                {
                    damageDets = targetSquare.GetComponent<Target>().enemy.thisEnemy.TakeDamage(move);
                    targetSquare.GetComponent<Target>().enemy.DisplayDmg(damageDets);
                    targetSquare.GetComponent<Target>().enemy.UpdateHealth();
                    if (damageDets.Fainted)
                    {
                        Destroy(targetSquare.GetComponent<Target>().enemy.parentObject);
                    }
                }
                targetSquare.SetActive(false);
                TurnManager.attacked++;
            }

            if (userInput.x != 0)
                userInput.y = 0;

            if (userInput != Vector2.zero)
            {
                anim.SetFloat("moveX", userInput.x);
                anim.SetFloat("moveY", userInput.y);
                var targetPos = transform.position;
                targetPos.x += userInput.x;
                targetPos.y += userInput.y;
                
                if (rangedTargeting)
                {
                    var rangedTarget = targetPos;
                    if(userInput.x != 0)
                    {
                        if(userInput.x == 1)
                        {
                            rangedTarget.x++;
                            targetSquare.transform.position = rangedTarget;
                        }
                        else
                        {
                            rangedTarget.x--;
                            targetSquare.transform.position = rangedTarget;
                        }
                    }
                    else
                    {
                        if (userInput.y == 1)
                        {
                            rangedTarget.y++;
                            targetSquare.transform.position = rangedTarget;
                        }
                        else
                        {
                            rangedTarget.y--;
                            targetSquare.transform.position = rangedTarget;
                        }
                    }
                }
                else
                {
                    targetSquare.transform.position = targetPos;
                }

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }

            }
        }

        anim.SetBool("moving", moving);
        //anim.SetBool("attacking", attacking);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        moving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        moving = false;
    }

    bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UpdateHealth()
    {
        hpText.text = pokemon.HP + "/" + pokemon.MaxHp;
        if (pokemon.HP <= 0)
        {
            pokemon.HP = 0;
            turnManager.partyMembers--;
            Destroy(gameObject);
        }
    }

    public void DisplayDmg(DamageDetails details)
    {
        effectiveness.text = "x" + details.TypeEffectiveness;
        damage.text = "-" + details.Damage;
        StartCoroutine(EmptyDmg());
    }

    public IEnumerator EmptyDmg()
    {
        yield return new WaitForSeconds(2f);
        effectiveness.text = "";
        damage.text = "";
    }
}