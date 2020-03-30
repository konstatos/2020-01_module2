
using System;
using UnityEngine;

[Serializable]
public struct SerializableVector2
{
    public float x;
    public float y;

    public Vector2 asVector2 { get {
            return new Vector2(x, y);
        } set {
            x = value.x;
            y = value.y;
        } }
}

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public Vector3 asVector3 { get {
            return new Vector3(x, y, z);
        } set {
            x = value.x;
            y = value.y;
            z = value.z;
        } }
}

[Serializable]
public struct SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Quaternion asQuaternion { get {
            return new Quaternion(x, y, z, w);
        } set {
            x = value.x;
            y = value.y;
            z = value.z;
            w = value.w;
        } }
}

[Serializable]
public class RigidBodyState
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public SerializableVector2 bodyPosition;
    public float bodyRotation;
    public SerializableVector2 velocity;
    public float angularVelocity;
    public float inertia;

    public void Init(Rigidbody2D body)
    {
        position.asVector3 = body.transform.position;
        rotation.asQuaternion = body.transform.rotation;
        bodyPosition.asVector2 = body.position;
        bodyRotation = body.rotation;
        velocity.asVector2 = body.velocity;
        angularVelocity = body.angularVelocity;
        inertia = body.inertia;
    }

    public void Apply(Rigidbody2D body)
    {
        body.velocity = velocity.asVector2;
        body.angularVelocity = angularVelocity;
        body.inertia = inertia;
        body.position = bodyPosition.asVector2;
        body.rotation = bodyRotation;
        body.transform.position = position.asVector3;
        body.transform.rotation = rotation.asQuaternion;
    }
}
