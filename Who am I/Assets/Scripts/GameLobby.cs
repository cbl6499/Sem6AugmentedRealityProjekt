using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby {// : MonoBehaviour {

    private List<Player> players;
    private Player owner;
    private int currentPlayerCount;
    private int size;
    private static GameLobby instance;

    public Player Owner{ get; set; }

    public List<Player> Players{ get; set; }

    public int Size { get; set; }

    public static GameLobby Instance{
        get {
            if(instance == null) {
                instance = new GameLobby();
            }
            return instance;
        }
        set {
            instance = value;
        }
    }

    public int CurrentPlayerCount { get; set; }

    private GameLobby() {
        this.CurrentPlayerCount = 0;
        this.Players = new List<Player>();
    }

    private GameLobby(string owner) {
        this.CurrentPlayerCount = 0;
        this.Owner = CreatePlayer(owner, Network.player.ipAddress);
        this.Players = new List<Player>();
        this.Players.Add(this.Owner);
    }

    public void SetOwner(string owner, string ip) {
        this.Owner = CreatePlayer(owner, ip);
        if (!this.Players.Contains(this.Owner)) {
            this.Players.Add(Owner);
        }
    }

    public void RegisterPlayer(string username, string ip) {
        Debug.Log("Registered player: " + username);
        players.Add(CreatePlayer(username, ip));
    }

    private Player CreatePlayer(string username, string ip) {
        this.CurrentPlayerCount += 1;
        return new Player(username, this.CurrentPlayerCount, ip);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
    }
/*
    public void StartGame() {
        NetworkHandler handler = NetworkHandler.Instance;
        handler.BroadCastReady();
    }
    */
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
