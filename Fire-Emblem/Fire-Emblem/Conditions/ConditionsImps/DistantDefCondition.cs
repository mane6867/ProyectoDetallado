namespace Fire_Emblem.Conditions.ConditionsImps;

public class DistantDefCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return !character.BattleContext.IsInitiator && 
               (defender.Weapon == WeaponType.Magic || 
                defender.Weapon == WeaponType.Bow) ;
    }
}