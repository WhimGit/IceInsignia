using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    [SerializeField] public PokemonBase _base;

    public PokemonBase Base
    {
        get
        {
            return _base;
        }
    }

    public int HP { get; set; }

    public MoveBase LearnedMove { get; set; }

    public void Init()
    {
        HP = MaxHp;

        LearnedMove = Base.Move;
    }

    public int MaxHp
    {
        get { return Base.MaxHp; }
    }

    public DamageDetails TakeDamage(MoveBase move)
    {
        float type = TypeChart.GetEffectiveness(move.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Fainted = false,
            Damage = 0
        };

        float modifiers = Random.Range(0.85f, 1.15f) * type;
        int damage = Mathf.FloorToInt(move.Power * modifiers);

        HP -= damage;
        damageDetails.Damage = damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }
        return damageDetails;
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }

    public float TypeEffectiveness { get; set; }

    public int Damage { get; set; }
}
