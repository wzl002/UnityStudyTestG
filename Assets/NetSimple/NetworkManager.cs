using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestMessage : MessageBase
{
	public string testMsgStr;
}

enum MessageId { TEST_MESSAGE=100, TEST_ACK_MESSAGE, USER_TEST_MESSAGE };

public class NetworkManager : MonoBehaviour {

	public bool isAtStartup = true;

	private NetworkClient myClient;
	private string debugMsg = "";

	// Use this for initialization
	void Start () {
		myClient = null;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isAtStartup) {
			if (Input.GetKeyDown (KeyCode.S)) {
				SetupServer ();
			}

			if (Input.GetKeyDown (KeyCode.C)) {
				SetupClient ();
			}

			if (Input.GetKeyDown (KeyCode.B)) {
				SetupServer ();
				SetupLocalClient ();
			}
		} else {
			if (Input.GetKeyDown (KeyCode.T)) {
				if (myClient != null) {
					TestMessage msg = new TestMessage ();
					msg.testMsgStr = "User test message from client";
					myClient.Send ((short)MessageId.USER_TEST_MESSAGE, msg);
				}
			}
		}
	}

	void OnGUI()
	{
		if (isAtStartup) {
			GUI.Label (new Rect (2, 10, 150, 100), "Press S for server");     
			GUI.Label (new Rect (2, 30, 150, 100), "Press B for both");       
			GUI.Label (new Rect (2, 50, 150, 100), "Press C for client");
		} else {
			GUI.Label (new Rect (100, 100, 500, 200), debugMsg);
		}
	}   

	// Create a server and listen on a port
	public void SetupServer()
	{
		NetworkServer.Listen(4444);
		NetworkServer.RegisterHandler((short)MessageId.TEST_MESSAGE, ReceiveTestMsgFromClient);
		NetworkServer.RegisterHandler((short)MessageId.USER_TEST_MESSAGE, ReceiveTestMsgFromClient);
		isAtStartup = false;
	}

	// Create a client and connect to the server port
	public void SetupClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler((short)MessageId.TEST_ACK_MESSAGE, ReceiveTestMsgFromServer);
		myClient.Connect("127.0.0.1", 4444);
		isAtStartup = false;
	}

	// Create a local client and connect to the local server
	public void SetupLocalClient()
	{
		myClient = ClientScene.ConnectLocalServer();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler((short)MessageId.TEST_ACK_MESSAGE, ReceiveTestMsgFromServer);
		isAtStartup = false;
	}

	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		debugMsg = "Connected to server";
		SendTestMsgToServer ();
	}

	public void ReceiveTestMsgFromClient(NetworkMessage netMsg)
	{
		TestMessage msg = netMsg.ReadMessage<TestMessage>();
		Debug.Log("Got test message from client: <" + msg.testMsgStr + ">");
		debugMsg = "Got test message from client: <" + msg.testMsgStr + ">";
		SendTestMsgToClient(netMsg.conn.connectionId, "ACK response from server");
	}

	public void SendTestMsgToClient(int nc, string testMsgStr)
	{
		TestMessage msg = new TestMessage();
		msg.testMsgStr = testMsgStr;
		NetworkServer.SendToClient (nc, (short)MessageId.TEST_ACK_MESSAGE, msg);
	}

	public void ReceiveTestMsgFromServer(NetworkMessage netMsg)
	{
		TestMessage msg = netMsg.ReadMessage<TestMessage> ();
		Debug.Log("Got test message from server: <" + msg.testMsgStr + ">");
		debugMsg = "Got test message from server: <" + msg.testMsgStr + ">";
	}

	public void SendTestMsgToServer()
	{
		TestMessage msg = new TestMessage ();
		msg.testMsgStr = "Test message from client";
		myClient.Send ((short)MessageId.TEST_MESSAGE, msg);
	}
}
