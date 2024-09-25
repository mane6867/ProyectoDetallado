namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Battle
{
    private View _view;
    private Utilities _utilities;
    private List<List<Character>> _teams;
    private Character _attackerCharacter;
    
    private Character _defenderCharacter;
    private BattleContext _battleContext;

    public Battle(View view, List<List<Character>> teams)
    {
        _view = view;
        _utilities = new Utilities(view);
        _teams = teams;


    }

    public (int attackerIndex, int defenderIndex) GetIndexAttackerDefender(int round)
    {
        int attackerIndex = round % 2 == 0 ? 2 : 1;
        int defenderIndex = 3 - attackerIndex;
        return (attackerIndex, defenderIndex);
    }

    public void PrintCaseOfAdvantage()
    {
        if (_attackerCharacter.HasAdvantage(_defenderCharacter))
        {
            //Console.WriteLine("el atacante tiene ventaja con" + _attackerCharacter.Weapon+ "sobre" + _defenderCharacter.Weapon);
            _view.WriteLine(_attackerCharacter.Name + " (" + _attackerCharacter.Weapon + ") tiene ventaja con respecto a " + _defenderCharacter.Name + " (" + _defenderCharacter.Weapon + ")");
        }
        
        else if (_defenderCharacter.HasAdvantage(_attackerCharacter))
        {
            //Console.WriteLine("el defensor tiene ventaja con" + _defenderCharacter.Weapon+ "sobre" + _attackerCharacter.Weapon);
            _view.WriteLine(_defenderCharacter.Name + " (" + _defenderCharacter.Weapon + ") tiene ventaja con respecto a " + _attackerCharacter.Name + " (" + _attackerCharacter.Weapon + ")");
        }
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
    }

    public void HandleDamageFight()
    {
        //Console.WriteLine("Estoy en HandleDamage Fight");
        int damage = _attackerCharacter.CalculateDamage(_defenderCharacter);
        
        //Console.WriteLine("Las stats originales son:");
        //Console.WriteLine(_attackerCharacter._originalStats.Atk);
        //Console.WriteLine(_attackerCharacter._originalStats.Def);
        //Console.WriteLine(_attackerCharacter._originalStats.Res);
        //Console.WriteLine(_attackerCharacter._originalStats.Spd);
        //Console.WriteLine("Las stats actuales son:");
        //Console.WriteLine(_attackerCharacter.Stats.Atk);
        //Console.WriteLine(_attackerCharacter.Stats.Def);
        //Console.WriteLine(_attackerCharacter.Stats.Res);
        //Console.WriteLine(_attackerCharacter.Stats.Spd);
        _view.WriteLine(_attackerCharacter.Name + " ataca a "+ _defenderCharacter.Name + " con " + damage + " de daño");
        _attackerCharacter.ApplyDamage(damage, _defenderCharacter);
    }

    public bool IsDuelOver()
    {
        return _teams.Any(team => team.Any(character => character.Stats.Hp == 0));
    }

    public void DeleteCharactersFromTeams()
    {
        //Console.WriteLine("Estoy en eliminar caracter");
        foreach (var team in _teams)
        {
            team.RemoveAll(character => character.Stats.Hp == 0);
        }
    }
    private void SwapCharacters()
    {
        (_attackerCharacter, _defenderCharacter) = (_defenderCharacter, _attackerCharacter);
    }

    

    //public void HandleSkills(Character character)
    //{
    //    var listOfSkills = character.GetSkills();
    //    foreach (var skill in listOfSkills)
    //    {
    //        skill.ApplyIfApplicable(character);
    //    }
    //    
    //}
    public void HandleDuelSequence()
    {
        //Console.WriteLine("Estoy en handel duel sequence");
        _attackerCharacter.SetIsNotFirstAttack();  //    REVISAR SI VA ACÁ
        HandleDamageFight();
        //Console.WriteLine("El duelo se acabó?" + IsDuelOver());
        if (!IsDuelOver())
        {   
            //Console.WriteLine("se esta cambiando acá");
            SwapCharacters();
            _attackerCharacter.SetIsNotFirstAttack();
            HandleDamageFight();
        }
        
    }

    public void StartDuel()
    {
        _attackerCharacter.ApplySkills(_defenderCharacter);
        _defenderCharacter.ApplySkills(_attackerCharacter);

        
        bool attackerIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_defenderCharacter);
        bool defenderIsAbleToFollowUp = _defenderCharacter.IsAbleToFollowUp(_attackerCharacter);
        
        PrintCaseOfAdvantage();
        
        _attackerCharacter.PrintSkillsEffects(_view);
        _attackerCharacter.PrintSkillsNeutralized( _view);
        _defenderCharacter.PrintSkillsEffects(_view);
        _defenderCharacter.PrintSkillsNeutralized(_view);
        _attackerCharacter.ResetStatsIfNeutralized();
        _defenderCharacter.ResetStatsIfNeutralized();
        if (_attackerCharacter.AreBonusSkillsNeutralized)
        {
            Console.WriteLine("dentro del if que se debe restaurar el is able to follow up");
            attackerIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_defenderCharacter);
        }
        if (_defenderCharacter.AreBonusSkillsNeutralized)
        {
            defenderIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_attackerCharacter);
        }
        HandleDuelSequence();
        //Console.WriteLine("el duelo se acabó?" + IsDuelOver());
        //Console.WriteLine("el attack puede "+ attackerIsAbleToFollowUp);
        //Console.WriteLine("el defensor puede " + defenderIsAbleToFollowUp);
        
        if (!IsDuelOver())
        {

            if (attackerIsAbleToFollowUp)
            {
                SwapCharacters();
                HandleDamageFight();
            }

            else if (defenderIsAbleToFollowUp)
            {
                //Console.WriteLine("atacará  antes" + _attackerCharacter.Name);
                //Console.WriteLine("defenderá  antes" + _defenderCharacter.Name);
                //Console.WriteLine("se está cambiando aqui");
                //Console.WriteLine("atacará " + _attackerCharacter.Name);
                //Console.WriteLine("defenderá " + _defenderCharacter.Name);
                HandleDamageFight();
            }
            else
            {
                _view.WriteLine("Ninguna unidad puede hacer un follow up");
            }
        }
    }

    public bool isRoundOver()
    {
        //Console.WriteLine("Estoy en isRound Over");
        return IsTeam1Empty() || IsTeam2Empty();
    }

    public bool IsTeam1Empty()
    {
        return  _teams[0].Count == 0;
    }
    public bool IsTeam2Empty()
    {
        return  _teams[1].Count == 0;
    }

    public void ReportRoundOutcome(int indexAttacker, string nameAttacker)
    {
        
        var team = _teams[indexAttacker-1];
        
        var attackerCharacter = team.FirstOrDefault(character => character.Name == nameAttacker);

        if (attackerCharacter != _attackerCharacter)
        {
            SwapCharacters();
        }
        _view.WriteLine(_attackerCharacter.Name +" (" + _attackerCharacter.Stats.Hp + ") : " + _defenderCharacter.Name + " (" + _defenderCharacter.Stats.Hp + ")" );
        
    }

    public void ReportWhoWon()
    {
        if (IsTeam1Empty())
        {
            _view.WriteLine("Player 2 ganó");
        }
        else
        {
            _view.WriteLine("Player 1 ganó");
        }
    }
    public void UpdateLastOponents()
    {
        _attackerCharacter.SetLastOpponent(_defenderCharacter);
        _defenderCharacter.SetLastOpponent(_attackerCharacter);
    }
    public bool Fight(int round)
    {
        (int attackerIndex, int defenderIndex) = GetIndexAttackerDefender(round);
        
        _attackerCharacter = AskUserToSelectUnidad(attackerIndex);
        _defenderCharacter = AskUserToSelectUnidad(defenderIndex);
        
        Character attacker = _attackerCharacter;
        Character defender = _defenderCharacter;

        _attackerCharacter.SetActualOpponent(_defenderCharacter);
        _defenderCharacter.SetActualOpponent(_attackerCharacter);
        
        _attackerCharacter.IsInitiator();
        _defenderCharacter.IsNotInitiator();
        
        _attackerCharacter.SetAttackType();
        _defenderCharacter.SetAttackType();
        
        

        string attackerName = _attackerCharacter.Name;
        
        
        _view.WriteLine("Round " + round + ": " + _attackerCharacter.Name + " (Player "+ attackerIndex+") comienza");
        
        StartDuel();
        DeleteCharactersFromTeams();
        ReportRoundOutcome(attackerIndex, attackerName);
        if (!isRoundOver())
        {
            UpdateLastOponents();
            _attackerCharacter.RestoreAllStats();
            _defenderCharacter.RestoreAllStats();
            _attackerCharacter.UnNeutralizeBonusSkills();
            _attackerCharacter.SetFalseArePenaltysSkillsNeutralized();
            _defenderCharacter.UnNeutralizeBonusSkills();
            _defenderCharacter.SetFalseArePenaltysSkillsNeutralized();
            
            Fight(round + 1);
        }
        else
        {
            //Console.WriteLine("Estoy en Fight over");
            ReportWhoWon();
        }
        
        return true;
    }

    
    public Character AskUserToSelectUnidad(int int_player)
    {
        _view.WriteLine("Player "+int_player+" selecciona una opción");

        int indice = 0;
        foreach (Character Jugador in _teams[int_player-1])
        {
            String nombreJugador = Jugador.Name;
            _view.WriteLine(indice + ": "+ nombreJugador);
            indice++;
        }
        // se le pide  seleccionar una unidad
        int selected = _utilities.AskUserToSelectNumber(0, indice);
        
        Character unidad = _teams[int_player - 1][selected];

        return unidad;
    }

}