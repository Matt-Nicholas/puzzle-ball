using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GoalManager:MonoBehaviour
{
    public List<ParticleSystem> fireworks = new List<ParticleSystem>();

    [SerializeField]
    private int levelNumber;
    private int _index = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("PlayerBall")) return;

        PlayerBall pb = other.GetComponent<PlayerBall>();
        pb.HitGoal(transform.position);

        int level = 0;

        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            string name = SceneManager.GetSceneAt(i).name;
            string[] nameAndLevel = name.Split('-');

            if(nameAndLevel.Length != 2 || !nameAndLevel[0].Equals("Level")) continue;

            if(!int.TryParse(nameAndLevel[1], out level))
            {
                continue;
            }
            else
            {

                if(level <= 0) continue;

                GameManager.Instance.CompletedLevel(level);

                PlayFireworks();
            }
        }
    }

    void PlayFireworks()
    {
        fireworks[_index].Play();
        _index++;
        if(_index >= fireworks.Count) return;
        Invoke("PlayFireworks", 0.4f);

    }
}
