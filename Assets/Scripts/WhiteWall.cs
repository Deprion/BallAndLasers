using UnityEngine;

public class WhiteWall : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Vector3[] guardPos;

    private int curIndex = 1;

    private Vector3 checkPos;

    private int beforeMove;

    private Vector3 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Events.PassedBottom.AddListener(Passed);
        Events.Lost.AddListener(Restart);

        beforeMove = Random.Range(5, 15);
    }

    private void Restart()
    {
        curIndex = 1;

        checkPos = guardPos[0];

        transform.localPosition = guardPos[0];

        beforeMove = Random.Range(5, 15);
    }

    private void Passed()
    {
        beforeMove--;

        if (beforeMove <= 0)
        {
            Guard();

            dir = (checkPos - transform.localPosition).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.localPosition, checkPos) < 0.1f)
        {
            rb.velocity = Vector3.zero;

            return;
        }

        rb.velocity = dir * 2;
    }

    private void Guard()
    {
        checkPos = guardPos[curIndex];

        curIndex = curIndex + 1 >= guardPos.Length ? 0 : curIndex + 1;

        beforeMove = Random.Range(2, 6);
    }
}
