using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace Battlefield
{
    public class TargetSystem : JobComponentSystem
    {
        struct TargetJob : IJobForEach<Target, Translation, Rotation>
        {
            public void Execute(ref Target target, [ReadOnly] ref Translation position, [ReadOnly] ref Rotation rotation)
            {
                // TODO: set target
                if (true)
                {
                    target.Value = new float3();
                }
                else
                {
                    target.Value.y = -2f;
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            TargetJob targetJob = new TargetJob
            {

            };

            return targetJob.Schedule(this, inputDeps);
        }
    }
}