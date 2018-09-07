using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIHud : NetworkBehaviour {

	private Player localPlayer;
	private int currentHP = 0;

	[SerializeField] private Text textHP;

	private IEnumerator UpdateHud() {
		Debug.Log("Hello");
		while (true) {
			// Update current hp.
			currentHP = localPlayer.GetCurrentHP();

			// Update UI with new HP.
			textHP.text = currentHP.ToString();

			// Wait until HP has changed.
			yield return new WaitUntil(() => currentHP != localPlayer.GetCurrentHP());
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Check if we have captured our local player.
		if (localPlayer == null && ClientScene.localPlayers.Count > 0) {
			// Get reference to player component in local player.
			localPlayer = ClientScene.localPlayers[0].gameObject.GetComponent<Player>();

			StartCoroutine(UpdateHud());
		}
	}
}
