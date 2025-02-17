using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        [Header("Target Object")]
        public GameObject targetEnemies;

        private void OnTriggerEnter(Collider other)
        {
            if (targetEnemies != null)
            {
                targetEnemies.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            // Ensure we have a BoxCollider component and it's set as trigger
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider>();
            }
            boxCollider.isTrigger = true;

            // If we have a target object, make sure it starts inactive
            if (targetEnemies != null)
            {
                targetEnemies.SetActive(false);
            }
        }
    }
}

