namespace Fire_Emblem;
public class Stats
{
    public int HpMax = 0;
    public int Hp
    {
        get { return _hp; }
        set {
            _hp = Math.Clamp(value, 0, HpMax); }  // Aseguramos que no se pueda asignar un valor negativo
    }
    private int _hp = 0;
    public int Atk = 0; 
    public int Spd = 0; 
    public int Res = 0; 
    public int Def = 0;
    public int GetStat(StatType statType)
    {
        return statType switch
        {
            StatType.HpMax => HpMax,
            StatType.Hp => Hp,
            StatType.Atk => Atk,
            StatType.Spd => Spd,
            StatType.Res => Res,
            StatType.Def => Def,
        };
    }
    
    public Stats Clone()
    {
        return new Stats
        {
            Atk = this.Atk,
            Def = this.Def,
            Res = this.Res,
            Spd = this.Spd
        };
    }

    public void SetDefault()
    {
        Atk = 0;
        Def = 0;
        Res = 0;
        Spd = 0;
    }
 }