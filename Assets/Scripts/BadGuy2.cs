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

				rigidbody.velocity = force * speed;
			}
		}
    }
}
