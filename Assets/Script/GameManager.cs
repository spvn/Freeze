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
    private GameObject HUD;
    private GameObject pauseScreen;
    private GameObject exitScreen;


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
        attachRequiredUI();
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
        if (canvas == null)
        {
            attachRequiredUI();
        }
    }

    public void loadLevelSelector()
    {
        Application.LoadLevel(1);
    }

    public void showAchievements()
    {
        mmScreen.SetActive(false);
        achieveScreen.SetActive(true);
        achieveScreen.GetComponent<AchievementDisplay>().loadFirstPage();
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
            case 3:
            case 4:
                Debug.Log("Attaching screens for playable level");
                startScreen = assignUI("StartingScreen");
                HUD = assignUI("HUD");
                goScreen = assignUI("GameOverScreen");
                winScreen = assignUI("WinScreen");
                pauseScreen = assignUI("PauseScreen");
                exitScreen = assignUI("ExitScreen");
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
        sm.addProgressByStatisticName("Deaths", 1);
    }

    public void restartCurrentLevel()
    {
        Debug.Log("Player restarting current level: " + currentLevel);
        Application.LoadLevel(currentLevel);
      //  attachRequiredUI();
    }

    public void currentLevelCleared()
    {
        Debug.Log("Player cleared current level: " + currentLevel);
        sm.setProgressIfHigher("Furthest Level", currentLevel);
    }

    public void loadNextLevel()
    {
        Debug.Log("Game attempting to load next level after " +  currentLevel +" : " + nextLevel[currentLevel]);
        Application.LoadLevel(nextLevel[currentLevel]);
    }

    public void pauseGame()
    {
        HUD.SetActive(false);
        pauseScreen.SetActive(true);
        exitScreen.SetActive(false);
    }

    public void unpauseGame()
    {
        HUD.SetActive(true);
        pauseScreen.SetActive(false);
        exitScreen.SetActive(false);
    }
    
    public void exitToMainMenu()
    {
        Debug.Log("Player exit to main menu");
        Application.LoadLevel(0);
    }

    public void backToMainMenu()
    {
        achieveScreen.SetActive(false);
        mmScreen.SetActive(true);
    }
}