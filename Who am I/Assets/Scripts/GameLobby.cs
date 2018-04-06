using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby : MonoBehaviour {

    private string identifier;
    private List<Player> players;
    private Player owner;
    private int currentPlayerCount;
    private static GameLobby instance;


    public Player Owner{ get; set; }

    public List<Player> Players{ get; set; }

    public static GameLobby Instance{
        get {
            if(instance == null) {
                instance = new GameLobby("test");
            }
            return instance;
        }
        set {
            instance = value;
        }
    }


    public int CurrentPlayerCount { get; set; }

    public string Identifier { get; set; }

    private GameLobby(string owner) {
        this.CurrentPlayerCount = 0;
        this.Owner = CreatePlayer(owner, Network.player.ipAddress);
        this.Players = new List<Player>();
    }

    public void CreateIdentifier() {
        //TODO: create unique identifier
        this.Identifier = "SampleIdentifier";
    }

    public void RegisterPlayer(string username, string ip) {
        players.Add(CreatePlayer(username, ip));
    }

    private Player CreatePlayer(string username, string ip) {
        this.CurrentPlayerCount += 1;
        return new Player(username, this.CurrentPlayerCount, ip);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
    }

    public void StartGame() {
        //start the game
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
