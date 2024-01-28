using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public CrewManager crewManager;
    [SerializeField] private GameObject[] accessories;

    private void Start()
    {
        int accIndex = Random.Range(0, accessories.Length);
        accessories[accIndex].SetActive(true);
    }

    private void FixedUpdate()
	{
        if(!crewManager || Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
				new Vector2(crewManager.transform.position.x, crewManager.transform.position.z)) > 100f || transform.position.y < -10f) //???
        {
			KillSelf();
            Destroy(gameObject, 0.2f); //0.2 so that the crew manager can find and remove it from the list without hitting a null reference
            //will probably increase the time till destroy to better show the people flying around
			enabled = false;
			GetComponent<Rigidbody>().AddForce(0f, 100f, 0f, ForceMode.VelocityChange);
        }
    }

    public void SetCrewManager(CrewManager manager)
    {
        crewManager = manager;
    }

	public void KillSelf() {
		crewManager?.KillPerson(this);
		crewManager = null;
	}
}