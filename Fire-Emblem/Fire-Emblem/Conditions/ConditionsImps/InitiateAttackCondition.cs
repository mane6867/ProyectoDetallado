namespace Fire_Emblem.Conditions.ConditionsImps;

public class InitiateAttackCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        return character.BattleContext.IsInitiator; 
    }
}