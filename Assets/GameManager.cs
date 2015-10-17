using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    private GameObject canvas;
    private StatisticManager sm;
    private int currentLevel;
    private GameObject mmScreen;
    private GameObject lsScreen;
    private GameObject achieveScreen;
    private GameObject goScreen;
    private GameObject startScreen;
    private GameObject winScreen;
    private GameObject timePanel;
    private GameObject scorePanel;
        

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            currentLevel = -1;
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        sm = StatisticManager.getManager();
        canvas = GameObject.Find("ORCanvas");
    }

    public static GameManager getManager()
    {
        if (_instance == null)
        {
            Debug.LogError("Game Manager not instantiated yet");
            return null;
        }
        else
        {
            return _instance;
        }
    }

    void Update()
    {

    }

    public void loadLevelSelector()
    {
        Application.LoadLevel(1);
    }

    public void showAchievements()
    {

    }

    public void quitGame()
    {
        Debug.Log("quiting...");
        sm.saveStatistics();
        Application.Quit();
    }

    private void checkUIsLoaded()
    {
        if (currentLevel != Application.loadedLevel)
        {
            switch (Application.loadedLevel)
            {
                case 0:
                    Debug.Log("Checking screens for MainMenu");
                    mmScreen = assignUI("MainMenuScreen");
                    lsScreen = assignUI("LevelSelectorScreen");
                    achieveScreen = assignUI("AchievementScreen");
                    break;
            }
            currentLevel = Application.loadedLevel;
        }
    }

    private GameObject assignUI(string screenName)
    {
        GameObject screenOb = canvas.transform.FindChild(screenName).gameObject;
        if (screenOb == null)
        {
            Debug.LogError("Could not find " + screenName);
            return null;
        } else {
            return screenOb;
        }
        
    }
}
