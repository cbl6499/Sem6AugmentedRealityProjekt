using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private int number;
    private string username;
    private string face;
    private int points;
    private bool faceSet;

    public Player(int number, string username) {
        this.Number = number;
        this.Username = username;
        this.Points = 0;
        this.FaceSet = false;
    }

    public bool FaceSet { get; set; }

    public void AddPoints(int points) {
        points += points;
    }

    public int Points { get; set; }
    public string Username { get; set; }
    public int Number { get; set; }

    public string Face { get; set; }

    public bool Equals(Player player) {
        if (this.number == player.Number && this.username == player.Username) {
            return true;
        }
        return false;
    }

}
