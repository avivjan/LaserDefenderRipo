using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> WayPoints;
    [SerializeField] float speed = 2f;
    private int nextWayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if ((WayPoints.Count == 0) || (WayPoints.Count == 1) || (WayPoints == null))
        {
            Debug.Log("Enemy's Path has not enough way points");
            return;
        }//Null Check.


        transform.position = (Vector3)WayPoints[nextWayPointIndex]?.position;
        nextWayPointIndex++;
    }



    // Update is called once per frame
    void Update()
    {
        MoveFinishPathAndDestroy();
    }



    private void MoveFinishPathAndDestroy()
    {
        if ((WayPoints.Count == 0) || (WayPoints.Count == 1) || (WayPoints == null))//Null Check.
            {
                return;
            }


        if (nextWayPointIndex <= WayPoints.Count - 1)
        {
            MoveToNextWayPoint();
            if (HasArrivedToNextWayPoint())
            {
                nextWayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveToNextWayPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[nextWayPointIndex].position, speed * Time.deltaTime);
    }

    private bool HasArrivedToNextWayPoint()
    {
        return (transform.position == WayPoints[nextWayPointIndex].position);
    }
}
