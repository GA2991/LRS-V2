using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimator : MonoBehaviour
{
    public WheelCollider frontLeftCollider;
    public GameObject frontLeftMesh;

    public WheelCollider frontRightCollider;
    public GameObject frontRightMesh;

    public WheelCollider rearLeftCollider;
    public GameObject rearLeftMesh;

    public WheelCollider rearRightCollider;
    public GameObject rearRightMesh;

    private void LateUpdate()
    {
        // Este método se ejecuta siempre, incluso si el controlador del coche está desactivado
        AnimateWheelMeshes();
    }

    void AnimateWheelMeshes()
    {
        try
        {
            Quaternion FLWRotation;
            Vector3 FLWPosition;
            frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
            frontLeftMesh.transform.position = FLWPosition;
            frontLeftMesh.transform.rotation = FLWRotation;

            Quaternion FRWRotation;
            Vector3 FRWPosition;
            frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
            frontRightMesh.transform.position = FRWPosition;
            frontRightMesh.transform.rotation = FRWRotation;

            Quaternion RLWRotation;
            Vector3 RLWPosition;
            rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
            rearLeftMesh.transform.position = RLWPosition;
            rearLeftMesh.transform.rotation = RLWRotation;

            Quaternion RRWRotation;
            Vector3 RRWPosition;
            rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
            rearRightMesh.transform.position = RRWPosition;
            rearRightMesh.transform.rotation = RRWRotation;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }
}