using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

using UnityEngine;

namespace AntColony
{
public class PheromoneSpawnerSystem : JobComponentSystem
{
	// BeginInitializationEntityCommandBufferSystem is used to create a command buffer which will then be played back
	// when that barrier system executes.
	// Though the instantiation command is recorded in the SpawnJob, it's not actually processed (or "played back")
	// until the corresponding EntityCommandBufferSystem is updated. To ensure that the transform system has a chance
	// to run on the newly-spawned entities before they're rendered for the first time, the HelloSpawnerSystem
	// will use the BeginSimulationEntityCommandBufferSystem to play back its commands. This introduces a one-frame lag
	// between recording the commands and instantiating the entities, but in practice this is usually not noticeable.
	BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

	protected override void OnCreate()
	{
		// Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
		m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
	}

	struct SpawnJob : IJobForEachWithEntity<PheromoneSpawner, Translation>
	{
        public float DeltaTime;
		public EntityCommandBuffer CommandBuffer;

		public void Execute(Entity entity, int index, ref PheromoneSpawner spawner,
			[ReadOnly] ref Translation translation)
		{
            spawner.Timer -= DeltaTime;

            if(spawner.Timer <= 0.0f)
            {
                var instance = CommandBuffer.Instantiate(spawner.Prefab);
                CommandBuffer.SetComponent(instance, new Translation { Value = translation.Value });
                CommandBuffer.SetComponent(instance, new Pheromone { duration = 5.0f });

                spawner.Timer = 0.25f;
            }
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		//Instead of performing structural changes directly, a Job can add a command to an EntityCommandBuffer to perform such changes on the main thread after the Job has finished.
		//Command buffers allow you to perform any, potentially costly, calculations on a worker thread, while queuing up the actual insertions and deletions for later.

		// Schedule the job that will add Instantiate commands to the EntityCommandBuffer.
		var job = new SpawnJob
		{
			CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            DeltaTime = Time.deltaTime
		}.ScheduleSingle(this, inputDeps);


		// SpawnJob runs in parallel with no sync point until the barrier system executes.
		// When the barrier system executes we want to complete the SpawnJob and then play back the commands (Creating the entities and placing them).
		// We need to tell the barrier system which job it needs to complete before it can play back the commands.
		m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

		return job;
	}
}
}
