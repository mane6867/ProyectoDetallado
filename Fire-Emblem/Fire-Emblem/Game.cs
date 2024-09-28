using System.Runtime.InteropServices;
using Fire_Emblem_View;

using Newtonsoft.Json;
using System.Text.Json;

using System.Text.RegularExpressions;
namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Utilities _utilities;
    private List<Character> _allCharacters = new List<Character>();

    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _utilities = new Utilities(_view);

    }
    

    private void DisplayAvailableFiles()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        string[] files = Directory.GetFiles(_teamsFolder);
        for (int i = 0; i < files.Length; i++)
        {
            _view.WriteLine(i + ": " + Path.GetFileName(files[i]));
        }
    }
    
    private string[] GetSelectedInfo()
    {
        string pathChosenFile = AskToChoosePathFile();
        string[] infoTeamsChosenFile = File.ReadAllLines(pathChosenFile);
        return infoTeamsChosenFile;

    }

    public string AskToChoosePathFile()
    {
        string[] files = Directory.GetFiles(_teamsFolder);
        int min = 0;
        int max = files.Length - 1;
        int numberOfSelectedFile = _utilities.AskUserToSelectNumber(min, max);
        return files[numberOfSelectedFile];
    }

    private static bool HasMaxTwoSkills(List<string> listOfSkills)
    {
        return listOfSkills.Count <= 2;
    }

    public void GetCharacters()
    {
        string myJson = File.ReadAllText("characters.json");
        _allCharacters = JsonConvert.DeserializeObject<List<Character>>(myJson);
    }
    private List<string> ExtractSkills(string skillString)
    {
        return skillString.TrimEnd(')').Split(',')
            .Select(skill => skill.Trim())
            .ToList();
    }
    private Character FindCharacterByName(string name)
    {
        return _allCharacters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
    private string ExtractCharacterName(string characterData, int skillStartIndex)
    {
        return skillStartIndex != -1 
            ? characterData.Substring(0, skillStartIndex).Trim() 
            : characterData.Trim();
    }
    private (string name, List<string> skills) ParseCharacterData(string characterData)
    {
        int skillStartIndex = characterData.IndexOf('(');
        
        string characterName = ExtractCharacterName(characterData, skillStartIndex);
        
        List<string> characterSkills = skillStartIndex != -1 
            ? ExtractSkills(characterData.Substring(skillStartIndex + 1)) 
            : new List<string>();

        return (characterName, characterSkills);
    }
    
    public List<Character> CreateCharactersFromNames(List<string> dataCharactersPlayer)
    {
        List<Character> characters = new List<Character>();
        GetCharacters();
        foreach (var characterData in dataCharactersPlayer)
        {
            var characterInfo = ParseCharacterData(characterData);
            Character character = FindCharacterByName(characterInfo.name);

            character.AddSkills(characterInfo.skills);
            characters.Add(character);
        }
        return characters;

    }
    public bool IsValidFileInfo(string[] infoTeams)
    {
        (List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2) =
            ObtainDataCharactersFromTeams(infoTeams);
        return InfoContainTwoPlayers(infoTeams) && ValidateTeams(dataCharactersPlayer1, dataCharactersPlayer2);

    }
    private List<List<Character>> CreateTeamsFromFile(string[] fileInfo)
    {
        var team1 = CreateCharactersFromNames(ObtainDataCharactersFromTeams(fileInfo).dataCharactersPlayer1);
        var team2 = CreateCharactersFromNames(ObtainDataCharactersFromTeams(fileInfo).dataCharactersPlayer2);

        return new List<List<Character>> { team1, team2 };
    }
    private void StartBattle(List<List<Character>> teams)
    {
        Battle battle = new Battle(_view, teams);
        battle.Fight(1);
    }
    public void Play()
    {
        DisplayAvailableFiles();
        string[] selectedFileInfo = GetSelectedInfo();

        if (!IsValidFileInfo(selectedFileInfo))
        {
            _view.WriteLine("Archivo de equipos no válido");
            return; // Salida temprana si el archivo no es válido
        }

        var teams = CreateTeamsFromFile(selectedFileInfo);
        StartBattle(teams);
        
    }
    public static bool InfoContainTwoPlayers(string[] info)
        =>  info.Contains("Player 2 Team") && info.Contains("Player 1 Team");
    
    
    public static (List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2)
        ObtainDataCharactersFromTeams(string[] infoTeams)
    {
        bool isReadingPlayer1 = false;
        List<string> dataCharactersPlayer1 = new List<string>();
        List<string> dataCharactersPlayer2 = new List<string>();

        foreach (var line in infoTeams)
        {
            if (line.StartsWith("Player 1")) isReadingPlayer1 = true;
            else if (line.StartsWith("Player 2")) isReadingPlayer1 = false;
            else AddCharacterData(line, isReadingPlayer1, dataCharactersPlayer1,
                dataCharactersPlayer2);
        }

        return (dataCharactersPlayer1, dataCharactersPlayer2);
    }
    

    private static void AddCharacterData(string line, bool isReadingPlayer1, List<string> player1Data,
        List<string> player2Data)
    {
        var trimmedLine = line.Trim();
        if (isReadingPlayer1) player1Data.Add(trimmedLine);
        else player2Data.Add(trimmedLine);
    }
    

    public  bool isValidTeam(List<string> dataCharacters)
    {
        bool areSkillsValid = true;
        string patron = @"\((.*?)\)";
        List<string> namesCharacters = [];

        foreach (var dataCharacter in dataCharacters)
        {
            string nameCharacterOnReview = dataCharacter;
            List<string> skills = new List<string>();
            Match containHabilities = Regex.Match(dataCharacter, patron);
            if (containHabilities.Success)
            {
                nameCharacterOnReview =
                    dataCharacter.Substring(0, containHabilities.Index).Trim(); // Guarda la parte antes del paréntesis
                string habilidades =
                    dataCharacter.Remove(0, nameCharacterOnReview.Length + 1)
                        .TrimStart(); // Extrae los valores entre paréntesis y los divide por coma
                habilidades = habilidades.Trim().Trim('(', ')');
                // Divide el string utilizando la coma como separador
                skills = habilidades.Split(',').Select(s => s.Trim()).ToList();
            }

            namesCharacters.Add(nameCharacterOnReview);

            if (!HasMaxTwoSkills(skills) || _utilities.HasDuplicates(skills))
            { 
                areSkillsValid = false;
            }
        }
        bool areNamesRepeated = _utilities.HasDuplicates(namesCharacters);
        bool hasMinOneCharacter = namesCharacters.Count > 0;
        bool hasMaxThreeCharacters = namesCharacters.Count <= 3;
        return !areNamesRepeated && areSkillsValid && hasMinOneCharacter && hasMaxThreeCharacters;


    }
    public  bool ValidateTeams(List<string> dataCharactersPlayer1, List<string> dataCharactersPlayer2)
    {
        return isValidTeam(dataCharactersPlayer1) && isValidTeam(dataCharactersPlayer2);
    }
    
}