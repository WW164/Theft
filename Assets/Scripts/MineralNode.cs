using Unity.Netcode;
using UnityEngine;

public class MineralNode : NetworkBehaviour
{
    [SerializeField] private int moneyPerMine = 10;
    [SerializeField] private int totalMinerals = 100;

    private NetworkVariable<int> remainingMinerals = new NetworkVariable<int>(
        100,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private PlayerWallet playerInRange;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            remainingMinerals.Value = totalMinerals;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerWallet>(out PlayerWallet wallet) && wallet.IsOwner)
        {
            playerInRange = wallet;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerWallet>(out PlayerWallet wallet) && wallet == playerInRange)
        {
            playerInRange = null;
        }
    }

    private void Update()
    {
        if (playerInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            RequestMineServerRpc(playerInRange.NetworkObjectId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestMineServerRpc(ulong walletNetworkObjectId)
    {
        if (remainingMinerals.Value <= 0) return;

        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(walletNetworkObjectId, out NetworkObject netObj))
        {
            if (netObj.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
            {
                int amountMined = Mathf.Min(moneyPerMine, remainingMinerals.Value);
                remainingMinerals.Value -= amountMined;
                wallet.AddMoney(amountMined);

                // Once depleted, remove this node from the game for everyone
                if (remainingMinerals.Value <= 0)
                {
                    GetComponent<NetworkObject>().Despawn();
                }
            }
        }
    }
}