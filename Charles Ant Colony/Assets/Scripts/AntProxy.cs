using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace AntColony
{
	[System.Serializable]
	public struct Ant : IComponentData
	{
		public float speed;
	}
	public class AntProxy : ComponentDataProxy<Ant>
	{
	}
}
