//using NUnit.Framework;
//using System.Collections;
//using System.Threading;
//using Unity.Netcode.Runtime;
//using UnityEngine.TestTools;

//public class MultiClientConnectionTests : NetcodeIntegrationTest
//{
//    protected override int NumberOfClients => 2;

//    [UnityTest]
//    public IEnumerator TwoClients_ConnectSuccessfully()
//    {
//        yield return WaitForClientsConnectedOrTimeOut();
//        Assert.IsFalse(TimedOut, "Clients failed to connect within timeout");
//        Assert.AreEqual(2, m_ClientNetworkManagers.Length);
//    }
//}