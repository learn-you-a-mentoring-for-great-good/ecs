using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;


namespace JobMultiThreaded
{
	public class AntColonySystem : JobComponentSystem
	{
		Random r;

		public AntColonySystem() : base()
		{
			r = new Random( 7778 );
		}

		[Unity.Burst.BurstCompile]
		struct AntJob : IJobForEach<Translation, Ant>
        {
            public float deltaTime;
    
            // The [ReadOnly] attribute tells the job scheduler that this job will not write to rotSpeed
            public void Execute(ref Translation t, [ReadOnly] ref Ant a)
            {
                t.Value.x += a.direction.x * a.speed * deltaTime;
				t.Value.z += a.direction.z * a.speed * deltaTime;
            }
		}

		protected override JobHandle OnUpdate( JobHandle inputDependencies )
		{
			AntJob job = new AntJob() { deltaTime = Time.deltaTime };
			return job.Schedule( this, inputDependencies );
		}
	}
}