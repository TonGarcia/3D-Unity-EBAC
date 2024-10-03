using UnityEngine;

namespace CombatSystem.Gun
{
    public class ProjectileBase : MonoBehaviour
    {
        public Vector3 moveModifier;
        public int damageAmount = 1;
        public float speed = 50f;
    
        // bad approach to instantiate and destroy, the best it get a list and it is visible or not
        public float toTimeBeDestroyed = 1f; 

        private void Awake()
        {
            Destroy(gameObject, toTimeBeDestroyed);
        }

        // Start is called before the first frame update
        void Start()
        {
    
        }

        // Update is called once per frame
        void Update()
        {
            // bad performance
            // transform.Translate(moveModifier * direction * Time.deltaTime);
            
            // bad performance 2
            // transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
            // best performance
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }

        private void OnCollisionEnter(Collision otherCollision)
        {
        
        }
    }
}    
