using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class BadGuy1 : NetworkBehaviour {

	private NavMeshAgent nmAgent;
	private Vector3[] movePoints;

	[SerializeField] private int destPoint = 0;

	[SerializeField] private Transform destTransform;

	private IEnumerator SlowUpdate() {
		while (true) {

			if (!nmAgent.pathPending && nmAgent.remainingDistance < 10.0f) {

				nmAgent.destination = movePoints[destPoint];

				destPoint = (destPoint + 1) % movePoints.Length;
			}

			yield return new WaitForSeconds(0.2f);
		}
	}

	// Use this for initialization
	void Start () {
		nmAgent = GetComponent<NavMeshAgent>();

		movePoints = new Vector3[2];
		movePoints[0] = transform.position;
		movePoints[1] = destTransform.position;

		if (isServer) {
			StartCoroutine(SlowUpdate());
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
