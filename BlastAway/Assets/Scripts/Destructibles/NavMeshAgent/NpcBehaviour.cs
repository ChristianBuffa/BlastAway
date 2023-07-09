using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform centerPoint;
    private int hitReset;
    
    public float range;
    public float runSpeed;

    public bool isHit = false;
    public bool isDead = false;
    
    void Start()
    {
        hitReset = 0;
        agent = GetComponent<NavMeshAgent>();
        centerPoint = gameObject.transform;
        agent.speed = runSpeed;
    }
    
    void Update()
    {
        CheckIfHit();
        CheckDistance();
    }

    private void CheckIfHit()
    {
        if (hitReset > 30)
        {
            isHit = false;
            hitReset = 0;
        }

        if (isDead)
        {
            agent.velocity = Vector3.zero;
        }
    }
    
    private void CheckDistance()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && isHit)
        {
            hitReset++;
            SetDestination();
        }
    }

    private void SetDestination()
    {
        Vector3 point;
        if (RandomPoint(centerPoint.position, range, out point)) 
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
