namespace Fire_Emblem.Effects;

public class WrathEffect : Effect
{
    private readonly int _maxBonus = 30;


    public WrathEffect(): base(StatType.Atk, 0)
    {
        effectType = EffectType.Bonus;
        effectDuration = EffectDuration.WholeBattle;
    }

    public override void Apply(Character character, Character defender)
    {
        // Calcular los puntos de HP perdidos
        int lostHp = character.Stats.HpMax - character.Stats.Hp;

        // Calcular el bono, asegurando que no exceda el máximo
        int bonus = Math.Min(lostHp, _maxBonus);
        _bonus = bonus;

        // Aplicar el bono a Atk y Spd
        character.Stats.Atk += bonus;
        character.Stats.Spd += bonus;
    }
}