using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject targetToFollow;

    void Update()
    {
        transform.position = targetToFollow.transform.position;
    }
}