﻿using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class AntSpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
	public GameObject Prefab;
	public int Count;

	// Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
	public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
	{
		gameObjects.Add(Prefab);
	}

	// Lets you convert the editor data representation to the entity optimal runtime representation

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		var spawnerData = new AntSpawner
		{
			// The referenced prefab will be converted due to DeclareReferencedPrefabs.
			// So here we simply map the game object to an entity reference to that prefab.
			Prefab = conversionSystem.GetPrimaryEntity(Prefab),
			Count = Count
		};
		dstManager.AddComponentData(entity, spawnerData);
	}
}