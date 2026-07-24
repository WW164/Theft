//using System.Collections;
//using NUnit.Framework;
//using Unity.Netcode;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.TestTools;

//public class MiningSyncTests
//{
//    [UnityTest]
//    public IEnumerator PlayerWallet_MoneyUpdates_WhenMined()
//    {
//        SceneManager.LoadScene("NetworkTestScene");
//        yield return null;

//        NetworkManager.Singleton.StartHost();
//        yield return null;

//        PlayerWallet wallet = Object.FindObjectOfType<PlayerWallet>();
//        Assert.IsNotNull(wallet, "PlayerWallet not found on spawned player");

//        int initialMoney = wallet.Money.Value;

//        wallet.AddMoney(10);
//        yield return null;

//        Assert.AreEqual(initialMoney + 10, wallet.Money.Value);

//        NetworkManager.Singleton.Shutdown();
//        yield return null;
//    }

//    [UnityTest]
//    public IEnumerator MineralNode_ExistsInScene_AndHasNetworkObject()
//    {
//        SceneManager.LoadScene("NetworkTestScene");
//        yield return null;

//        NetworkManager.Singleton.StartHost();
//        yield return null;

//        MineralNode node = Object.FindObjectOfType<MineralNode>();
//        Assert.IsNotNull(node, "MineralNode not found in scene");
//        Assert.IsNotNull(node.GetComponent<NetworkObject>(), "MineralNode missing NetworkObject component");

//        NetworkManager.Singleton.Shutdown();
//        yield return null;
//    }
//}