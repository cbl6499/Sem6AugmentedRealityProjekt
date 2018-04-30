using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class SelectPerson : MonoBehaviour {

	public GameObject Person1;
	public GameObject Person2;
	public GameObject Person3;
	public GameObject Person4;
	public GameObject Person5;
	public GameObject Person6;
	private GameObject activePerson;
	public GameObject selectPlayerList;
	public GameObject selectButton;
	public GameObject infoCanvas;
	public GameObject submit;
	public GameObject panel;
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

    public void DisableAll() {
        Person1.SetActive(false);
        Person2.SetActive(false);
        Person3.SetActive(false);
        Person4.SetActive(false);
        Person5.SetActive(false);
        Person6.SetActive(false);
    }

	public void setFinalPlayer(){
		/*selectPlayerList.SetActive (false);
		selectButton.SetActive (false);
		infoCanvas.SetActive (true);*/
		submit.SetActive (true);
		Debug.Log ("Tuan do mol an debug inne");
        GameManager.Instance.FixSelection(activePerson, this.gameObject, selectPlayerList, selectButton, infoCanvas);
	}

	public GameObject loadInformations(){
		return activePerson;
	}

	public string getName(){
		return activePerson.gameObject.name;
	}

	public void setStuffVisible(){
		infoCanvas.SetActive (false);
		panel.SetActive (true);
	}

	public void Restart(){
		Person1.SetActive(false);
		Person2.SetActive(false);
		Person3.SetActive(false);
		Person4.SetActive(false);
		Person5.SetActive(false);
		Person6.SetActive(false);
		infoCanvas.SetActive (false);
		selectButton.SetActive (true);
		selectPlayerList.SetActive (true);
	}
}
