using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedPokemon = null;
    public SpriteRenderer selectedSprite;
    public Text moveName;
    public SpriteRenderer moveRange;
    public SpriteRenderer moveType;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (rayHit)
                {
                    if (rayHit.collider.tag == "Player")
                    {
                        Debug.Log(rayHit.transform.name);
                        GameObject hit = rayHit.collider.gameObject;
                        if (selectedPokemon == null && !hit.GetComponent<WorldMovement>().hasAttacked)
                        {
                            selectedSprite.sprite = hit.GetComponent<WorldMovement>().pokemon._base.FrontSprite;
                            moveName.text = hit.GetComponent<WorldMovement>().move.Name;
                            moveRange.sprite = hit.GetComponent<WorldMovement>().move.Range;
                            moveType.sprite = hit.GetComponent<WorldMovement>().move.TypeSprite;

                            hit.GetComponent<WorldMovement>().enabled = true;
                            hit.GetComponent<WorldMovement>().targetSquare.GetComponent<SpriteRenderer>().enabled = true;
                            selectedPokemon = hit;
                        }
                        else if (hit.name != selectedPokemon.name && !hit.GetComponent<WorldMovement>().hasAttacked)
                        {

                            selectedPokemon.GetComponent<WorldMovement>().targetSquare.GetComponent<SpriteRenderer>().enabled = false;
                            selectedPokemon.GetComponent<WorldMovement>().enabled = false;
                            if (selectedPokemon.GetComponent<WorldMovement>().hasAttacked)
                            {
                                selectedPokemon.GetComponent<WorldMovement>().sprite.color = Color.gray;
                            }
                            
                            selectedSprite.sprite = hit.GetComponent<WorldMovement>().pokemon._base.FrontSprite;
                            moveName.text = hit.GetComponent<WorldMovement>().move.Name;
                            moveRange.sprite = hit.GetComponent<WorldMovement>().move.Range;
                            moveType.sprite = hit.GetComponent<WorldMovement>().move.TypeSprite;

                            hit.GetComponent<WorldMovement>().enabled = true;
                            hit.GetComponent<WorldMovement>().targetSquare.GetComponent<SpriteRenderer>().enabled = true;
                            selectedPokemon = hit;
                        }
                    }
                }
                
            }
        }
    }
}
