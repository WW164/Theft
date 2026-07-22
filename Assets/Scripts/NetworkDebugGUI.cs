using UnityEngine;
using Unity.Netcode;

public class NetworkDebugGUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 150));

        // Only show start buttons if networking hasn't started yet
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Start Host"))
            {
                NetworkManager.Singleton.StartHost(); // Acts as server and local player
            }
            if (GUILayout.Button("Start Client"))
            {
                NetworkManager.Singleton.StartClient(); // Connects to an existing host
            }
        }
        else
        {
            GUILayout.Label($"Mode: {(NetworkManager.Singleton.IsHost ? "Host" : "Client")}");
            GUILayout.Label($"Connected Clients: {NetworkManager.Singleton.ConnectedClients.Count}"); //Checks if connection is working
        }

        GUILayout.EndArea();
    }
}
