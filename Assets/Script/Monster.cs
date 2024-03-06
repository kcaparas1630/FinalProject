using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    private NavMeshAgent enemy;
    public GameObject PlayerTarget;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(PlayerTarget.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start attacking animation
            anim.SetBool("IsAttacking", true);

            // Add your attack logic here

            // For example, you might want to decrease player health
            // or trigger a game over state
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop attacking animation
            anim.SetBool("IsAttacking", false);
        }
    }
}
