using System.Collections;
using NUnit.Framework;
using Unity.Netcode;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class PlayerSpawnTests
{
    [UnityTest]
    public IEnumerator HostStart_SpawnsOnePlayer()
    {

        // Load the scene containing the NetworkManager GameObject
        SceneManager.LoadScene("NetworkTestScene");
        yield return null; // Wait a frame for the scene to load

        NetworkManager.Singleton.StartHost();
        yield return null; // Wait a frame for spawn to complete

        Assert.AreEqual(1, NetworkManager.Singleton.ConnectedClients.Count);

        NetworkManager.Singleton.Shutdown(); // Cleans up so the next test run starts fresh
        yield return null;
    }
}
