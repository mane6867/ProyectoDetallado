namespace Fire_Emblem.Conditions.ConditionsImps;

public class ChaosStyleCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.IsInitiator && 
               (character.BattleContext.AttackType == AttackType.Physical && 
                defender.Weapon == WeaponType.Magic) ||
               character.BattleContext.IsInitiator && 
               (character.BattleContext.AttackType == AttackType.Magical &&  
                defender.Weapon != WeaponType.Magic);
    }
}