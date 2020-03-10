using VRTK;
using UnityEngine;

public class BodyPhysics : VRTK_BodyPhysics
{
    protected override GameObject CreateColliderContainer(string name, Transform parent)
    {
        GameObject generatedContainer = new GameObject(VRTK_SharedMethods.GenerateVRTKObjectName(true, name));
        generatedContainer.transform.SetParent(parent);
        generatedContainer.transform.localPosition = Vector3.zero;
        generatedContainer.transform.localRotation = Quaternion.identity;
        generatedContainer.transform.localScale = Vector3.one;

        generatedContainer.layer = LayerMask.NameToLayer("Player");
        VRTK_PlayerObject.SetPlayerObject(generatedContainer, VRTK_PlayerObject.ObjectTypes.Collider);

        return generatedContainer;
    }

}
