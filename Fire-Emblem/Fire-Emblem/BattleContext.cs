namespace Fire_Emblem;

public class BattleContext
{
    public bool IsInitiator { get; set; }
    public bool IsFirstAttack { get; set; }
    public AttackType AttackType { get; set; }
    public Character LastOpponent { get; set; }
    public Character ActualOpponent { get; set; }
    public WeaponType WeaponType { get; set; }


    public BattleContext()
    {
        this.IsInitiator = false;
        this.IsFirstAttack = true;
        this.AttackType = AttackType.Physical;
        this.LastOpponent = null;
        this.ActualOpponent = null;
    }
    
}