using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;

public class MoveSphereSystem : JobComponentSystem
{
    // Entities 0.1.1
    /*[BurstCompile]
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
            DeltaTime = Time.DeltaTime,
        };
        return job.Schedule(this, inputDeps);
    }*/


    // Entities 0.3.0
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;
        
        var jobHandle = Entities
            .WithAll<SphereTagComponentData>()
            .WithoutBurst()
            .ForEach((ref PhysicsVelocity physicsVelocity, in Force force, in PhysicsMass physicsMass) =>
            {
                physicsVelocity.Linear += physicsMass.InverseMass * force.value * deltaTime;
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}