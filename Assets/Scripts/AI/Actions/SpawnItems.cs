using System;
using Apex.AI;
using Photon.Pun;
using UnityEngine;

namespace AI.Actions
{
    public class SpawnItems : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (SpawnersContext) context;
            foreach (var VAR in c.itemPlaces)
            {
                if (PhotonNetwork.IsConnected)
                    PhotonNetwork.Instantiate(selectItemToSpawn(), VAR, Quaternion.identity);
                else
                    GameObject.Instantiate((GameObject) Resources.Load(selectItemToSpawn()), VAR,
                        Quaternion.identity); //todo сделать, чтобы предметы 'лежали' на меше с помощью нормалей
            }
        }

        private String selectItemToSpawn()
        {
            return "Item";
        }
    }
}