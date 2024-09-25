namespace Fire_Emblem.Conditions.ConditionsImps;

public class UsesWeaponCondition: Condition
{
    private WeaponType _weaponType;
    public UsesWeaponCondition(WeaponType weaponType)
    {
        _weaponType = weaponType;
    }
    public override bool DoesHold(Character character)
    {
        return character.Weapon == _weaponType;
    }
}