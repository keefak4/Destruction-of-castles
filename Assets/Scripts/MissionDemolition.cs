using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Gamemode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;
    public Text uitLevel;// link to  UIText_Level
    public Text uitShots;//link to UIText_Shots
    public Text uitButton;//link to UiButton Text
    public Vector3 castlePos;//position castle
    public GameObject[] castles;//masiv castle
    public int _level;//curent level
    public int _levelMax;//numbers level
    public int _shotsTaken;
    public GameObject castle;
    public Gamemode mode = Gamemode.idle;
    public string showing = "Show Slingshot";//mode FollowCam
    private void Start()
    {
        S = this; //define an object alone
        _level = 0;
        _levelMax = castles.Length;
        StartLevel();
    }
    void StartLevel()
    {
        //Destroy castle
        if(castles != null)
        {
            Destroy(castle);
        }
        //Destroy shot
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Progectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        //Create new castle
        castle = Instantiate<GameObject>(castles[_level]);
        castle.transform.position = castlePos;
        _shotsTaken = 0;
        //reinstall camera to normal position
        SwitchView("Show Botch");
        //Restart purpose
        Goal.goalMet = false;
        UpdateGUI();
        mode = Gamemode.playing;
    }
    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (_level + 1) + " of " + _levelMax;
        uitShots.text = "Shots Taken: " + _shotsTaken;
    }
    private void Update()
    {
        UpdateGUI();
        if ((mode == Gamemode.playing) && Goal.goalMet)
        {
            mode = Gamemode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }
    void NextLevel()
    {
        _level++;
        if(_level == _levelMax)
        {
            _level = 0;
        }
        StartLevel();
    }
    public void SwitchView(string eView = "")
    {
        if(eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCameraToShot.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCameraToShot.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;

        }
    }
    public static void ShowFired()
    {
        S._shotsTaken++;
    }


}
