using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevelContainer", menuName = "ScriptableObjects/DataLevelContainer", order = 1)]
public class DataLevelContainer : ScriptableObject
{
    [SerializeField] private DataLevel[] _levels;
    [SerializeField] private int _sceneToLoad;
    public int SceneToLoad { get => _sceneToLoad; set => _sceneToLoad = value; }
    public DataLevel[] Levels => _levels;

    public DataLevel GetLevel(int id)
    {
        return _levels[id];
    }
    public DataLevel GetCurrentLevel()
    {
        return _levels[_sceneToLoad];
    }

    public void NextLevel()
    {
        _sceneToLoad++;
    }

    public void ResetData()
    {
        foreach (DataLevel level in Levels)
        {
            level.DataToSaves = new DataToSaves();
        }
        Save();
    }
    public void Complete()
    {
        foreach (DataLevel level in Levels)
        {
            level.DataToSaves = new DataToSaves();
            level.DataToSaves.IsCompleted = true;
        }
        Save();
    }

    private void Save()
    {
        string _filePath = Application.persistentDataPath + "/GameData.save";
        List<DataToSaves> _gameData = new List<DataToSaves>();

        foreach (DataLevel level in Levels)
        {
            _gameData.Add(level.DataToSaves);
        }

        BinaryFormatter formatter = new();
        FileStream stream = new(_filePath, FileMode.Create);
        formatter.Serialize(stream, _gameData);
        stream.Close();
    }
}
