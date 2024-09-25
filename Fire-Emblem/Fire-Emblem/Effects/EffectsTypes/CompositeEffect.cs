namespace Fire_Emblem.Effects;

public class CompositeEffect : Effect
{
    private readonly List<Effect> _effects;
    //ARREGLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAR BONUS = 0
    public CompositeEffect( params Effect[] effects): base(StatType.Hp, 0)
    {
        _effects = new List<Effect>(effects);
    }

    public override void Apply(Character character, Character defender)
    {
        foreach (var effect in _effects)
        {
            effect.Apply(character, defender);
        }
    }
    public IEnumerable<Effect> GetInnerEffects()
    {
        return _effects;
    }
}
//public class CompositeEffect : Effect
//{
//    private readonly Effect _firsteffect;
//    private readonly Effect _secondeffect;
//
//    public CompositeEffect(Effect firstEffect, Effect secondEffect): base(StatType.Atk, 0)
//    {
//        _firsteffect = firstEffect;
//        _secondeffect = secondEffect;
//    }
//
//    public override void Apply(Character character)
//    {
//        _firsteffect.Apply(character);
//        _secondeffect.Apply(character);
//    }
//}