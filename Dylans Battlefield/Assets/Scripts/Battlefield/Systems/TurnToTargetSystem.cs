using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace Battlefield
{
    public class TurnToTargetSystem : JobComponentSystem
    {
        struct TurnJob : IJobForEach<Rotation, Translation, Target>
        {
            public void Execute(ref Rotation rotation, [ReadOnly] ref Translation position, [ReadOnly] ref Target target )
            {
                Entity targetValue = target.Value;

                if (targetValue != Entity.Null)
                {
                    float3 targetPosition = new float3(); // TODO: Fix this

                    float3 direction = math.normalize(position.Value - targetPosition);

                    quaternion rotationToHave = quaternion.LookRotation(direction, math.up());

                    rotation.Value = rotationToHave;
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            TurnJob turnJob = new TurnJob
            {

            };

            return turnJob.Schedule(this, inputDeps);
        }
    }
}