using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class NetworkConnect : MonoBehaviour
{
    public int maxConnection = 6;
    public UnityTransport transport;

    private Lobby currentlobby;
    private float heartBeatTimer;

    [SerializeField] private Vector3 ObjectSpawnerPosition;

    private async void Awake() {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        JoinOrCreate();


    }

    public async void JoinOrCreate() {
        try {
            currentlobby = await Lobbies.Instance.QuickJoinLobbyAsync();
            string relayJoinCode = currentlobby.Data["JOIN_CODE"].Value;

            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);

            transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);



            NetworkManager.Singleton.StartClient();
        }
        catch {
            Create();
        }
    }

    public async void Create()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.Log(newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);


        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;
        lobbyOptions.Data = new Dictionary<string, DataObject>();
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, newJoinCode);
        lobbyOptions.Data.Add("JOIN_CODE", dataObject);

        currentlobby = await Lobbies.Instance.CreateLobbyAsync("Lobby Name", maxConnection, lobbyOptions);

        NetworkManager.Singleton.StartHost();

        //GameObject spawner = Resources.Load("Table") as GameObject;
        //GameObject go = Instantiate(spawner, ObjectSpawnerPosition, Quaternion.identity);
        //go.GetComponent<NetworkObject>().Spawn();
        //go.GetComponent<GrabbableCreator>().SpawnGrabbables();
    }

    public async void Join()
    {
        currentlobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        string relayJoinCode = currentlobby.Data["JOIN_CODE"].Value;

        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

        

        NetworkManager.Singleton.StartClient();
    }

    private void Update() {
        if (heartBeatTimer > 15) {
            heartBeatTimer -= 15;

            if (currentlobby != null && currentlobby.HostId == AuthenticationService.Instance.PlayerId) {
                LobbyService.Instance.SendHeartbeatPingAsync(currentlobby.Id);
            }
        }

        heartBeatTimer += Time.deltaTime;
    }
}
