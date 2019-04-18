using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace Battlefield
{
    public class DeathSystem : JobComponentSystem
    {
        struct DeathJob : IJobForEachWithEntity<Health>
        {
            public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
            {
                int healthValue = health.Value;

                if (healthValue <= 0)
                {
                    World.Active.EntityManager.DestroyEntity(entity);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            DeathJob deathJob = new DeathJob
            {

            };

            return deathJob.Schedule(this, inputDeps);
        }
    }
}