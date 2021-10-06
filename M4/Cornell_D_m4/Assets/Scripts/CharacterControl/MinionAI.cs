using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    public GameObject targetVisualizer;
    public GameObject[] waypoints;
    public enum AIState {
        StationaryWaypoint,
        MovingWaypoint
        //TODO more? states…
    };
    public AIState aiState;

    private int currWaypoint = -1;
    private NavMeshAgent navMeshAgent;
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        aiState = AIState.StationaryWaypoint;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        setNextWaypoint();
    }

    void Update() {
        switch (aiState) {
            case AIState.StationaryWaypoint:
                if (waypoints[currWaypoint].GetComponent<Animator>() != null) {
                    aiState = AIState.MovingWaypoint;
                }
                break;
            case AIState.MovingWaypoint:
                if (waypoints[currWaypoint].GetComponent<Animator>() == null) {
                    aiState = AIState.StationaryWaypoint;
                } else {
                    UpdateTarget();
                }
                break;

            default:
                break;
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
            setNextWaypoint();
        }
        anim.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        targetVisualizer.transform.position = new Vector3(navMeshAgent.steeringTarget.x, targetVisualizer.transform.position.y, navMeshAgent.steeringTarget.z);
    }

    private void setNextWaypoint() {
        if (waypoints.Length == 0) {
            Debug.LogError("The waypoints list is empty");
            return;
        }
        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    private void UpdateTarget() {
        print("Seeking Moving Target");
        GameObject target = waypoints[currWaypoint];
        VelocityReporter vr = target.GetComponent<VelocityReporter>();
        if (vr) {
            float dist = (target.transform.position - this.transform.position).magnitude;
            float time = dist / navMeshAgent.speed;

            Vector3 futureTarget = target.transform.position + time * vr.velocity;

            NavMeshHit hit;
            bool didHit = NavMesh.Raycast(this.transform.position, futureTarget, out hit, NavMesh.AllAreas);
            if (didHit) {
                futureTarget = hit.position;
            }



            navMeshAgent.SetDestination(futureTarget);
        }
    }
}
