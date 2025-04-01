using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move2 : MonoBehaviour
{
    [SerializeField]
    private Move _waypointPath;
    [SerializeField]

    private float _speed;
    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    void Start()
    {
        TargetNextWayPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        if(elapsedPercentage >= 1)
        {
            TargetNextWayPoint();
        }
    }

    private void TargetNextWayPoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint( _targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;
        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint/ _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other) 
    {
        other.transform.SetParent(null);
    }
    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
        Debug.Log($"Speed changed to {newSpeed} at {System.DateTime.Now}");
        //StartCoroutine(SleepAfterLog(5f));
    }

    private IEnumerator SleepAfterLog(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);  // Wait for the given time (in seconds)

        // Code to execute after the delay (if any)
        Debug.Log("Delay finished after " + delayTime + " seconds.");
    }

}
