using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReadInformations : MonoBehaviour {

	private string information;
	public SelectPerson person;
	public GameObject panel;
	public GameObject infoCan;
	public string infoStr;

	public Text info;

	void Start () {
		//activePerson = person.loadInformations ();

		//Read the text from directly from the test.txt file
		TextAsset infos = Resources.Load(person.getName()) as TextAsset;
		info.text = infos.text.ToString ();
	}


	public void closeInfoScreen(){
		panel.SetActive (false);
		infoCan.SetActive (true);
	}
}

