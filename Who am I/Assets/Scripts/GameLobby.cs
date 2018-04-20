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
        this.Owner = CreatePlayer(Network.player.ipAddress);
        this.Players = new List<Player>();
        this.Players.Add(this.Owner);
    }

    public void SetOwner(string ip) {
        this.Owner = CreatePlayer(ip);
        if (!this.Players.Contains(this.Owner)) {
            this.Players.Add(Owner);
        }
    }

    public void RegisterPlayer(string ip) {
        Debug.Log("Registered player: ");
        players.Add(CreatePlayer(ip));
    }

    private Player CreatePlayer(string ip) {
        this.CurrentPlayerCount += 1;
        return new Player(this.CurrentPlayerCount, ip);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
        this.CurrentPlayerCount -= 1;
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
