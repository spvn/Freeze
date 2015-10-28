using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour {
    public Transform achievementPage;

    private const int NEXT = 6;
    private const int PREV = 7;
    private const int MAINMENU = 8;

    private int currentViewingPage = 0;
    private int currentSelection = -1;
    private int achievementPerPage = 5;
    private StatisticManager sm;
    private List<Achievement> achievements;
    private List<Transform> achievementPages;

    // Use this for initialization
    void Start()
    {/*
        sm = StatisticManager.getManager();
        achievements = sm.getAllAchievements();
        reconstructAchievementPages();
        Debug.Log("achiemvent display loaded");
        */
    }
	// Update is called once per frame
	void Update () {

    }

    public void loadFirstPage()
    {
        sm = StatisticManager.getManager();
        achievements = sm.getAllAchievements();
        reconstructAchievementPages();
        achievementPages[0].gameObject.SetActive(true);
        currentSelection = MAINMENU;
        currentViewingPage = 0;
        achievementPages[0].GetChild(8).GetComponent<Button>().Select() ;
    }

    public void reconstructAchievementPages()
    {
        destroyAllPages();
        createAchievementPages();
        populateAchievementPages();
    }

    private Transform createPage()
    {
        Transform clone = (Transform)Instantiate(achievementPage, transform.position, transform.rotation);
        clone.SetParent(gameObject.transform);
        clone.localScale = new Vector3(1, 1, 1);
        RectTransform cloneRect = (RectTransform)clone;
        cloneRect.offsetMax = new Vector2(0, 0);
        cloneRect.offsetMin = new Vector2(0, 0);
        clone.gameObject.SetActive(false);
        return clone;
    }

    private void destroyAllPages()
    {
        foreach (Transform childTransform in transform) Destroy(childTransform.gameObject);
        achievementPages = new List<Transform>();
    } 

    private void createAchievementPages()
    {
        Debug.Log(achievements.Count);
        for (int i = 0; i < achievements.Count/5; i++)
        {
            achievementPages.Add(createPage());
        }
    }

    private void populateAchievementPages()
    {
        for (int i = 0; i < achievements.Count/5; i++)
        {
            Transform currentPage = achievementPages[i];
            for (int j = 0; j < achievements.Count - i*5 && j < 5; j++)
            {
                // Add 1 to get panel as 1st child is background
                Transform currentPanel = currentPage.GetChild(j+1);
                Achievement currentAchievement = achievements[j + i * 5];

                // Setting panel color and description
                if (currentAchievement.unlocked)
                {
                    currentPanel.GetComponent<Image>().color = new Color(0, 1, 0, 1);
                    currentPanel.GetChild(1).GetComponent<Text>().text = currentAchievement.target + "/" + currentAchievement.target;
                } else
                {
                    currentPanel.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                    currentPanel.GetChild(1).GetComponent<Text>().text = currentAchievement.progress + "/" + currentAchievement.target;
                }
                currentPanel.GetChild(0).GetComponent<Text>().text = currentAchievement.name;
            }

            // Setting page buttons
            if (i == 0)
            { 
                currentPage.GetChild(7).gameObject.SetActive(false);
            }

            if (i == (achievements.Count/5) - 1)
            {
                currentPage.GetChild(6).gameObject.SetActive(false);
            }

            currentPage.GetChild(NEXT).GetComponent<Button>().onClick.AddListener(nextButtonPressed);
            currentPage.GetChild(PREV).GetComponent<Button>().onClick.AddListener(prevButtonPressed);
            currentPage.GetChild(MAINMENU).GetComponent<Button>().onClick.AddListener(mainMenuButtonPressed);
        }
    }

    public void nextButtonPressed()
    {
        achievementPages[currentViewingPage].gameObject.SetActive(false);
        currentViewingPage += 1;
        achievementPages[currentViewingPage].gameObject.SetActive(true);
        currentSelection = NEXT;
        highlightCurrentSelection();
    }

    public void prevButtonPressed()
    {
        achievementPages[currentViewingPage].gameObject.SetActive(false);
        currentViewingPage -= 1;
        achievementPages[currentViewingPage].gameObject.SetActive(true);
        currentSelection = PREV;
        highlightCurrentSelection();
    }

    public void mainMenuButtonPressed()
    {
        achievementPages[currentViewingPage].gameObject.SetActive(false);
        currentViewingPage = -1;
        GameManager gm = GameManager.getManager();
        gm.backToMainMenu();
    }

    private void highlightCurrentSelection()
    {
        if (!achievementPages[currentViewingPage].GetChild(currentSelection).GetComponent<Button>().IsActive())
        {
            currentSelection = MAINMENU;
        }
        achievementPages[currentViewingPage].GetChild(currentSelection).GetComponent<Button>().Select();
    }
}
