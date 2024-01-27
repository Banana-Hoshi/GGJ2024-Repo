using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private CrewManager crewManager;
    private void Update()
    {
        if(Vector3.Distance(transform.position, transform.parent.position) > 150f) //?
        {
            crewManager.KillPerson(gameObject.name);
            Destroy(gameObject, 0.2f); //0.2 so that the crew manager can find and remove it from the list without hitting a null reference
            //will probably increase the time till destroy to better show the people flying around
        }
    }

    public void SetCrewManager(CrewManager manager)
    {
        crewManager = manager;
    }
}