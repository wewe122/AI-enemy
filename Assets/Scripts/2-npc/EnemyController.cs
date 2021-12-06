using System;
using UnityEngine;
using UnityEngine.AI;

/**
 * This component patrols between given points, chases a given target object when it sees it, and rotates from time to time.
 */
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
     [SerializeField] private GameObject Engine = null;
    [SerializeField] private GameObject Player = null;
    [SerializeField] private Transform targetFolder = null;

    enum ModeSwitching {Chaser, Brave, Coward, GoToEngine};
    [Tooltip("choose enemy type")]
    [SerializeField] private ModeSwitching EnemyMode;


    private Target[] allTargets = null;
    private NavMeshAgent navMeshAgent;
    private EnemyMovement enemyMovement;


private void Brave() {
                    var targetBrave = distanceFromPlayer(2);
                    navMeshAgent.SetDestination(targetBrave.transform.position);
                    enemyMovement.target = targetBrave.transform;
    }
    
    private void Chase() {
                    navMeshAgent.SetDestination(Player.transform.position);
                    enemyMovement.target = Player.transform;
    }

      private void Coward() {
                     var targetCoward = distanceFromPlayer(3);
                    navMeshAgent.SetDestination(targetCoward.transform.position);
                    enemyMovement.target = targetCoward.transform;
    }
      private void GoToEngine() {
                     navMeshAgent.SetDestination(Engine.transform.position);
                    enemyMovement.target = Engine.transform;
    }
        void Start()
    {
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        allTargets = targetFolder.GetComponentsInChildren<Target>(false);
    }

    void Update()
    {        
            if(EnemyMode==ModeSwitching.Chaser)
            Chase();  
            else if( EnemyMode==ModeSwitching.Brave)
            Brave();
            else if( EnemyMode==ModeSwitching.Coward)
            Coward();
            else if( EnemyMode==ModeSwitching.GoToEngine)
            GoToEngine();
   
        
    }

    // this method find a target from TargetFolder by comparing distance
    private Target distanceFromPlayer(int type)
    {
        var target = allTargets[0];
        foreach(var t in allTargets) {
            if (target.name != t.name) {
                float distance_t1_to_player = Vector3.Distance(target.transform.position, Player.transform.position);
                float distance_t2_to_player = Vector3.Distance(t.transform.position, Player.transform.position);

                //brave
                if (type == 2)
                    target = distance_t1_to_player < distance_t2_to_player ? target : t;
                //coward
                else
                    target = distance_t1_to_player > distance_t2_to_player ? target : t;
            }
        }
        return target;
    }
}
