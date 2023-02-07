using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : StateMachineBehaviour
{

    Transform enemyTransform;
    Enemy enemy;
    public Vector2 moveSpot;
    public float range = 0.5f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
 //       moveSpot = new Vector2(Random.Range(enemyTransform.position.x - range, enemyTransform.position.x + range), Random.Range(enemyTransform.position.y - range, enemyTransform.position.y + range));
       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(enemyTransform.position, enemy.playerpos.position) <= enemy.range)
            animator.SetBool("isFollow", true);
        //else
        //{
        //    //if (Vector2.Distance(enemyTransform.position, moveSpot) < 0.1f)
        //    //{
        //    //    moveSpot = new Vector2(Random.Range(enemyTransform.position.x - range, enemyTransform.position.x + range), Random.Range(enemyTransform.position.y - range, enemyTransform.position.y + range));
        //    //}
        //    //else
        //    //{
        //    //    enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, moveSpot, enemy.speed * Time.deltaTime);
        //    //    Debug.Log("patrol" + enemy.speed);
        //    //}
        //}
    }


}
