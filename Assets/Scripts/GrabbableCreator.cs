using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GrabbableCreator : NetworkBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int maxObjectsToSpawn = 5;

    public void SpawnGrabbables() {
        if (IsHost || IsServer) {
            Debug.Log("Spawning " + maxObjectsToSpawn + " grabbables ...");
            Vector2 placementAreaMin, placementAreaMax;
            placementAreaMin.x = transform.position.x - (transform.localScale.x) / 2;
            placementAreaMax.x = transform.position.x + (transform.localScale.x) / 2;
            placementAreaMin.y = transform.position.z + (transform.localScale.z) / 2;
            placementAreaMax.y = transform.position.z - (transform.localScale.z) / 2;

            for (int i = 0; i < maxObjectsToSpawn; i++) {
                GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)], Vector3.zero, Quaternion.identity);
                go.transform.position = new Vector3(
                    Random.Range(placementAreaMin.x, placementAreaMax.x),
                    transform.position.y + (transform.localScale.y / 2) + go.transform.localScale.y / 2,
                    Random.Range(placementAreaMin.y, placementAreaMax.y));
                go.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}
