using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed = 5;
    public float sprintSpeed = 10;
    public float jumpHeight = 15;
    public PhysicalCC physicalCC;

    public Transform bodyRender;
    IEnumerator sitCort;
    public bool isSitting;

    private float baseSpeed;
    private float baseJumpHeight;
    private float baseHeight;
    private Vector3 baseBodyScale;
    private CharacterController cc;

    void Start()
    {
        if (physicalCC == null || bodyRender == null)
        {
            Debug.LogError("PlayerInput: physicalCC o bodyRender no están asignados en el Inspector");
            return;
        }

        // Obtener CharacterController directamente
        cc = GetComponent<CharacterController>();
        if (cc == null)
        {
            Debug.LogError("PlayerInput: No hay CharacterController en este GameObject");
            return;
        }

        baseSpeed = speed;
        baseJumpHeight = jumpHeight;
        baseHeight = cc.height;
        baseBodyScale = bodyRender.localScale;
    }

    void Update()
    {
        if (physicalCC == null || cc == null)
            return;

        if (physicalCC.isGround)
        {
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
            
            physicalCC.moveInput = Vector3.ClampMagnitude(transform.forward
                            * Input.GetAxis("Vertical")
                            + transform.right
                            * Input.GetAxis("Horizontal"), 1f) * currentSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                physicalCC.inertiaVelocity.y = 0f;
                physicalCC.inertiaVelocity.y += jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.C) && sitCort == null)
            {
                sitCort = sitDown();
                StartCoroutine(sitCort);
            }
        }
    }

    IEnumerator sitDown()
    {
        if (physicalCC == null || cc == null)
            yield break;

        Vector3 raycastOrigin = transform.position + Vector3.up * baseHeight;
        if (isSitting && Physics.Raycast(raycastOrigin, Vector3.up, baseHeight * 0.5f))
        {
            sitCort = null;
            yield break;
        }
        isSitting = !isSitting;

        float t = 0;
        float startHeight = cc.height;
        float finalHeight = isSitting ? baseHeight * 0.5f : baseHeight;

        Vector3 startBodySize = bodyRender.localScale;
        Vector3 finalBodySize = isSitting ? baseBodyScale * 0.5f : baseBodyScale;

        speed = isSitting ? baseSpeed * 0.5f : baseSpeed;
        jumpHeight = isSitting ? baseJumpHeight / 3 : baseJumpHeight;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            cc.height = Mathf.Lerp(startHeight, finalHeight, t / 0.2f);
            bodyRender.localScale = Vector3.Lerp(startBodySize, finalBodySize, t / 0.2f);
            yield return null;
        }

        sitCort = null;
        yield break;
    }
}