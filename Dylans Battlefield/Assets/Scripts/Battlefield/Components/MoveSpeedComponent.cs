using System;

using Unity.Entities;

namespace Battlefield
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public int Value;
    }

    public class MoveSpeedComponent : ComponentDataProxy<MoveSpeed> { }
}