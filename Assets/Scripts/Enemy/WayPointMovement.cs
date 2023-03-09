using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPath;
    [SerializeField] private float _speed;

    private int currentPoint = 0;

    private void Update()
    {
        Transform target = _patrolPath[currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            currentPoint++;

            if (currentPoint >= _patrolPath.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
