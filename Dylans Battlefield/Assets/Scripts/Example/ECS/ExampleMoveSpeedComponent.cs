﻿using System;

using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }

    public class ExampleMoveSpeedComponent : ComponentDataProxy<MoveSpeed> { }
}