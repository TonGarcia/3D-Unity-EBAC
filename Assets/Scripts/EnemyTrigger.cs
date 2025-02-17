using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header("Target Object")]
    public GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
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
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
} 