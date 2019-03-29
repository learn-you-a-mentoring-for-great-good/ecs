using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

namespace AntColony
{
	public class AntColonySystem : JobComponentSystem
	{
		struct AntColonyJob : IJobProcessComponentData<Ant, Position>
		{
			public float DeltaTime;
			public Unity.Mathematics.Random random;

			public void Execute(ref Ant test, ref Position pos)
			{
				pos.Value.x += random.NextInt(-5, 5) * test.speed / 2 * DeltaTime;
				pos.Value.z += random.NextInt(-5, 5) * test.speed / 2 * DeltaTime;
			}
		}

		protected override JobHandle OnUpdate(JobHandle inputDependencies)
		{
			var job = new AntColonyJob()
			{
				DeltaTime = Time.deltaTime,
				random = new Unity.Mathematics.Random(1)
			};

			return job.Schedule(this, inputDependencies);
		}
	}
}