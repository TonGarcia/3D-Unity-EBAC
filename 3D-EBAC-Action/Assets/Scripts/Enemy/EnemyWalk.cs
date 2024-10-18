using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Waypoints")]
        public GameObject[] waypoints;
        public float minDistance = 1f;
        public float speed = 1f;
        
        private int _index = 0;

        #region UnityEvents
        // Update is called once per frame
        void Update()
        {
            Vector3 currentPosition = transform.position;
            Vector3 currentWayPointPosition = waypoints[_index].transform.position;
            float distanceWaypointToEnemy = Vector3.Distance(transform.position, currentWayPointPosition);
            
            // check if the enemy GameObject entered the waypoint position zone
            if(distanceWaypointToEnemy < minDistance)
            {
                // increment to ref the next waypoint
                _index++;
                if (_index >= waypoints.Length) _index = 0;
            }
            
            // start GameObject walk
            Vector3 targetPosition = waypoints[_index].transform.position;
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, SpeedDeltaTime());
        }
        #endregion


        #region Helper Methods

        private float SpeedDeltaTime()
        {
            return Time.deltaTime * speed;
        }
        #endregion
    }
}
