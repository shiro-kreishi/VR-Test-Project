using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class Fire : NetworkBehaviour {
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float speed = 20f;

    void Start() {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg) {
        if (IsServer) {
            SpawnBullet(); 
        }
        else {
            RequestFireServerRpc(); 
        }
    }

    
    private void SpawnBullet() {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * speed;
        bullet.GetComponent<NetworkObject>().Spawn();
        Destroy(bullet, 5);
    }

    [ServerRpc(RequireOwnership = false)] // Этот метод будет вызван на сервере по запросу клиента
    void RequestFireServerRpc(ServerRpcParams rpcParams = default) {
        SpawnBullet(); // Вызываем функцию создания пули на сервере
    }
}