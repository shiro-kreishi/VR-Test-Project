using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    void Update()
    {
        if (IsOwner)
        {
            root.position = VrRigPreference.Singleton.root.position;
            root.rotation = VrRigPreference.Singleton.root.rotation;
            head.position = VrRigPreference.Singleton.head.position;
            head.rotation = VrRigPreference.Singleton.head.rotation;
            leftHand.position = VrRigPreference.Singleton.leftHand.position;
            leftHand.rotation = VrRigPreference.Singleton.leftHand.rotation;
            rightHand.position = VrRigPreference.Singleton.rightHand.position;
            rightHand.rotation = VrRigPreference.Singleton.rightHand.rotation;
        }
        
    }
}
