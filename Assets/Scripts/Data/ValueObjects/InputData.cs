using System;
using System.Numerics;
using Unity.Mathematics;

namespace Data.ValueObjects
{
    //
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public float2 ClampValues;
        public float ClampSpeed;
    }
}