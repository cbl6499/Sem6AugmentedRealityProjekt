using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextMesh textObject = GameObject.Find("Info").GetComponent<TextMesh>();
		textObject.text = "Obama \n President of the USA from 2008 - 2016";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
