using System.Collections;
using NUnit.Framework;
using Unity.Netcode;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class NetworkConnectionTests
{
    [UnityTest]
    public IEnumerator HostStarts_PlayerSpawns()
    {
        SceneManager.LoadScene("NetworkTestScene");
        yield return null; // wait for scene load

        NetworkManager.Singleton.StartHost();
        yield return null; // wait for spawn

        Assert.IsTrue(NetworkManager.Singleton.IsHost);
        Assert.IsTrue(NetworkManager.Singleton.ConnectedClients.Count > 0);

        NetworkManager.Singleton.Shutdown();
        yield return null;
    }
}