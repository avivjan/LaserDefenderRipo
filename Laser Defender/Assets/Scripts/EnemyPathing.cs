using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    int nextWayPointIndex = 0;  
    List<Transform> WayPoints;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        WayPoints = waveConfig.GetWayPoints();
        speed = waveConfig.GetMoveSpeed();


        if ((WayPoints.Count == 0) || (WayPoints.Count == 1))
        {
            Debug.Log("Enemy's Path has not enough way points");
            return;
        }//Empty Check.
        transform.position = (Vector3)WayPoints[nextWayPointIndex]?.position;
        nextWayPointIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFinishPathAndDestroy();
    }


    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveFinishPathAndDestroy()
    {
        if ((WayPoints.Count == 0) || (WayPoints.Count == 1))//Empty Check.
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
