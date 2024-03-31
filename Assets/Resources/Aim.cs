using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.ID;

public class Aim : MonoBehaviour
{
   [SerializeField] private Transform spawnPosition;
   [SerializeField] private List<GameObject> allTarget;
   [SerializeField] private GameObject targetCyllinder;
   [SerializeField] private float range;
   private PlayerInput inputs;
   private PhotonView pv;
   private CharacterController controller;
   private GameObject targetObj;
   private bool canSearch = true;
   private int targetCount;

   private void Awake()
   {
    inputs = new PlayerInput();
    controller = GetComponent<CharacterController>();
    pv = GetComponentInParent<PhotonView>();
   }
}
