using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DectectiveComponent : MonoBehaviour
{
    [SerializeField]
    private float radius;
    public float maxDistance;
    [SerializeField]
    LayerMask targetLayerMask;
    [SerializeField]
    public bool isRangeTarget;
    [SerializeField]
    public bool isRayDecTarget;
    Transform pos;
    public Transform Pos
    {
        get;
        private set;
    }
    Animator animator;

    bool CheckTarget(int layerMask)
    {
        return (targetLayerMask & 1 << layerMask) != 0;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, targetLayerMask);
        isRangeTarget = cols.Length > 0;
        animator.SetBool("TargetRange", false);
        
        if(isRangeTarget) //범위 안에 타겟이 있는 경우
        {
            RaycastHit hit;
            Vector3 direction = (cols[0].transform.position - transform.position).normalized; 
            animator.SetBool("TargetRange", true); //애니메이션 실행 
            Debug.DrawLine(transform.position, transform.position + (direction * maxDistance), Color.black);
            Pos = cols[0].transform;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                isRayDecTarget = CheckTarget(hit.collider.gameObject.layer);
                if(isRayDecTarget && (cols[0].transform.position - transform.position).magnitude <= 1.7f)
                {
                    animator.SetTrigger("Dectect");
                    Debug.DrawLine(transform.position, transform.position + (direction * maxDistance), Color.red);
                }
                else if(isRayDecTarget)
                {
                    Debug.DrawLine(transform.position, transform.position + (direction * maxDistance), Color.red);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
