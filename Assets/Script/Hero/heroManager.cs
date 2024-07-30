using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class heroManager : MonoBehaviour 
{
    public static heroManager Instance { get; private set; }

    public List<Hero> detectedHeroes = new List<Hero>();
    public List<Hero> saveHeros = new List<Hero>();

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "detectedHeroes.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveDetectedHeroes()
    {
        string jsonSave = JsonUtility.ToJson(new Serialization<Hero>(detectedHeroes));
        File.WriteAllText(saveFilePath, jsonSave);
        Debug.Log("Detected heroes saved.");
    }

    public void LoadDetectedHeroes()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            var loadedHeroes = JsonUtility.FromJson<Serialization<Hero>>(json).ToList();

            saveHeros = loadedHeroes.FindAll(hero => hero != null);
            Debug.Log("Detected heroes loaded.");
        }
        else
        {
            Debug.LogWarning("No save file found for detected heroes.");
        }
    }
}

// Helper class for serializing lists to JSON
[System.Serializable]
public class Serialization<T>
{
    public List<T> target;

    public Serialization(List<T> target)
    {
        this.target = target;
    }

    public List<T> ToList()
    {
        return target;
    }
}
