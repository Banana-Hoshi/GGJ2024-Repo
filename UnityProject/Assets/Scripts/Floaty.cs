using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour
{
	Rigidbody rb;
	WaterHeightCheck[] bumps;
	private void Awake() {
		rb = GetComponent<Rigidbody>();
		bumps = GetComponentsInChildren<WaterHeightCheck>();
	}
	
	public void Bump(float force, float turbulence) {
		foreach (WaterHeightCheck bump in bumps) {
			bump.Bump(rb, force * Random.Range(1f - turbulence, 1f + turbulence) / bumps.Length);
		}
	}
}
