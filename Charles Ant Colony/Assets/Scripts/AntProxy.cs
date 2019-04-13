using UnityEngine;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace AntColony
{
	[System.Serializable]
	public struct Ant : IComponentData
	{
		public float speed;
		public Vector3 direction;
	}

	[RequiresEntityConversion]
	public class AntProxy : MonoBehaviour, IConvertGameObjectToEntity
	{
		static Random s_random;
		static bool s_randomIsSeeded;

		public void Convert( Entity entity, EntityManager manager, GameObjectConversionSystem converter )
		{
			InitRandom();
			float randomSpeed = s_random.NextFloat( 1f, 5f );

			float randomAngle = s_random.NextFloat( 0f, (float)(2.0 * System.Math.PI) );
			Vector3 randomDirection = new Vector3( Mathf.Cos( randomAngle ), 0f, Mathf.Sin( randomAngle ) );

			Ant a = new Ant { speed = randomSpeed, direction = randomDirection };
			manager.AddComponentData( entity, a );
		}

		// PRIVATE

		static void InitRandom()
		{
			if( !s_randomIsSeeded )
			{
				s_randomIsSeeded = true;
				s_random = new Random( 5555 );
			}
		}
	}
}