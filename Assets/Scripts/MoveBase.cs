using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName;


    [SerializeField] MonsterType type;
    [SerializeField] int power;
    [SerializeField] Sprite rangeSprite;
    [SerializeField] Sprite typeSprite;

    public string Name { get { return moveName; } }

    public MonsterType Type { get { return type; } }

    public int Power { get { return power; } }

    public Sprite Range { get { return rangeSprite; } }

    public Sprite TypeSprite { get { return typeSprite; } }
}
