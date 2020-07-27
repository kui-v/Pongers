using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    public GUISkin layout;

    GameObject theBall;
    GameObject[] pauseObjects;

    void Start() {
        // Set theBall to reference Ball object class
        theBall = GameObject.FindGameObjectWithTag("Ball");
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Updates the appropriate player score
    public static void Score(string wallID) {
        if (wallID == "RightWall") {
            PlayerScore1++;
        } else {
            PlayerScore2++;
        }
    }

    // RESERVED UNITY FUNCTION
    // OnGUI called for rendering and handling GUI events
    // Displays score and implements reset button functionality
    // Checks every time something happens if someone has won yet
    void OnGUI() {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

        // RESTART button
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART")) {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            
            // SendMessage calls method "RestartGame" on every MonoBehaviour in this game object
            // Triggers any function that matches the name that we send it in a class we specify (BallControl class)
            // SendMessage(string methodName, object value=null, SendMessageOptions options=SendMessageOptions.RequireReceiver)
            theBall.SendMessage("RestartGame", 0.5f);
        }

        if (PlayerScore1 == 10) {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
            theBall.SendMessage("ResetBall");
        } else if (PlayerScore2 == 10) {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
            theBall.SendMessage("ResetBall");
        }
    }

    void Update() {

        // uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P)) {
            if (Time.timeScale == 1) {
                Time.timeScale = 0; // set the timescale to zero (stopped)
                showPaused();
            } else if (Time.timeScale == 0) {
                Time.timeScale = 1; // set the timescale back to one (normal play)
                hidePaused();
            }
        }
    }

    // shows objects with ShowOnPause tag
    public void showPaused() {
        foreach(GameObject g in pauseObjects) { 
            g.SetActive(true);
        }
    }

    // hides objects with ShowOnPause tag
    public void hidePaused() {
        foreach(GameObject g in pauseObjects) {
            g.SetActive(false);
        }
    }

}