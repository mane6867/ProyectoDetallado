using Fire_Emblem_View;

namespace Fire_Emblem.Effects;

public abstract class Effect
{
    public EffectType EffectType;
    public EffectDuration EffectDuration;
    protected int _bonus;
    protected EffectType effectType;
    protected EffectDuration effectDuration;
    public int Bonus => _bonus;
    public StatType TargetStat;
    

    public Effect(StatType statType, int bonus)
    {
        TargetStat = statType;
        _bonus = bonus;
        
    }
    
    public abstract void Apply (Character character, Character defender);
    public void SetBonus( int bonus)
    {
        _bonus = bonus;
    }
 }
