using UnityEngine;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace JobMultiThreaded
{

[System.Serializable]
public struct Ant : IComponentData
{
	public float speed;
	public Vector3 direction;
}

}