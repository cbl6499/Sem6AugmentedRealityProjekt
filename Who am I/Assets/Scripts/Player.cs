using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {// : MonoBehaviour {

    private int number;
    //private string ip;
    private string username;
    private string face;

    public Player(int number, string username) {
        this.Number = number;
        this.Username = username;
    }

    public string Username { get; set; }
    public int Number { get; set; }

    public string Ip { get; set; }

    public string Face
    {
        get
        {
            return face;
        }

        set
        {
            face = value;
        }
    }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
