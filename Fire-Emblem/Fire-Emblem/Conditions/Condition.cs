namespace Fire_Emblem.Conditions;

public abstract class Condition
{
    public abstract bool DoesHold ( Character character, Character defender);
}