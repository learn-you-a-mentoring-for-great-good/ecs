using Unity.Entities;

public struct AntSpawner : IComponentData
{
	public int CountX;
	public int CountY;
	public Entity Prefab;
}
