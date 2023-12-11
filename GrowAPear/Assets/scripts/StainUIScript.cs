using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainUIScript : MonoBehaviour
{
	private float startSize = 0.1f;
	float currentTimer = 0f;
	float timer = 0.2f;

	private void Start()
	{
		transform.localScale = new Vector3(startSize, startSize, startSize);
	}

	// Update is called once per frame
	void Update()
	{
		if (currentTimer > timer) return;
		currentTimer += Time.deltaTime;

		var newSize = Mathf.Lerp(startSize, 1f, currentTimer / timer);
		transform.localScale = new Vector3(newSize, newSize, newSize);
	}
}
