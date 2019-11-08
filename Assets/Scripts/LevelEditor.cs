using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class LevelEditor : MonoBehaviour {

    private string filepath;
    private bool overwrite = false;
    private string levelNumString = "#";
    private void Start()
    {
        filepath = Application.persistentDataPath + "/levels.json";
    }

    public void Save(LevelInfo levelInfo, int levelNum, bool overwriteIfExists)
    {
        int attempts = 0;
        bool attemptingToSave = true;
        LevelInfoCollection levelCollection = null;

        while(attemptingToSave)
        {
            try
            {
                attempts++;
                if(File.Exists(filepath))
                {
                    levelCollection = Load();

                    if(levelCollection == null) levelCollection = new LevelInfoCollection();

                    if(levelCollection.levels.ContainsKey(levelInfo.levelNum))
                    {
                        if(overwriteIfExists)
                        {
                            levelCollection.levels.Remove(levelInfo.levelNum);
                            levelCollection.levels.Add(levelInfo.levelNum, levelInfo);
                        }
                        else
                        {
                            Debug.LogWarning("Level Number already exists. If you would like to overwite it change setting on LevelEditor. Otherwise change leveln number.");
                            return;
                        }
                    }
                    else
                    {
                        levelCollection.levels.Add(levelInfo.levelNum, levelInfo);
                    }
                }
                else
                {
                    File.Create(filepath);
                    levelCollection = new LevelInfoCollection();
                    levelCollection.levels.Add(levelInfo.levelNum, levelInfo);
                }

                //string json = JsonUtility.ToJson(levelCollection);

                string json = JsonConvert.SerializeObject(levelCollection);

                File.WriteAllText(filepath, json, Encoding.UTF8);

                attemptingToSave = false;
            }
            catch(IOException e)
            {
                if(attempts >= 100)
                {
                    return;
                }
                Thread.Sleep(10);
            }
            catch(Exception e)
            {
                if(attempts >= 10)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }
    }

    public LevelInfoCollection Load()
    {
        //FileStream file = null;
        //BinaryFormatter bf = new BinaryFormatter();

        bool attemptingToLoad = true;
        int attempts = 0;

        while(attemptingToLoad)
        {
            try
            {
                attempts++;

                string json = File.ReadAllText(filepath, Encoding.UTF8);

                return JsonConvert.DeserializeObject<LevelInfoCollection>(json);

            }
            catch(IOException e)
            {
                if(attempts >= 10)
                {
                    return null;
                }
                Thread.Sleep(10);
            }
            catch(Exception e)
            {
                if(attempts >= 10)
                {
                    return null;
                }
                Thread.Sleep(10);
            }

        }
        return null;
    }

    void OnGUI()
    {
        int levelNum = -1;
        levelNumString = GUI.TextField(new Rect(30, 60, 150, 30), levelNumString);

        if(GUI.Toggle(new Rect(30, 90, 150, 30), false, "Overwrite Level: " + overwrite.ToString()))
        {
            overwrite = !overwrite;
        }

        if(GUI.Button(new Rect(30, 30, 150, 30), "Save Level"))
        {

            if(!int.TryParse(levelNumString, out levelNum))
            {
                Debug.LogWarning("Level Not Saved! Please enter a valid number.");
            }
            else
            {
                if(levelNum > 0)
                {
                    LevelInfo levelInfo = new LevelInfo(levelNum, gameObject);
                    Save(levelInfo, levelNum, overwrite);
                }
                else
                {
                    Debug.LogWarning("Level Not Saved! Level Number must be greater than zero.");
                }
            }
        }

        if(GUI.Button(new Rect(30, 130, 150, 30), "Load Level"))
        {
            LevelInfoCollection levelCollection = Load();

            if(!int.TryParse(levelNumString, out levelNum))
            {
                Debug.LogWarning("Level Not Loaded! Please enter a valid number.");
            }
            else
            {
                if(!levelCollection.levels.ContainsKey(levelNum))
                {
                    Debug.LogWarning("LevelEditor::OnGUI - Failed to load: " + levelNum);
                }

                LevelInfo level = levelCollection.levels[levelNum];

                if(level != null)
                {
                    foreach(var item in level.items)
                    {
                        GameObject prefab = (GameObject)Resources.Load("prefabs/" + item.prefabName, typeof(GameObject));

                        Transform instance = Instantiate(prefab, item.position, item.rotation).GetComponent<Transform>();
                        instance.localScale = item.scale;
                        instance.parent = gameObject.transform;
                    }
                }
            }
        }

        if(GUI.Button(new Rect(30, 170, 150, 30), "Clear"))
        {
            foreach(Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}



