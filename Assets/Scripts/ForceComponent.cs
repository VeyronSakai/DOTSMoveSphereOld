using Unity.Entities;
using Unity.Mathematics;
using System;

[Serializable]
public struct Force : IComponentData
{
    public float3 value;
}