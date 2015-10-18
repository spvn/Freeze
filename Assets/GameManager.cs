using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public int[] nextLevel;

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
    private GameObject freezeBar;
    private GameObject actionBar;
    private GameObject eventScreen;


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

    private void attachRequiredUI()
    {
        switch (Application.loadedLevel)
        {
            case 0:
                Debug.Log("Attaching screens for MainMenu");
                mmScreen = assignUI("MainMenuScreen");
                lsScreen = assignUI("LevelSelectorScreen");
                achieveScreen = assignUI("AchievementScreen");
                break;
            case 1:
            case 2:
                Debug.Log("Attaching screens for Level 1");
                startScreen = assignUI("StartingScreen");
                timePanel = assignUI("Panel");
                scorePanel = assignUI("Score Panel");
                goScreen = assignUI("GameOverScreen");
                winScreen = assignUI("WinScreen");
                freezeBar = assignUI("Freeze bar");
                actionBar = assignUI("Action Bar");
                eventScreen = assignUI("EventScreen");
                break;
        }
    }

    private GameObject assignUI(string screenName)
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("ORCanvas");
        }

        GameObject screenOb = canvas.transform.FindChild(screenName).gameObject;
        if (screenOb == null)
        {
            Debug.LogError("Could not find " + screenName);
            return null;
        } else {
            return screenOb;
        }        
    }

    public void updateCurrentLevel()
    {
        currentLevel = Application.loadedLevel;
        attachRequiredUI();
    }

    public void playerGameOver()
    {
        Debug.Log("Player died on level: " + currentLevel);
        goScreen.SetActive(true);
    }

    public void restartCurrentLevel()
    {
        Debug.Log("Player restarting current level: " + currentLevel);
        Application.LoadLevel(currentLevel);
        attachRequiredUI();
    }

    public void currentLevelCleared()
    {
        Debug.Log("Player cleared current level: " + currentLevel);
    }

    public void loadNextLevel()
    {
        Debug.Log("Game attempting to load next level after " +  currentLevel +" : " + nextLevel[currentLevel]);
        Application.LoadLevel(nextLevel[currentLevel]);
    }
}