using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class BulletSpawner : NetworkBehaviour
{
    public GameObject bulletPrefab;
    float bulletSpeed = 20f;


    public void SpawnBullet(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
        NetworkObject networkObject = bullet.GetComponent<NetworkObject>();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bullet.transform.forward * bulletSpeed;
        }
        if (networkObject != null)
        {
            networkObject.Spawn();
        }
    }
}