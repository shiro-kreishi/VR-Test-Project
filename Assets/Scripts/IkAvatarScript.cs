using UnityEngine;

[System.Serializable]
public class MapTransform {
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar() {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class IkAvatarScript : MonoBehaviour {
    public MapTransform head;

    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private MapTransform leftHandRig;
    [SerializeField] private MapTransform rightHandRig;

    [SerializeField] private float turnSmoothness;

    [SerializeField] private Transform IKHead;

    [SerializeField] private Vector3 headBodyOffset;

    private float yOffset = 0;

    void LateUpdate() {
        transform.position = IKHead.position + headBodyOffset + new Vector3(0, yOffset, 0);
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness); ;
        
        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
        leftHandRig.MapVRAvatar();
        rightHandRig.MapVRAvatar();
    }

    public void ChengeRost(float value)
    {
        yOffset = value;
    }
}