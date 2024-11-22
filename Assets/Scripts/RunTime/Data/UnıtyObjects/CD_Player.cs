using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnıtyObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "BallTaker3D/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}