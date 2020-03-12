
using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStarter : NetworkBehaviour
{
   [SerializeField]
   Behaviour[] componentsDisabled;

   private Camera Main_Camera;
   void Start()
   {
      if (!isLocalPlayer)
      {
         for (int i = 0; i <  componentsDisabled.Length; i++)
         {
            componentsDisabled[i].enabled = false;
         }
      }
      else
      {
         Main_Camera = Camera.main;
         if (Main_Camera != null)
         {
            Main_Camera.gameObject.SetActive(false);
         }
         
      }
   }

   private void OnDisable()
   {
      if (Main_Camera != null)
      {
         Main_Camera.gameObject.SetActive(true);
      }
   }
}
