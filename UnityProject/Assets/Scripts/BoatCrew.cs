using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCrew : MonoBehaviour
{
	public CrewManager crew;
	private void Start() {
		crew = GetComponent<CrewManager>();
	}
}
