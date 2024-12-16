using System.Linq;
using RunTime.Data.ValueObjects;
using RunTime.Managers;
using UnityEngine;

namespace RunTime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        private PlayerManager _manager;
        PlayerForceData _forceData;

        public ForceBallsToPoolCommand(PlayerManager manager, PlayerForceData forceData)
        {
            _manager = manager;
            _forceData = forceData;
        }

        internal void Execute()
        {
            Debug.Log("execute girdi");
            var transform1 = _manager.transform;
            var position1 = transform1.position;
            var forcepos = new Vector3(position1.x, position1.y + 1f, position1.z + +1f);

            var colliders = Physics.OverlapSphere(forcepos, 1.35f);
            var collectableColliderList = colliders.Where(col => col.CompareTag("Collectable")).ToList();
            foreach (var col in collectableColliderList)
            {
                if (col.GetComponent<Rigidbody>() == null) continue;
                var rigidbody = col.GetComponent<Rigidbody>();
                rigidbody.AddForce(0, _forceData.ForceParameters.y, _forceData.ForceParameters.z, ForceMode.Impulse);
            }

            collectableColliderList.Clear();
        }
    }
}