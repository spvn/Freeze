using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBar: MonoBehaviour {
	public float maxActionStock;
//	public float freezeDepletionRate;
	public float actionReplenishRate;
	public RectTransform actionBarUI;
//	public RectTransform coolDownBarUI;
	
	private float currActionAmt;
	private float actionBarUIMinX;
	private float actionBarUIWidth;
	private LevelManager levelManager;
	private float countdownTime = 0.0f;
	
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		currActionAmt = maxActionStock;
		actionBarUIWidth = actionBarUI.rect.size.x;
		actionBarUIMinX = actionBarUI.localPosition.x - actionBarUIWidth;
	}
	
	void Update () {
		//print (currActionAmt);
		
		if (levelManager.getIsAttackingStatus()) {
			// Check if player is attacking. If no, countdown 2 secs before replenishing action bar.
			//Debug.Log ("In Action bar");
			DepleteActionStock(300);
			levelManager.setActionBar();
		}
		else {
		//	countdownTime -= Time.deltaTime;
			//Debug.Log ("In Action bar - replenishing");
	//		if (countdownTime <= 0.0) {
				ReplenishActionStock();
	//		}
		//	countdownTime = 1.0f;
		}
	}

	public void DepleteActionStock(int actionPoints) {
		// actionPoints must range from zero to 1000.

		currActionAmt -= actionPoints;
		
		if (currActionAmt < 0) {
			currActionAmt = 0;
		}
		
		UpdateActionBarUI();
	}
	
	public void ReplenishActionStock() {
		currActionAmt += actionReplenishRate;
		
		if (currActionAmt > maxActionStock) {
			currActionAmt = maxActionStock;
		}
		
		UpdateActionBarUI();
	}
	
	void UpdateActionBarUI() {
		float actionStockPercentage = currActionAmt / maxActionStock * 1f;
		
		Vector3 position = actionBarUI.localPosition;
		position.x = actionBarUIMinX + actionStockPercentage * actionBarUIWidth;
		actionBarUI.localPosition = position;
	}

    public bool canDeplete(int actionPoint)
    {
        if (currActionAmt >= actionPoint)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
