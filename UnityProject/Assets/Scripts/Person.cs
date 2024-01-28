using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    CrewManager crewManager;
    private void FixedUpdate()
	{
        if(!crewManager || Vector3.Distance(transform.position, crewManager.transform.position) > 150f) //???
        {
			KillSelf();
            Destroy(gameObject, 0.2f); //0.2 so that the crew manager can find and remove it from the list without hitting a null reference
            //will probably increase the time till destroy to better show the people flying around
			enabled = false;
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