using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	private Rigidbody rigidbody;

	[SerializeField] private float minimumInput = 0.1f;
	[SerializeField] private float moveSpeed = 25.0f;

	[SyncVar] private float health;

	private IEnumerator UpdateInput() {
		while (true) {
			Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			if (Mathf.Abs(move.x) > minimumInput || Mathf.Abs(move.y) > minimumInput) {
				CmdMove(move);
			}

			yield return new WaitForSeconds(0.05f);
		}
	}

	[Command]
	private void CmdMove(Vector2 request) {
		rigidbody.velocity = new Vector3(request.x, 0, request.y) * moveSpeed;
	}

	private void OnCollisionExit(Collision other) {
		if (isServer) {
			if (other.gameObject.tag == "BadGuy") {
				--health;
			}
		}
	}

	// Use this for initialization
	void Start () {
		// Get rigidbody component.
		rigidbody = GetComponent<Rigidbody>();

		if (isServer) {
			// Set Initial health.
			health = 100;
		}

		if (isLocalPlayer) {
			// Start input coroutine.
			StartCoroutine(UpdateInput());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
