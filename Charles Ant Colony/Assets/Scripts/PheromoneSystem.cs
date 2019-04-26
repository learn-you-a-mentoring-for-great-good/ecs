using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace AntColony
{
	public class PheromoneSystem : JobComponentSystem
	{
	    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

		[Unity.Burst.BurstCompile]
		struct PheromoneJob : IJobForEachWithEntity<Pheromone>
        {
            public float deltaTime;

            public EntityCommandBuffer CommandBuffer;
    
            // The [ReadOnly] attribute tells the job scheduler that this job will not write to rotSpeed
            public void Execute( Entity entity, int index, [ReadOnly] ref Pheromone p)
            {
                p.duration -= deltaTime;

                if(p.duration <= 0.0f)
                {
                    CommandBuffer.DestroyEntity(entity);
                }
            }
		}

		protected override JobHandle OnUpdate( JobHandle inputDependencies )
		{
			var job = new PheromoneJob() { deltaTime = Time.deltaTime, CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer() }.ScheduleSingle(this, inputDependencies);
            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
			return job;
		}
	}
}