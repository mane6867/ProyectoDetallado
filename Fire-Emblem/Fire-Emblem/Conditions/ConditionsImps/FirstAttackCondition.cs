namespace Fire_Emblem.Conditions.ConditionsImps;

public class FirstAttackCondition:Condition
{
    public override bool DoesHold(Character character)
    {
        return character.BattleContext.isFirstAttack;
    }
}