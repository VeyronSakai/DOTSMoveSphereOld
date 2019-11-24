using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using UnityEngine;

public class MoveSphereSystem : JobComponentSystem
{
    [BurstCompile]
    private struct MoveSphereJob : IJobForEach<PhysicsVelocity, PhysicsMass, SphereTagComponentData, Force>
    {
        public float DeltaTime;
            
        public void Execute(ref PhysicsVelocity physicsVelocity, 
            [ReadOnly] ref PhysicsMass physicsMass, 
            [ReadOnly] ref SphereTagComponentData sphereComponentData,
            ref Force force)
        {
            physicsVelocity.Linear += physicsMass.InverseMass * force.value * DeltaTime;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new MoveSphereJob()
        {
            DeltaTime = Time.deltaTime,
        };
        return job.Schedule(this, inputDeps);
    }
}