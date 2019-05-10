using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace JobMultiThreaded
{

// this entity will exist only temporarily until the spawning is complete
public struct Spawner : IComponentData
{
	public uint seed;
	public int count;
	public Entity prefab;
}

[RequiresEntityConversion]
public class SpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
	[SerializeField]
	uint _seed = 5555;

	[SerializeField]
	GameObject _prefab = null;
	
	[SerializeField]
	int _count = 1;

	// Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
	public void DeclareReferencedPrefabs( List<GameObject> gameObjects )
	{
		gameObjects.Add( _prefab );
	}

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		Spawner spawner = new Spawner
		{
			// The referenced prefab will be converted due to DeclareReferencedPrefabs.
			// So here we simply map the game object to an entity reference to that prefab.
			  seed = _seed
			, count = _count
			, prefab = conversionSystem.GetPrimaryEntity( _prefab )
		};
		dstManager.AddComponentData(entity, spawner);
	}
}

}