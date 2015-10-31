using System.Linq;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Achievement {
	public string name;
	public string description;
	public float progress = 0.0f;
	public float target;
	public bool unlocked;

	public void addProgress (float progress)
	{
		this.progress += progress;
		Debug.Log ("Achievement \"" + this.name + "\" has been updated! (" + progress + "/" + target + ")");
		this.updateUnlockStatus ();
	}

	public void setProgress (float progress) 
	{
		this.progress = progress;
		Debug.Log ("Achievement \"" + this.name + "\" has been updated! (" + progress + "/" + target + ")");
		this.updateUnlockStatus ();
	}

	private void updateUnlockStatus()
	{
		if (this.progress >= this.target && !this.unlocked) {
			this.unlocked = true;
			this.notifyAchievementUnlocked();
		} else if (this.progress < this.target && this.unlocked) {
			this.unlocked = false;
		}
	}
	
	private void notifyAchievementUnlocked ()
	{
        StatisticManager sm = StatisticManager.getManager();
        sm.newAchievementUnlocked(this.name);
    }
}

// Achievement Manager no longer in use
[System.Serializable]
public class AchievementManager : MonoBehaviour {
	private	static AchievementManager _instance;
	public Achievement[] achievements;

	void Awake () {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this);
		} else {
			if (this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}

	public static AchievementManager getManager()
	{
		return _instance;
	}

	public Achievement getAchievementByName(string achievementName)
	{
		return achievements.FirstOrDefault(achievement => achievement.name == achievementName);
	}

	public void addProgressByAchievementName(string achievementName, float progressAmount)
	{
		Achievement achievement = this.getAchievementByName (achievementName);
		achievement.addProgress (progressAmount);
	}

	public void setProgressByAchievementName(string achievementName, float progressAmount)
	{
		Achievement achievement = this.getAchievementByName (achievementName);
		achievement.addProgress (progressAmount);
	}
}

