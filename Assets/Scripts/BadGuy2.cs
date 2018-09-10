using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BadGuy2 : NetworkBehaviour {

	private Rigidbody rigidbody;

	[SerializeField] private float speed = 1.0f;

	// Use this for initialization
	void Start () {
		// Get rigidbody component.
		rigidbody = GetComponent<Rigidbody>();

		if (isServer) {
			rigidbody.velocity = new Vector3(0, 0, 1) * speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
    {
        if (isServer) {
			if (collision.collider.gameObject.tag == "Wall") {
				Vector3 force = transform.position - collision.transform.position;
				force.Normalize();

				RaycastHit hit;

				while (Physics.Raycast(transform.position, force, out hit, 25.0f)) {
					Debug.Log("We hit something...");

					// Check if we didn't hit a wall.
					if (hit.transform.gameObject.tag != "Wall") {
						Debug.Log("we can go this way.");

						break;
					}

					// Rotate our direction.
					force = Quaternion.Euler(0, -15, 0) * force;
				}

				rigidbody.velocity = force * speed;
			}
		}
    }
}
