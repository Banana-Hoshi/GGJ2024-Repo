using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThingSpawner : MonoBehaviour
{
	[SerializeField] Vector2 minRange = Vector2.one * -400f;
	[SerializeField] Vector2 maxRange = Vector2.one * 400f;
	[SerializeField] Vector2 timerRange = new Vector2(3f, 6f);
	[SerializeField] GameObject[] things;
	[SerializeField] int initialThings = 20;
	List<Transform> players = new List<Transform>();
	private void Start() {
		StartCoroutine(TimerSpawn());
	}

	IEnumerator TimerSpawn() {
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < initialThings; ++i) {
			SpawnThing();
		}
		while (true) {
			yield return new WaitForSeconds(Random.Range(timerRange.x, timerRange.y));
			SpawnThing();
		}
	}

	void SpawnThing() {
		GameObject thing = Instantiate(things[Random.Range(0, things.Length)],
			transform.position + new Vector3(Random.Range(minRange.x, maxRange.x), 0f, Random.Range(minRange.y, maxRange.y)),
			Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
		
		if (thing.GetComponent<AIBoat>()) {
			thing.GetComponent<AIBoat>().target = GetPlayer();
		}
	}

	public void SetPlayer(PlayerInput player) {
		players.Add(player.transform);
		player.transform.position = new Vector3(Random.Range(minRange.x, maxRange.x), 5f, Random.Range(minRange.y, maxRange.y));
		player.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
		foreach (AIBoat boat in FindObjectsByType<AIBoat>(FindObjectsSortMode.None)) {
			boat.target = GetPlayer();
		}
	}

	public void DeadPlayer(PlayerInput player) {
		players.Remove(player.transform);
		foreach (AIBoat boat in FindObjectsByType<AIBoat>(FindObjectsSortMode.None)) {
			boat.target = GetPlayer();
		}
	}

	Transform GetPlayer() {
		if (players.Count > 0)
			return players[Random.Range(0, players.Count)];
		else
			return null;
	}
}
