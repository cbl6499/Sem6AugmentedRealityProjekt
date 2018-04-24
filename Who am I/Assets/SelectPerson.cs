using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SelectPerson : MonoBehaviour {

	public GameObject Person1;
	public GameObject Person2;
	public GameObject Person3;
	public GameObject Person4;
	public GameObject Person5;
	public GameObject Person6;
	public GameObject activePerson;
	// Use this for initialization
	void Start () {
		Person2.SetActive (false);
		Person3.SetActive (false);
		Person4.SetActive (false);
		Person4.SetActive (false);
		activePerson = Person1;
	}
	
	public void LoadPerson1(){
		Person1.SetActive (true);
		Person2.SetActive (false);
		Person3.SetActive (false);
		Person4.SetActive (false);
		Person5.SetActive (false);
		Person6.SetActive (false);
		activePerson = Person1;
	}

	public void LoadPerson2(){
		Person1.SetActive (false);
		Person2.SetActive (true);
		Person3.SetActive (false);
		Person4.SetActive (false);
		Person5.SetActive (false);
		Person6.SetActive (false);
		activePerson = Person2;
	}

	public void LoadPerson3(){
		Person1.SetActive (false);
		Person2.SetActive (false);
		Person3.SetActive (true);
		Person4.SetActive (false);
		Person5.SetActive (false);
		Person6.SetActive (false);
		activePerson = Person3;
	}

	public void LoadPerson4(){
		Person1.SetActive (false);
		Person2.SetActive (false);
		Person3.SetActive (false);
		Person4.SetActive (true);
		Person5.SetActive (false);
		Person6.SetActive (false);
		activePerson = Person4;
	}

	public void LoadPerson5(){
		Person1.SetActive (false);
		Person2.SetActive (false);
		Person3.SetActive (false);
		Person4.SetActive (false);
		Person5.SetActive (true);
		Person6.SetActive (false);
		activePerson = Person5;
	}

	public void LoadPerson6(){
		Person1.SetActive (false);
		Person2.SetActive (false);
		Person3.SetActive (false);
		Person4.SetActive (false);
		Person5.SetActive (false);
		Person6.SetActive (true);
		activePerson = Person6;
	}

	public GameObject loadInformations(){
		return activePerson;
	}
}
