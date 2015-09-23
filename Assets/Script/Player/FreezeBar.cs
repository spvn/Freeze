using UnityEngine;
using System.Collections;

public class FreezeBar : MonoBehaviour {
	public float maxFreezeStock;
	public float freezeDepletionRate;
	public float freezeReplenishRate;
	public RectTransform freezeBarUI;

	private bool isFrozen;

	private float currFreezeAmt;
	private float freezeBarUIMinX;
	private float freezeBarUIWidth;

	void Start () {
		currFreezeAmt = maxFreezeStock;

		freezeBarUIWidth = freezeBarUI.rect.size.x;
		freezeBarUIMinX = freezeBarUI.localPosition.x - freezeBarUIWidth;
	}
	
	void Update () {
		//print (currFreezeAmt);
		if (isFrozen) {
			DepleteFreezeStock();
		}
		else {
			ReplenishFreezeStock();
		}
	}

	void DepleteFreezeStock() {
		currFreezeAmt -= freezeDepletionRate;

		if (currFreezeAmt < 0) {
			currFreezeAmt = 0;
		}

		UpdateFreezeBarUI();
	}

	public void ReplenishFreezeStock() {
		currFreezeAmt += freezeReplenishRate;
		
		if (currFreezeAmt > maxFreezeStock) {
			currFreezeAmt = maxFreezeStock;
		}
		
		UpdateFreezeBarUI();
	}

	void UpdateFreezeBarUI() {
		float freezeStockPercentage = currFreezeAmt / maxFreezeStock * 1f;

		Vector3 position = freezeBarUI.localPosition;
		position.x = freezeBarUIMinX + freezeStockPercentage * freezeBarUIWidth;
		freezeBarUI.localPosition = position;
	}
}
