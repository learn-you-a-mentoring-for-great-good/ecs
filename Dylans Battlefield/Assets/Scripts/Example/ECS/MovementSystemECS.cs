using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace ECS
{
    //public class MovementSystemECS : JobComponentSystem
    //{
    //    struct MovementJob : IJobForEach<Translation, Rotation, MoveSpeed>
    //    {
    //        public float topBound;
    //        public float bottomBound;
    //        public float deltaTime;

    //        public void Execute(ref Translation position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MoveSpeed speed)
    //        {
    //            float3 value = position.Value;

    //            value += speed.Value * deltaTime * math.forward(rotation.Value);

    //            if (value.z < bottomBound)
    //            {
    //                value.z = topBound;
    //            }

    //            position.Value = value;
    //        }
    //    }

    //    protected override JobHandle OnUpdate(JobHandle inputDeps)
    //    {
    //        MovementJob moveJob = new MovementJob
    //        {
    //            topBound = GameManagerECS.GM.topBound,
    //            bottomBound = GameManagerECS.GM.bottomBound,
    //            deltaTime = Time.deltaTime
    //        };

    //        return moveJob.Schedule(this, inputDeps);
    //    }
    //}
}