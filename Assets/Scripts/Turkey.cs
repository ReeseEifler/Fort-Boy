using UnityEngine;
using System.Collections;

public class Turkey : MonoBehaviour {

	private int hits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Projectile"))
		{
			hits++;

			if (hits == 3) 
			{
				this.gameObject.SetActive (false);
			}
		}
	}
}
