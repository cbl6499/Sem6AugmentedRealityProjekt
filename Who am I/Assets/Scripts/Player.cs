using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {// : MonoBehaviour {

    private int number;
    private string ip;

    public Player(int number, string ip) {
        this.Number = number;
        this.Ip = ip;
    }

    public int Number { get; set; }

    public string Ip { get; set; }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
