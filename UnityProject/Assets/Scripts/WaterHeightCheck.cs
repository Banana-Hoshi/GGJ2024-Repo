using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterHeightCheck : MonoBehaviour
{
	AsyncGPUReadbackRequest readback;
	int x, y;

	private void Update() {
		if (readback.done) {
			if (readback.hasError) {
				Debug.Log("whoops");
			}

			StartReadback();
		}
	}

	void StartReadback() {
		x = 0;
		y = 0;
		readback = AsyncGPUReadback.Request(null, 0, x, 1, y, 1, 0, 1);
	}
}
