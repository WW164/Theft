using Unity.Netcode;
using UnityEngine;

public class PlayerWallet : NetworkBehaviour
{
    public NetworkVariable<int> Money = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public void AddMoney(int amount)
    {
        if (!IsServer) return; // Only server can modify money to prevent cheating
        Money.Value += amount;
    }
}