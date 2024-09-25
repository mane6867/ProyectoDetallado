namespace Fire_Emblem.Conditions.ConditionsImps;

public class UsesMagicCondition : Condition
{
    public override bool DoesHold(Character character)
    {
        return character.BattleContext.attackType == AttackType.Magical;
    }
}