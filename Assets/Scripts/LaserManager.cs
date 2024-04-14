using System.Collections;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private Laser laser;

    private int startCount = -1;

    private Vector2 leftPos = new Vector2(-9, 0), rightPos = new Vector2(9, 0);

    private WaitForSeconds waitFor = new WaitForSeconds(0.5f);

    private void Awake()
    {
        Events.PassedBottom.AddListener(Passed);
        Events.Lost.AddListener(Restart);
    }

    private void Restart()
    {
        startCount = -1;

        laser.ChangeSpeed(6);

        transform.localPosition = new Vector3(-20, 0, 0);
    }

    private void Passed()
    {
        startCount++;

        if (startCount < 1 || laser.isMoving) return;

        StartCoroutine(MoveCor());
    }

    private IEnumerator MoveCor()
    {
        laser.Prepare();

        laser.ChangeSpeed(6 + startCount / 5f);

        if (Random.Range(0, 2) == 1)
        {
            transform.localPosition = leftPos;

            yield return waitFor;

            laser.MoveTo(Vector2.right);
        }
        else
        {
            transform.localPosition = rightPos;

            yield return waitFor;

            laser.MoveTo(Vector2.left);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Events.Lost.Invoke();
        }
    }
}
