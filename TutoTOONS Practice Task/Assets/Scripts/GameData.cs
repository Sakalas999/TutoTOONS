using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public TextAsset jsonFile;
    public LevelList myLevelList = new LevelList();
    public List<Coordinates> levelsCoordinatesList = new List<Coordinates>();
    public int levelToLoadIndex = 0;

    public static GameData Instance;


    [System.Serializable]
    public class Level
    {
        public string[] level_data;
    }

    [System.Serializable]
    public class LevelList
    {
        public Level[] levels;
    }

    public class Coordinates
    {
        public Vector2[] coordinates;
    }

    private void Start()
    {
        myLevelList = JsonUtility.FromJson<LevelList>(jsonFile.text);
        CorectCoordinates();
    }

    void Awake()
    {
        GameData [] objs = FindObjectsOfType<GameData>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void CorectCoordinates()
    {
        Level[] levels = myLevelList.levels;

        for (int i = 0; i < levels.Length; i++)
        {
            string[] level_data = levels[i].level_data;
            Vector2[] coordinates = new Vector2[level_data.Length/2];
            int indexer = 0;

            for(int j = 0; j < level_data.Length; j+=2)
            {
                coordinates[indexer] = new Vector2(int.Parse(level_data[j]), int.Parse(level_data[j+1]));
                indexer++;
            }

            Coordinates coardinateList = new Coordinates();
            coardinateList.coordinates = ReversYCoordinatePosition(coordinates);
            levelsCoordinatesList.Add(coardinateList);
        }
    }

    private Vector2[] ReversYCoordinatePosition(Vector2[] coordinates)
    {
        for (int i = 0; i < coordinates.Length; i++)
        {
            float y = coordinates[i].y;
            float difference = y - 1000;
            coordinates[i].y = difference * (-1);
        }

        return coordinates;
    }
}
