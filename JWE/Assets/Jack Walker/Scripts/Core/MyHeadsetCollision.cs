using FPS.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MyHeadsetCollision : VRTK_HeadsetCollision
{
    protected override void CreateHeadsetColliderContainer()
    {
        headsetColliderContainer = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, "HeadsetColliderContainer"));
        headsetColliderContainer.transform.position = Vector3.zero;
        headsetColliderContainer.transform.localRotation = headset.localRotation;
        headsetColliderContainer.transform.localScale = Vector3.one;
        headsetColliderContainer.layer = LayerMask.NameToLayer("Player");
    }
}
