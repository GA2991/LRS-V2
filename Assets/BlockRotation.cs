using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
void LateUpdate()
{
    transform.rotation = Quaternion.identity; // o la rotación que quieras mantener
}

}
