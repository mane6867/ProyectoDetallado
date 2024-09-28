namespace Fire_Emblem.Effects;

public class WrathEffect : Effect
{
    private readonly int _maxBonus = 30;


    public WrathEffect(): base(StatType.Atk, 0)
    {
        EffectType = Fire_Emblem.EffectType.Bonus;
        EffectDuration = Fire_Emblem.EffectDuration.WholeBattle;
    }

    public override void Apply(Character character, Character defender)
    {
        int lostHp = character.Stats.HpMax - character.Stats.Hp;
        
        int bonus = Math.Min(lostHp, _maxBonus);
        _bonus = bonus;
        
        character.StatsBonus.Atk += bonus;
        character.StatsBonus.Spd += bonus;
    }
}