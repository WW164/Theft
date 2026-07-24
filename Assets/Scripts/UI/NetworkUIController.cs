using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkUIController : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_Text statusText;

    private void Awake()
    {
        hostButton.onClick.AddListener(OnHostClicked);
        clientButton.onClick.AddListener(OnClientClicked);
    }

    private void OnHostClicked()
    {
        NetworkManager.Singleton.StartHost();
        UpdateStatus();
    }

    private void OnClientClicked()
    {
        NetworkManager.Singleton.StartClient();
        UpdateStatus();
    }

    private void Update()
    {
        if (NetworkManager.Singleton == null) return;

        // Keep status live-updated in case connection state changes
        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
        {
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        string mode = NetworkManager.Singleton.IsHost ? "Host" : "Client";
        int count = NetworkManager.Singleton.ConnectedClients.Count;
        statusText.text = $"Mode: {mode} | Connected: {count}";
    }
}