namespace Fire_Emblem.Conditions.ConditionsImps;

public class SameOpponentCondition: Condition

{
    public override bool DoesHold(Character character, Character defender)
    {
        return defender == character.BattleContext.LastOpponent;
    }
}