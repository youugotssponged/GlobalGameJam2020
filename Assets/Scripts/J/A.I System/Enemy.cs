using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public enum State{
        PATROL, 
        CHASE,
    }

    public State state;
    private bool alive;

    public GameObject[] waypoints;

    private int waypointIndex = 0;
    public float patrolSpeed = 0.5f;

    public float chaseSpeed = 1f;
    public GameObject target;

    private bool isActive;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;

        state = State.PATROL;
        alive = true;
        isActive = true;
        StartCoroutine("FSM");
    }

    IEnumerator FSM ()
		{
			while (alive && isActive) {
				switch (state) {
				case State.PATROL:
					Patrol ();
					break;
				case State.CHASE:
					Chase ();
					break;
				}
				yield return null;
			}
		}

		void Patrol ()
		{
            if(CheckEnemyHealth() == 0 && isActive){ 
                alive = false;
                isActive = false;
                Destroy(this.gameObject);
                return;
            }

			agent.speed = patrolSpeed;
			//if too far away, move to waypoint
			if (Vector3.Distance (transform.position, waypoints [waypointIndex].transform.position) >= 1)
				agent.SetDestination (waypoints [waypointIndex].transform.position);
			else
				waypointIndex += 1;
			if (waypointIndex >= waypoints.Length)
				waypointIndex = 0;
		}

		void Chase ()
		{
            if(CheckEnemyHealth() == 0 && isActive) {
                alive = false;
                isActive = false;
                Destroy(this.gameObject);
                return;
            }

			//set speed to chase target
			agent.speed = chaseSpeed;
			agent.SetDestination (target.transform.position);
		}

		void OnTriggerEnter (Collider coll)
		{
			if (coll.tag == "Player") {
				state = State.CHASE;
				target = coll.gameObject;
			}
		}

        private float CheckEnemyHealth(){
            var enemyHealth = GetComponent<EnemyHealth>();
            Debug.Log(enemyHealth.health);
            return enemyHealth.health;
        }
}
