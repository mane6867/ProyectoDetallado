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

    private (int attackerIndex, int defenderIndex) GetIndexAttackerDefender(int round)
    {
        int attackerIndex = round % 2 == 0 ? 2 : 1;
        int defenderIndex = 3 - attackerIndex;
        return (attackerIndex, defenderIndex);
    }

    private void PrintCaseOfAdvantage()
    {
        if (_attackerCharacter.HasAdvantage(_defenderCharacter)) _view.WriteLine(_attackerCharacter.Name + 
            " (" + _attackerCharacter.Weapon + ") tiene ventaja con respecto a " + _defenderCharacter.Name +
            " (" + _defenderCharacter.Weapon + ")");
        
        else if (_defenderCharacter.HasAdvantage(_attackerCharacter)) _view.WriteLine(_defenderCharacter.Name +
            " (" + _defenderCharacter.Weapon + ") tiene ventaja con respecto a " + _attackerCharacter.Name + 
            " (" + _attackerCharacter.Weapon + ")");
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
    }

    private void PrintDamage(int damage)
    {
        _view.WriteLine(_attackerCharacter.Name + " ataca a "+ _defenderCharacter.Name + " con " + damage +
                        " de daño");
    }

    private void ApplyDamageToCharacter(int damage)
    {
        _attackerCharacter.ApplyDamage(damage, _defenderCharacter);
    }

    private void HandleDamageFight()
    {
        int damage = _attackerCharacter.CalculateDamage(_defenderCharacter);
        PrintDamage(damage);
        ApplyDamageToCharacter(damage);
    }

    private bool IsDuelOver()
    {
        return _teams.Any(team => team.Any(character => character.Stats.Hp == 0));
    }

    private void DeleteCharactersFromTeams()
    {
        foreach (var team in _teams)
        {
            team.RemoveAll(character => character.Stats.Hp == 0);
        }
    }
    private void SwapCharacters()
    {
        (_attackerCharacter, _defenderCharacter) = (_defenderCharacter, _attackerCharacter);
    }
    
    private void HandleDuelSequence()
    {
        _attackerCharacter.SetIsNotFirstAttack(); 
        HandleDamageFight();
        if (!IsDuelOver())
        {   
            SwapCharacters();
            _attackerCharacter.SetIsNotFirstAttack();
            HandleDamageFight();
        }
    }

    private void SimulateApplySkills()
    {
        
        _attackerCharacter.SimulateApplySkills(_defenderCharacter);
        _defenderCharacter.SimulateApplySkills(_attackerCharacter);
    }

    private void PrintSkillsStatus()
    {
        _attackerCharacter.PrintSkillsEffects(_view);
        _attackerCharacter.PrintSkillsNeutralized(_view);
        _defenderCharacter.PrintSkillsEffects(_view);
        _defenderCharacter.PrintSkillsNeutralized(_view);
    }
    public void StartDuel()
    {
        SimulateApplySkills();
        PrintCaseOfAdvantage();
        
        PrintSkillsStatus();
        _attackerCharacter.ResetStatsIfNeutralized();
        _defenderCharacter.ResetStatsIfNeutralized();
        
        _attackerCharacter.ApplyDefinitiveSkills();
        _defenderCharacter.ApplyDefinitiveSkills();
        
        var attackerIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_defenderCharacter);
        var defenderIsAbleToFollowUp = _defenderCharacter.IsAbleToFollowUp(_attackerCharacter);
        if (_attackerCharacter.AreBonusSkillsNeutralized)
        {
            Console.WriteLine("se está en el if así que se va a ver si puede hacer follow up otra vez");
            attackerIsAbleToFollowUp = _attackerCharacter.IsAbleToFollowUp(_defenderCharacter);
        }
        if (_defenderCharacter.AreBonusSkillsNeutralized)
        {   Console.WriteLine("se está en el if así que se va a ver si puede hacer follow up otra vez");
            defenderIsAbleToFollowUp = _defenderCharacter.IsAbleToFollowUp(_attackerCharacter);
        }
        HandleDuelSequence();
        
        if (!IsDuelOver())
        {

            if (attackerIsAbleToFollowUp)
            {
                SwapCharacters();
                HandleDamageFight();
            }

            else if (defenderIsAbleToFollowUp)
            {
                HandleDamageFight();
            }
            else
            {
                _view.WriteLine("Ninguna unidad puede hacer un follow up");
            }
        }
    }

    public bool IsRoundOver()
    {
        //Console.WriteLine("Estoy en isRound Over");
        return IsTeam1Empty() || IsTeam2Empty();
    }

    private bool IsTeam1Empty()
    {
        return  _teams[0].Count == 0;
    }
    private bool IsTeam2Empty()
    {
        return  _teams[1].Count == 0;
    }

    private void ReportRoundOutcome(int indexAttacker, string nameAttacker)
    {
        var winnerTeam = _teams[indexAttacker-1];
        var winnerCharacter = winnerTeam.FirstOrDefault(character => character.Name == nameAttacker);

        if (winnerCharacter != _attackerCharacter) SwapCharacters();
        
        _view.WriteLine(_attackerCharacter.Name +" (" + _attackerCharacter.Stats.Hp + ") : " +
                        _defenderCharacter.Name + " (" + _defenderCharacter.Stats.Hp + ")" );
    }
    private void ReportWhoWon()
    {
        if (IsTeam1Empty()) _view.WriteLine("Player 2 ganó");
        else _view.WriteLine("Player 1 ganó");
    }
    private void UpdateLastOpponents()
    {
        _attackerCharacter.SetLastOpponent(_defenderCharacter);
        _defenderCharacter.SetLastOpponent(_attackerCharacter);
    }
    public bool Fight(int round)
    {
        (int attackerIndex, int defenderIndex) = GetIndexAttackerDefender(round);
        
        _attackerCharacter = AskUserToSelectUnit(attackerIndex);
        _defenderCharacter = AskUserToSelectUnit(defenderIndex);
        
        Character attacker = _attackerCharacter;
        Character defender = _defenderCharacter;

        _attackerCharacter.SetActualOpponent(_defenderCharacter);
        _defenderCharacter.SetActualOpponent(_attackerCharacter);
        
        _attackerCharacter.IsInitiator();
        _defenderCharacter.IsNotInitiator();

        string attackerName = _attackerCharacter.Name;
        
        
        _view.WriteLine("Round " + round + ": " + _attackerCharacter.Name + " (Player "+ attackerIndex+") comienza");
        
        StartDuel();
        DeleteCharactersFromTeams();
        ReportRoundOutcome(attackerIndex, attackerName);
        if (!IsRoundOver())
        {
            UpdateLastOpponents();
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

    
    public Character AskUserToSelectUnit(int int_player)
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