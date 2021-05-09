using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Transform recoilPosition;
    public Transform rotationPoint;

    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    public Vector3 RecoilRotation = new Vector3(10, 5, 7);
    public Vector3 RecoilKickback = new Vector3(0.015f,0f,-0.2f);

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 Rot;

    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalRecoilSpeed*Time.deltaTime);
        rotationPoint.localRotation = Quaternion.Euler(Rot);
    }

    public void RecoilHandling()
    {
        rotationalRecoil += new Vector3(-RecoilRotation.x,
            Random.Range(-RecoilRotation.y, RecoilRotation.y),
            Random.Range(-RecoilRotation.z, RecoilRotation.z));

        positionalRecoil += new Vector3(Random.Range(-RecoilKickback.x,RecoilKickback.x),
            Random.Range(-RecoilKickback.y, RecoilKickback.y),
            RecoilKickback.z);
    }
}
