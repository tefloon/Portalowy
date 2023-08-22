using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BallScript : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.transform.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
