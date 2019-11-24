using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class ChangeForceSystem : JobComponentSystem
{
    [BurstCompile]
    private struct ChangeForceJob : IJobForEach<PhysicsVelocity, PhysicsMass, SphereTagComponentData, Force>
    {
        public float3 Force;
                
        public void Execute(ref PhysicsVelocity physicsVelocity, 
            [ReadOnly] ref PhysicsMass physicsMass, 
            [ReadOnly] ref SphereTagComponentData sphereComponentData, 
            ref Force force)
        {
            force.value = Force;
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new ChangeForceJob();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            job.Force = math.float3(-10, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            job.Force = math.float3(10, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            job.Force = math.float3(0, 0, 10);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            job.Force = math.float3(0, 0, -10);
        }
            
        return job.Schedule(this, inputDeps);
    }
}