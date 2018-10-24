using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2 {
  public class PlaceBall : MonoBehaviour {

    [SerializeField]
    Vector3 startingPos;
    [SerializeField]
    Transform ball;

    PlayerBall playerBall;

    void OnEnable() {

    }

    void OnDisable() {

    }
    // Use this for initialization
    void Start() {
      

      Instantiate(ball, startingPos, Quaternion.identity);

      playerBall = FindObjectOfType<PlayerBall>();

      playerBall.ConstrainRB();
    }

    public void UnconstrainBall()
    {
      //playerBall.UnconstrainRB();
    }

    // Update is called once per frame
    void Update() {

    }

    void InitializeReferences() {

    }
  }
}