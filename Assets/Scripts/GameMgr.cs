using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMgr : MonoBehaviour {
    
    public static GameMgr instance; 
    private string nextScene;
    private PlanetPos.Date date; //Date Selected in Options or null then 01.01.1998
    void Awake() 
    { 
        if (instance) { 
            Destroy(gameObject);
            } 
        else 
        { 
            instance = this;
        } 
    }
	// Use this for initialization
	void Start () {
    }

   
    public void setPositions(PlanetPos.Date date)
    {
       
        foreach (int i in System.Enum.GetValues(typeof(Planet)))
        {
            Planet planet = (Planet)Enum.ToObject(typeof(Planet),i);
            PlanetPos pos = StartUp.getInitPosition(planet);
            PlanetPos.Parsed parsed = pos.GetParsed(date);
            //TODO Set Pos from parsed Positions
        };
    }

    public void TogglePauseMenu()
    {
         // not the optimal way but for the sake of readability
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            if (!SceneManager.GetSceneByName("Sonnensystem2").isLoaded)
            {
                SceneManager.LoadScene("Sonnensystem2");
            }
            else
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Sonnensystem2")); 
                Time.timeScale = 1.0f;
            }
            nextScene = "Sonnensystem2";
        }
        else
        {
            if (!SceneManager.GetSceneByName("MenuScene").isLoaded)
            {
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScene")); 
                Time.timeScale = 0f;
            }
            nextScene = "MenuScene";
        }
    }
    
    // Update is called once per frame
    void Update () {
        //Check each frame if MenuScene is loaded to switch to it
        if (SceneManager.GetSceneByName(nextScene).isLoaded && SceneManager.GetActiveScene().name != nextScene)
        {
            if (nextScene.Equals("MenuScene"))
            {
                
            Time.timeScale = 0f;
            }
            else
            {
                
            Time.timeScale = 1.0f;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene)); 
        }
	}

    //Start Planets on a given Day is previous selected in Options
    void restorePlanetPositions()
    {
        //date
    }
    
}
