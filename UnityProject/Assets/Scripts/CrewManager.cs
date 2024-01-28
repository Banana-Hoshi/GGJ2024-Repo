using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrewManager : MonoBehaviour
{
    [SerializeField] private GameObject personPrefabs;
    private List<Person> crewList = new List<Person>();

    public void SpawnPerson(Vector3 location, Quaternion rotation, Transform parent)
    {
        GameObject person = Instantiate(personPrefabs, location, rotation, parent);
        person.GetComponent<Person>().SetCrewManager(this);
        crewList.Add(person.GetComponent<Person>());
    }

    public int GetCrewCount()
    {
        return crewList.Count;
    }

	public void KillCrew() {
		foreach (Person person in crewList) {
			person.SetCrewManager(null);
		}
		crewList.Clear();
	}

    public void KillPerson(Person person)
    {
        crewList.Remove(person);
    }

    public void AddPersonToCrew(Person person)
    {
        crewList.Add(person);
		person.SetCrewManager(this);
    }
}