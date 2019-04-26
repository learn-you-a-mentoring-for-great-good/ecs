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
                float3 targetPosition = target.Value;

                // TODO: find better way to determine if target is set
                if (targetPosition.y > -1f)
                {
                    float3 direction = math.normalize(targetPosition - position.Value);

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