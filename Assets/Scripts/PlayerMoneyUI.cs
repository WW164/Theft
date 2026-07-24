using Unity.Netcode;
using UnityEngine;
using TMPro;

public class PlayerMoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    private PlayerWallet localWallet;

    private void Update()
    {
        // Keep searching until the local player's wallet is found and spawned
        if (localWallet == null)
        {
            FindLocalWallet();
            return;
        }
    }

    private void FindLocalWallet()
    {
        foreach (var wallet in FindObjectsOfType<PlayerWallet>())
        {
            // Only subscribe to the wallet owned by this client, not other players'
            if (wallet.IsOwner)
            {
                localWallet = wallet;
                localWallet.Money.OnValueChanged += OnMoneyChanged;
                UpdateText(localWallet.Money.Value); // Set initial value immediately
                break;
            }
        }
    }

    private void OnMoneyChanged(int previousValue, int newValue)
    {
        UpdateText(newValue);
    }

    private void UpdateText(int value)
    {
        moneyText.text = $"Money: {value}";
    }

    private void OnDestroy()
    {
        // Prevent memory leaks / errors if this UI is destroyed while still subscribed
        if (localWallet != null)
        {
            localWallet.Money.OnValueChanged -= OnMoneyChanged;
        }
    }
}