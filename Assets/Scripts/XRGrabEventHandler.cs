using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabEventHandler : MonoBehaviour
{
    public ulong avatarNetworkObjectId;
    public GameObject avatarObject;

    public void SelectLeftGrabEtnerEventController(SelectEnterEventArgs eventArgs) {
        var grabbedObj = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
        avatarObject.GetComponent<NetworkPlayer>().AvatarSelectGrabEnterEventHub(grabbedObj, true);
    }

    public void SelectLeftGrabExitEventController(SelectExitEventArgs eventArgs) {
        var grabbedObj = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
        avatarObject.GetComponent<NetworkPlayer>().AvatarSelectGrabExitEventHub(grabbedObj, true);
    }
    public void SelectRightGrabEtnerEventController(SelectEnterEventArgs eventArgs) {
        var grabbedObj = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
        avatarObject.GetComponent<NetworkPlayer>().AvatarSelectGrabEnterEventHub(grabbedObj, false);
    }

    public void SelectRightGrabExitEventController(SelectExitEventArgs eventArgs) {
        var grabbedObj = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
        avatarObject.GetComponent<NetworkPlayer>().AvatarSelectGrabExitEventHub(grabbedObj, false);
    }


}
