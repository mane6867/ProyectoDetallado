namespace Fire_Emblem;

public class BattleContext
{
    public bool isInitiator { get; set; }
    public bool isFirstAttack { get; set; }
    public AttackType attackType { get; set; }
    public Character lastOpponent { get; set; }
    public Character actualOpponent { get; set; }
    public WeaponType weaponType { get; set; }


    public BattleContext()
    {
        this.isInitiator = false;
        this.isFirstAttack = true;
        this.attackType = AttackType.Physical;
        this.lastOpponent = null;
        this.actualOpponent = null;
    }
    
}