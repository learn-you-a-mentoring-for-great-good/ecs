using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace Battlefield
{
    public class MovementSystem : JobComponentSystem
    {
		[BurstCompile]
        struct MovementJob : IJobForEach<Translation, Rotation, MoveSpeed>
        {
            public float deltaTime;

            public void Execute(ref Translation position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MoveSpeed speed)
            {
                float3 positionValue = position.Value;

                positionValue += speed.Value * deltaTime * math.forward(rotation.Value);

                position.Value = positionValue;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            MovementJob moveJob = new MovementJob
            {
                deltaTime = Time.deltaTime
            };

            return moveJob.Schedule(this, inputDeps);
        }
    }
}