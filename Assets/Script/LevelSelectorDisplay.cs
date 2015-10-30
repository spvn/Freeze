using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectorDisplay : MonoBehaviour {

    private int highestLevel = 4;
    private int furthestLevel;
    private const int DEFAULT_LEVEL = 1;
    private StatisticManager sm;

    public void loadLevelSelector()
    {
        initialiseLevelSelector();
        transform.GetChild(1).GetComponent<Button>().Select();
    }

    public void initialiseLevelSelector()
    {
        sm = StatisticManager.getManager();
        furthestLevel = (int)sm.getStatisticByName("Furthest Level").getProgress();
        
        if (furthestLevel == 0)
        {
            Application.LoadLevel(DEFAULT_LEVEL);
        } else {
            for (int i = furthestLevel+2; i <= highestLevel; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(transform.childCount-1).GetComponent<Button>().onClick.AddListener(mainMenuButtonPressed);
            gameObject.SetActive(true);
        }
    }

    public void levelButtonPressed(int levelSelected)
    {
        Application.LoadLevel(levelSelected);
    }

    public void mainMenuButtonPressed()
    {
        GameManager gm = GameManager.getManager();
        gm.backToMainMenu();
    }
}
