using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public enum CollectableTypes {Trophy, Money};

	public CollectableTypes CollectableType; // this gameObject's type
	public int value = 100;
	public float rotationSpeed = 70f, bouncingSpeed = 0.65f, bouncingBounds = 0.35f;
	float bouncingBounds1 = 0f, bouncingBounds2 = 0f;
	Vector3 startingPos;
	bool moveUp = true, moveDown = false;
	public TextMeshProUGUI trophyText;

	//public AudioClip collectSound;

	void Start ()
	{
		startingPos = this.transform.position;
		bouncingBounds1 = startingPos.y + bouncingBounds;
		bouncingBounds2 = startingPos.y - bouncingBounds;
		//Debug.Log("Bounds: " + bouncingBounds1 + ", " + bouncingBounds2);
		startingPos.y = Random.Range(bouncingBounds1, bouncingBounds2);
		this.transform.position = startingPos;

		if (trophyText == null) return;
	}
	
	void Update()
	{
		
	}

	void FixedUpdate ()
	{
		if (moveUp)
		{
			transform.Translate(Vector3.up * bouncingSpeed * Time.deltaTime, Space.World);

			if (this.transform.position.y >= bouncingBounds1)
			{
				moveDown = true;
				moveUp = false;
			}
		}
		else if (moveDown)
		{
			transform.Translate(Vector3.down * bouncingSpeed * Time.deltaTime, Space.World);

			if (this.transform.position.y <= bouncingBounds2)
			{
				moveUp = true;
				moveDown = false;
			}
		}

		transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Collect();
		}
	}

	public void Collect()
	{
		//if(collectSound) AudioSource.PlayClipAtPoint(collectSound, transform.position);

		switch (CollectableType)
		{
			case (CollectableTypes.Trophy):
				EventManager.Instance.EnableTextWithTimer(this.trophyText, true);
				break;
			case (CollectableTypes.Money):
				EventManager.Instance.AddMoney(value);
				break;
		}
		Destroy(this.gameObject);
	}
}
