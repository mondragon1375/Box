using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    Transform target;
    UnityEngine.AI.NavMeshAgent pathFinder;
    public float refreshRate = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

        // Sets the player as the target to get to and will be a problem when introducing more than one player/good guy
        // should get moved to Update() later to constantly find a target

        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(updatePath());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator updatePath()
    {
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathFinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
