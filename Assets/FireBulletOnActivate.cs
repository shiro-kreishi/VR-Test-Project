using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : NetworkBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    private BulletSpawner bulletSpawnScript;

    private void Start() {
        StartShot();
    }
    void StartShot()
    {
        GameObject bulletSpawnerObject = GameObject.FindGameObjectWithTag("BulletSpawn");
        if (bulletSpawnerObject != null)
        {
            //Debug.Log("Все норм!");
            bulletSpawnScript = bulletSpawnerObject.GetComponent<BulletSpawner>();
            if (bulletSpawnScript != null)
            {
                //Debug.Log("Скрипт Найден!");
                XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

                grabbable.activated.AddListener((ActivateEventArgs arg) => FireBullet(spawnPoint.transform.position, spawnPoint.rotation));
            }
            else
            {
                Debug.LogWarning("Скрипт не найдн XD");
            }
        }
        else
        {
            Debug.LogWarning("Объект не найден LOL");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void FireBullet(Vector3 position, Quaternion rotation)
    {
        bulletSpawnScript.SpawnBullet(position, rotation);
    }
}
