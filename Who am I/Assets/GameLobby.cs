using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby : MonoBehaviour {

    private string identifier;
    private List<Player> players;
    private Player owner;
    private int currentPlayerCount;

    public Player Owner{ get; set; }

    public List<Player> Players{ get; set; }

    public int CurrentPlayerCount { get; set; }

    public string Identifier { get; set; }

    public GameLobby(string owner) {
        this.CurrentPlayerCount = 0;
        this.Owner = CreatePlayer(owner);
        this.Players = new List<Player>();
    }

    public void CreateIdentifier() {
        //TODO: create unique identifier
        this.Identifier = "SampleIdentifier";
    }

    public void RegisterPlayer(string username) {
        players.Add(CreatePlayer(username));
    }

    private Player CreatePlayer(string username) {
        this.CurrentPlayerCount += 1;
        return new Player(username, this.CurrentPlayerCount);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
