using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;

// MonoBehaviour�� �Բ� INetworkRunnerCallbacks �������̽��� ��ӹ޽��ϴ�.
public class LobbyNetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    // ��Ʈ��ũ ������ �����ϴ� �ٽ� ������Ʈ
    private NetworkRunner _runner;

    // ����(�κ� ��)�� ���۵� �� �ڵ����� ȣ��Ǵ� �Լ�
    void Start()
    {
        // ������ ������ �õ��ϴ� �Լ��� ȣ���մϴ�.
        ConnectToPhotonServer();
    }

    // �񵿱������� ���� ������ ������ �����ϴ� �Լ�
    async void ConnectToPhotonServer()
    {
        // NetworkRunner�� ���� ������ ���� �����մϴ�.
        if (_runner == null)
        {
            // �� ���ӿ�����Ʈ�� NetworkRunner ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ����ϴ�.
            _runner = gameObject.AddComponent<NetworkRunner>();
        }

        // �� ��ȯ�� ������ �⺻ �� �Ŵ����� �߰��մϴ�.
        gameObject.AddComponent<NetworkSceneManagerDefault>();

        // StartGame �޼ҵ带 ȣ���Ͽ� ���� ������ �����մϴ�.
        var result = await _runner.StartGame(new StartGameArgs()
        {
            // ���� ���: AutoHostOrClient�� ���� ������ ���� �����(Host), ������ ����(Client)�ϴ� ���� ����Դϴ�.
            GameMode = GameMode.AutoHostOrClient,

            // ���� �̸�(�� �̸�): ���� �̸��� ���� �÷��̾�鳢�� ������ �˴ϴ�.
            SessionName = "VRSmartCityLobby",

            // �ݹ� ������: �� ��ũ��Ʈ���� �߻��ϴ� ��Ʈ��ũ �̺�Ʈ�� �����ϵ��� �����մϴ�.
            
        });

        // ���� �õ� ����� ���� �α׸� ����մϴ�.
        if (result.Ok)
        {
            Debug.Log("���� ���� ���� ����!");
        }
        else
        {
            Debug.LogError($"���� ���� ����: {result.ShutdownReason}");
        }
    }

    // --- INetworkRunnerCallbacks �������̽� �ʼ� ���� �޼ҵ�� ---

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer: ������ ���������� ����Ǿ����ϴ�.");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"OnPlayerJoined: �÷��̾� {player.PlayerId}�� �����߽��ϴ�.");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"OnPlayerLeft: �÷��̾� {player.PlayerId}�� �����߽��ϴ�.");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogError($"OnConnectFailed: ���� ���ῡ �����߽��ϴ�. ����: {reason}");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log($"OnDisconnectedFromServer: ������ ������ ������ϴ�. ����: {reason}");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log($"OnShutdown: NetworkRunner�� ����Ǿ����ϴ�. ����: {shutdownReason}");
    }

    // -- ���� �� �������� �����ؾ� �ϴ� �޼ҵ�� --

    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}