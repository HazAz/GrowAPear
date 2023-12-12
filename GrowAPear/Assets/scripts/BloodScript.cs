using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Invoke("DestroyAfterTime", 0.2f);
	}

	private void DestroyAfterTime()
	{
		Destroy(gameObject);
	}
}
