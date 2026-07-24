using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameTimer : NetworkBehaviour
{
    public NetworkVariable<float> TimeRemaining = new NetworkVariable<float>(10f);
    public NetworkVariable<ulong> WinnerClientId = new NetworkVariable<ulong>(0);
    public NetworkVariable<bool> GameEndedNetVar = new NetworkVariable<bool>(false);

    public bool GameEnded => GameEndedNetVar.Value;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            TimeRemaining.Value = 10f;
            GameEndedNetVar.Value = false;
        }
    }

    void Update()
    {
        if (!IsServer || GameEndedNetVar.Value) return;

        TimeRemaining.Value -= Time.deltaTime;

        if (TimeRemaining.Value <= 0f)
        {
            TimeRemaining.Value = 0f;
            EndGame();
        }
    }

    void EndGame()
    {
        if (GameEndedNetVar.Value) return;

        GameEndedNetVar.Value = true;

        Dictionary<ulong, int> playerMoney = new Dictionary<ulong, int>();

        foreach (var clientPair in NetworkManager.Singleton.ConnectedClients)
        {
            ulong clientId = clientPair.Key;
            NetworkClient client = clientPair.Value;

            if (client.PlayerObject == null) continue;

            PlayerWallet wallet = client.PlayerObject.GetComponent<PlayerWallet>();
            if (wallet != null)
            {
                playerMoney[clientId] = wallet.Money.Value;
            }
        }

        if (playerMoney.Count == 0)
        {
            Debug.LogWarning("EndGame called but no player wallets found.");
            return;
        }

        ulong winnerId = GameWinChecker.DetermineWinner(playerMoney);
        WinnerClientId.Value = winnerId;

        AnnounceWinnerClientRpc(winnerId);
    }

    [ClientRpc]
    void AnnounceWinnerClientRpc(ulong winnerId)
    {
        bool isLocalPlayerWinner = winnerId == NetworkManager.Singleton.LocalClientId;

        if (isLocalPlayerWinner)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log($"Player {winnerId} wins!");
        }

        GameOverUI.Instance.ShowResult(winnerId, isLocalPlayerWinner);
    }
}