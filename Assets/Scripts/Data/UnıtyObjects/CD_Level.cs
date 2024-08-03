using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

namespace Data.UnıtyObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BallTaker3D/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}