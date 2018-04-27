using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private int number;
    private string username;
    private string face;

    public Player(int number, string username) {
        this.Number = number;
        this.Username = username;
    }

    public string Username { get; set; }
    public int Number { get; set; }

    public string Face { get; set; }

}
