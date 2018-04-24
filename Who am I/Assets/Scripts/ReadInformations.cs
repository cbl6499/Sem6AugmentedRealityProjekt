using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadInformations : MonoBehaviour {

	private string information;
	private SelectPerson person;
	private GameObject activePerson;

	void Start () {
		activePerson = person.loadInformations();
		string path = "Assets/Informations/obama.txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path); 
		information = reader.ToString();

		GameObject text = new GameObject();
		TextMesh t = text.AddComponent<TextMesh>();
		t.text = information;
		t.fontSize = 14;


		Debug.Log(reader.ReadToEnd());
		reader.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
