namespace Fire_Emblem.Conditions.ConditionsImps;

public class InitiateAttackCondition: Condition
{
    public override bool DoesHold(Character character, Character defender)
    {
        Console.WriteLine("dentro de la condición se revisa si inició" + character.Name + " y es " + character.BattleContext.isInitiator);
        return character.BattleContext.isInitiator; 
    }
}