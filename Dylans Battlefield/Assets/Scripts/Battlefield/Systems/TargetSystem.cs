using Unity.Burst;
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
        Unity.Mathematics.Random _rng = new Unity.Mathematics.Random(1u);

		[BurstCompile]
		struct TargetJob : IJobForEachWithEntity<Target, Translation, Rotation>
        {
            static readonly float3 MIN = new float3(-10f, 0f, -10f);
            static readonly float3 MAX = new float3(10f, 0f, 10f);

            public uint baseSeed;
            public float frameCount;

            public void Execute(Entity entity, int index, ref Target target, [ReadOnly] ref Translation position, [ReadOnly] ref Rotation rotation)
            {
                // TODO: set target
                if (true)
                {
                    if (frameCount % 100f == 0f)
                    {
                        // TODO: make the index change the seed more dramaticly
                        Unity.Mathematics.Random rng = new Unity.Mathematics.Random(baseSeed + (uint)index);

                        float3 _randomTarget = rng.NextFloat3(MIN, MAX);
                        _randomTarget.y = 0f;

                        target.Value = _randomTarget;
                    }
                }
                else
                {
                    //target.Value.y = -2f;
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            if (Time.frameCount % 100 == 0)
            {
                // I need to call this here to mutate the rng state, because the threads dont?
                _rng.NextFloat3();
            }

            TargetJob targetJob = new TargetJob
            {
                baseSeed = _rng.state,
                frameCount = Time.frameCount
            };

            return targetJob.Schedule(this, inputDeps);
        }
    }
}