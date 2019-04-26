using UnityEngine;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace AntColony
{
	[System.Serializable]
	public struct Pheromone : IComponentData
	{
		public float duration;
	}

	[RequiresEntityConversion]
	public class PheromoneProxy : MonoBehaviour, IConvertGameObjectToEntity
	{
		public void Convert( Entity entity, EntityManager manager, GameObjectConversionSystem converter )
		{
            Pheromone p = new Pheromone { duration = 10f };
			manager.AddComponentData( entity, p );
		}
	}
}