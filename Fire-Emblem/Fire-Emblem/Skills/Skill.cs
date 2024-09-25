namespace Fire_Emblem.Skills;
using Fire_Emblem.Effects;
using Fire_Emblem.Conditions;

public class Skill { 
    private readonly Condition _condition;
    private readonly Effect _effect;
    private EffectType _attackType;
    public Effect Effect => _effect;
    public Skill ( Condition condition, Effect effect ) {
        _condition = condition;
        _effect = effect; 
    } 
    public void ApplyIfApplicable (Character attacker, Character defender) {
        Console.WriteLine($"Evaluando la condición de tipo: {_condition.GetType().Name}");
        if (_condition.DoesHold(attacker))
        {
            Console.WriteLine("lA CONDICIÓN SE CUMPLE");
            
            _effect.Apply(attacker, defender);
            AddDeltas(attacker, defender, _effect);
        }
        else{Console.WriteLine("A CONDICIÓN NO SE CUMPLE");}
    }
    public bool IsApplicable (Character attacker)
    {
        return _condition.DoesHold(attacker);
    }

    public void AddDeltas(Character character, Character defender, Effect effect)
    
    { //Console.WriteLine("estoy en add delta");
        //onsole.WriteLine($"Effect Type: {_effect.EffectType}, Target Stat: {_effect.TargetStat}");
        if (effect is CompositeEffect compositeEffect)
        {
            foreach (var innerEffect in compositeEffect.GetInnerEffects())
            {
                AddDeltas(character, defender, innerEffect); // Llamada recursiva para aplicar a los efectos internos
            }
        }
        else if (effect is WrathEffect wrathEffect)
        {
            
                // Incrementamos tanto ATK como SPD
                character.StatsBonus.Atk += effect.Bonus;
                character.StatsBonus.Spd += effect.Bonus;
        }
        else
        {
            if (effect.EffectType == EffectType.Bonus)
            {
                //Console.WriteLine("estoy en add delta bonuns");

                if (effect.TargetStat == StatType.Atk)
                {
                    character.StatsBonus.Atk += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Def)
                {
                    //Console.WriteLine("estoy en add delta def");
                    character.StatsBonus.Def += effect.Bonus;
                    //Console.WriteLine("se añade "+ _effect.Bonus + " al stats de bonus" );
                    //Console.WriteLine("el valor queda en " + character.StatsBonus.Def);
                }

                if (effect.TargetStat == StatType.Res)
                {
                    character.StatsBonus.Res += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Spd)
                {
                    character.StatsBonus.Spd += effect.Bonus;
                }

            }
            if (effect.EffectType == EffectType.PenaltyOwn)
            {
                //Console.WriteLine("estoy en add delta bonuns");

                if (effect.TargetStat == StatType.Atk)
                {
                    character.StatsPenalties.Atk += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Def)
                {
                    //Console.WriteLine("estoy en add delta def");
                    character.StatsPenalties.Def += effect.Bonus;
                    //Console.WriteLine("se añade "+ _effect.Bonus + " al stats de bonus" );
                    //Console.WriteLine("el valor queda en " + character.StatsBonus.Def);
                }

                if (effect.TargetStat == StatType.Res)
                {
                    character.StatsPenalties.Res += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Spd)
                {
                    character.StatsPenalties.Spd += effect.Bonus;
                }

            }

            if (effect.EffectType == EffectType.PenaltyRival)
            {
                //Console.WriteLine("estoy en add delta penalty");
                if (effect.TargetStat == StatType.Atk)
                {  
                    //Console.WriteLine("se añade al stats de penalty "+ effect.Bonus);
                    defender.StatsPenalties.Atk += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Def)
                {
                    defender.StatsPenalties.Def += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Res)
                {
                    defender.StatsPenalties.Res += effect.Bonus;
                }

                if (effect.TargetStat == StatType.Spd)
                {
                    defender.StatsPenalties.Spd += effect.Bonus;
                }

            }
        }
    }
}