using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DataLevel
{
    [SerializeField] private string _levelName;
    public string LevelName => _levelName;
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
    [SerializeField] private Sprite _imagePreviewMini;
    public Sprite ImagePreviewMini => _imagePreviewMini;
    [SerializeField] private DataToSaves _dataToSaves;
    public DataToSaves DataToSaves{ get => _dataToSaves; set => _dataToSaves = value; }
}
