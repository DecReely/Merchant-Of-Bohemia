using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MerchantOfBohemia.Characters;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private GameObject[] characterPrefabs;
        [SerializeField] private GameObject root;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Player playerScriptableObj;
        
        void Start()
        {
            GameObject player = Instantiate(playerScriptableObj.gameObjectPrefab,
                spawnPoint.position, Quaternion.identity);
            root.transform.SetParent(player.transform);
            player.SetActive(true);
            player.name = "Player";
            
            Object.Destroy(GameObject.Find("All Merchant Prefabs"));
        }
    }
}
