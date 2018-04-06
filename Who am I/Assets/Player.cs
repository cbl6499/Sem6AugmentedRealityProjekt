using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private string username;
    private int number;

    public Player(string username, int number) {
        this.Username = username;
        this.Number = number;
    }

    public string Username{ get; set; }

    public int Number { get; set; }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
