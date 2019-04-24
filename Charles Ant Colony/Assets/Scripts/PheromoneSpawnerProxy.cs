using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PheromoneSpawner : IComponentData
{
    public float Timer;
	public Entity Prefab;
}

[RequiresEntityConversion]
public class PheromoneSpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
	public GameObject Prefab;

	// Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
	public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
	{
		gameObjects.Add(Prefab);
	}

	// Lets you convert the editor data representation to the entity optimal runtime representation

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		var spawnerData = new PheromoneSpawner
		{
			// The referenced prefab will be converted due to DeclareReferencedPrefabs.
			// So here we simply map the game object to an entity reference to that prefab.
			Prefab = conversionSystem.GetPrimaryEntity(Prefab),
            Timer = 0.25f
		};
		dstManager.AddComponentData(entity, spawnerData);
	}
}