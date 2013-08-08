using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.gui;
using Assets.Scripts.network;
using ProtoShared.Packets.FromClient;
/// <summary>
/// Home screens menu handler.
/// </summary>
public class StartMenu : MonoBehaviour {
    private bool bLoadMenu;
	NetworkManager _network;
	
	
    public static class StartValues
    {
        public static string SaveGameToLoad;
        public static bool bLoadSaveGame;
    }
	
	private int XCentre = Screen.width / 2;
	private int YCentre = Screen.height / 2;
    private bool bStarted;
	
	// Use this for initialization
	void Start () {
		
	}

    void LateStart() {
        _network = GameObject.FindGameObjectWithTag("GameController").GetComponent<NetworkManager>();

        TextBox userName = new TextBox("user", new Rect(XCentre - 50, YCentre - 200, 100, 50));
        TextBox password = new TextBox("password", new Rect(XCentre - 50, YCentre - 150, 100, 50));
        Button b = new Button("Connect", XCentre - 50, YCentre - 100);
        b.OnClick += delegate(Button sender)
        {
            _network.Connect(userName.Text, password.Text);
            userName.Enabled = false;
            password.Enabled = false;
            b.Enabled = false;
        };
        _network.OnShowCharSelect += _network_OnShowCharSelect;
        _network.OnEnterWorld += _network_OnEnterWorld;
        bStarted = true;
    }

    private void _network_OnEnterWorld(ProtoShared.Packets.FromServer.EnterWorld msg) {
        DarkGUI.Instance.ClearAllUI();
        Application.LoadLevel(msg.Toon.SceneSerial);
        Logger.Log("Entering world..." + msg.Toon.SceneSerial);


    }

    void _network_OnShowCharSelect(ProtoShared.Packets.FromServer.LoginResponse msg) {
        int index = 0;
        if(msg.Characters != null)
        foreach (var Char in msg.Characters)
            new Button(Char, XCentre - 50, YCentre - (index * 50)).setTag(index).OnClick += delegate(Button sender) { _network.Send(new SelectCharacter((int)sender.Tag)); };
        new Button("Create new Toon", XCentre - 50, YCentre + 50).OnClick += delegate(Button sender) {  DrawCreateToonMenu(); };
    }

    private void DrawCreateToonMenu() {
        DarkGUI.Instance.ClearAllUI();
        var charName = new TextBox("charName", new Rect(XCentre - 50, YCentre, 100, 50));
        var isMale = new CheckBox("isMale?", new Rect(XCentre - 50, YCentre - 50, 100, 50));
        new Button("Create!", XCentre - 50, YCentre - 150).OnClick += delegate(Button sender) { _network.Send(new CreateCharacter() { Name = charName.Text, isMale = isMale.Checked }); };
    }
	
	// Update is called once per frame
	void Update () {
        if (!bStarted)
            LateStart();
	}

    void OnGUI() {
           
        
      
       
    }
}
