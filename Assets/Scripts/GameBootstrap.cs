using Unity.Netcode;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject gameTimerPrefab;

    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnGameTimer;
    }

    void SpawnGameTimer()
    {
        GameObject timerInstance = Instantiate(gameTimerPrefab);
        timerInstance.GetComponent<NetworkObject>().Spawn();
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.OnServerStarted -= SpawnGameTimer;
    }
}