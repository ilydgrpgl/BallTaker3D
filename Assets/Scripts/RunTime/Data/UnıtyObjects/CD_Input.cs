using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnıtyObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "BallTaker3D/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;
    }
}