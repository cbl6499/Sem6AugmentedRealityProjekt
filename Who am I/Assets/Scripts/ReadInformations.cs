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

		string path = "Assets/Informations/" + person.getName() +".txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path, System.Text.Encoding.UTF8); 


		while (reader.Peek() >= 0) 
		{
			information += reader.ReadLine().ToString();
			information += "\n";

		}
		info.text = information;


		//Debug.Log(path);
		reader.Close();
	}


	public void closeInfoScreen(){
		panel.SetActive (false);
		infoCan.SetActive (true);
	}
}

