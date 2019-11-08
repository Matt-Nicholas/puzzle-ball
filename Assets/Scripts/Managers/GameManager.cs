using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:SingletonPersistant<GameManager>
{
    private int _lastLevelPlayed = 1;
    private int _highestLevelUnlocked = 1;
    private string name = "RollyBall";
    private bool preLoading = false;
    PlayerData _playerData;

    public GameManager() { }

    protected virtual void Awake()
    {
        base.Awake();

        if (FileExists(name))
        {
            _playerData = LoadGame(name);
        }
        else
        {
            _playerData = new PlayerData(name, 1, 0);
            SaveGame(name, _playerData);
        }

        LoadScene("Menu");
    }

    public void CompletedLevel(int levelCompleted)
    {
        _lastLevelPlayed = levelCompleted;

        if(_highestLevelUnlocked < levelCompleted)
        {
            _highestLevelUnlocked = levelCompleted;
        }

        //if(SceneManager.GetActiveScene().buildIndex < (UnityEditor.EditorBuildSettings.scenes.Length - 1))
        if(SceneManager.GetActiveScene().buildIndex < 4)
                Invoke("NextLevel", 2.0f);
        else
        {
            Debug.Log("YOU BEAT THE GAME!");
        }
        //ParticleSystem.Instantiate.particleSystem(fireworks);

    }

    private void LoadNextLevel()
    {
        
        if(_lastLevelPlayed >= 4) return;

        LoadScene(_lastLevelPlayed + 1);
    }

    public void LoadMaxLevel()
    {
        LoadScene("Level-" + _highestLevelUnlocked);
    }

    public void SaveGame(string playerName, PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        bool attemptingToSave = true;
        int attempts = 0;

        while(attemptingToSave)
        {
            try
            {
                attempts++;

                if(!File.Exists(Application.persistentDataPath + "/" + playerName + ".dat"))
                {
                    file = File.Create(Application.persistentDataPath + "/" + playerName + ".dat");
                }
                else
                {
                    file = File.Open(Application.persistentDataPath + "/" + playerName + ".dat", FileMode.Open);
                }

                bf.Serialize(file, data);
                file.Close();

                attemptingToSave = false;
            }
            catch(IOException)
            {
                if(attempts >= 10)
                {
                    return;
                }
                Thread.Sleep(1);
            }
        }
    }

    public PlayerData LoadGame(string fileName)
    {
        FileStream file = null;
        BinaryFormatter bf = new BinaryFormatter();

        bool attemptingToLoad = true;
        int attempts = 0;

        while(attemptingToLoad)
        {
            try
            {
                attempts++;

                file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);

                return (PlayerData)bf.Deserialize(file);

            }
            catch(IOException)
            {
                if(attempts >= 10)
                {
                    return null;
                }
                Thread.Sleep(200);
            }
        }
        return null;
    }

    public bool FileExists(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
            return true;

        return false;
    }

    public void LoadScene(string name, bool additive = true)
    {
        CloseAllAdditiveScenes();

        if(additive)
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(name);

    }
    public void LoadScene(int index, bool additive = true)
    {
        CloseAllAdditiveScenes();

        if(additive)
            SceneManager.LoadScene(index, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(index);
    }
    public void LoadScene(Scene scene, bool additive = true)
    {
        CloseAllAdditiveScenes();

        if(additive)
            SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(scene.name);
    }

    private void CloseAllAdditiveScenes()
    {
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene cScene = SceneManager.GetSceneByBuildIndex(i);
            if (cScene == null || cScene.name == null) continue;

            if(cScene.name.Equals("Main")) continue;

            SceneManager.UnloadSceneAsync(cScene);
        }
    }

    public void RestartScene()
    {
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(!SceneManager.GetSceneAt(i).name.Equals("Main"))
            {
                LoadScene(SceneManager.GetSceneAt(i));
                break;
            }
        }
    }

    public void Preload(Scene scene)
    {
        LoadScene("Main", false);
        LoadScene(scene);
    }

    private void OnGUI()
    {
        //int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        //GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 80, 100, 30), "Current Scene:" + (SceneManager.GetActiveScene().buildIndex));

        if(GUI.Button(new Rect(Screen.width / 2 - 50, 2, 50, 20), "Replay"))
        {
            RestartScene();
        }

    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch(IOException)
        {
            //the file is unavailable
            return true;
        }
        finally
        {
            if(stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }
}


