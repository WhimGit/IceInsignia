using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldMovement : MonoBehaviour
{
    public float speed;
    public LayerMask solidObjectLayer;
    public GameObject pauseMenu;

    //public event Action OnEncountered;

    private bool moving;
    //static public bool attacking { get; set; }
    private Vector2 userInput;

    private Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        if (!moving)
        {
            userInput.x = Input.GetAxisRaw("Horizontal");
            userInput.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetBool("attacking", true);
                //attacking = false;
                //anim.Play("AttackDown");
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
}