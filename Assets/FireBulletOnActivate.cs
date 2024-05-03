using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
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
            //Debug.Log("��� ����!");
            bulletSpawnScript = bulletSpawnerObject.GetComponent<BulletSpawner>();
            if (bulletSpawnScript != null)
            {
                //Debug.Log("������ ������!");
                XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

                grabbable.activated.AddListener((arg) => {
                    FireBullet(spawnPoint.transform.position, spawnPoint.rotation);

                });
            }
            else
            {
                Debug.LogWarning("������ �� ����� XD");
            }
        }
        else
        {
            Debug.LogWarning("������ �� ������ LOL");
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
