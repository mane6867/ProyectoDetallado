namespace Fire_Emblem.Effects;

public class CompositeEffect : Effect
{
    private readonly List<Effect> _effects;
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
