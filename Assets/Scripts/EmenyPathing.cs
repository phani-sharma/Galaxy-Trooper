using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyPathing : MonoBehaviour
{
     WaveConfig Waveconfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    void Start()
    {
        waypoints = Waveconfig.GetWayPoints();
        transform.position = waypoints[waypointIndex] . transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }
    public void  SetWaveConfig(WaveConfig waveConfig)
    {
        this.Waveconfig = waveConfig;
    }

    public void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var TargetPosition = waypoints[waypointIndex].transform.position;
            var MovementThisFrame = Waveconfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, MovementThisFrame);

            if (transform.position == TargetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
}
