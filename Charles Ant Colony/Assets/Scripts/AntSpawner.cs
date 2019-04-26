using Unity.Entities;

public struct AntSpawner : IComponentData
{
	public int Count;
	public Entity Prefab;
}
