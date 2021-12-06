using UnityEngine;




[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{

    public Transform target = null;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    [SerializeField]
    private float MovementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            if (navMeshAgent.hasPath)
            {
                navMeshAgent.speed = MovementSpeed;
                FaceDestination();
            }
        }
    }
    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
    }
}
