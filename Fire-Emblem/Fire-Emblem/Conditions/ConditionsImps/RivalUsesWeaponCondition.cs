namespace Fire_Emblem.Conditions.ConditionsImps;

public class RivalUsesWeaponCondition: Condition
{
    private WeaponType _weaponType;
    
    public RivalUsesWeaponCondition(WeaponType weaponType)
    {
        _weaponType = weaponType;
    }
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.ActualOpponent.Weapon == _weaponType;
    }
}