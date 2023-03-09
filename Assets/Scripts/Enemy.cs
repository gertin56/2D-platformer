using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPath;
    [SerializeField] private float _speed;

    private int currentPoint = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Die();
        }
    }

    private void Update()
    {
        Transform target = _patrolPath[currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(transform.position == target.position)
        {
            currentPoint++;

            if(currentPoint >= _patrolPath.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
