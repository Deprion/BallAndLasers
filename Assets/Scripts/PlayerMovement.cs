using UnityEngine;
using UnityEngine.Rendering;
using YG;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float g;
    [SerializeField] private AudioSource source;

    private Vector2 dir;

    private Vector2 topPos = new Vector2(0, 15.5f);
    private Vector2 sidePos = new Vector2();

    public void SetMovement(float x)
    {
        dir.x = x * speed;
    }

    private void Update()
    {
        if (YandexGame.EnvironmentData.deviceType == "desktop")
            dir.x = Input.GetAxisRaw("Horizontal") * speed;

        if (dir.y > -12)
        {
            dir.y += g * Time.deltaTime;
        }

        CheckBottom();
        CheckSide();
    }

    private void CheckBottom()
    {
        if (transform.localPosition.y < -15.5)
        {
            topPos.x = transform.localPosition.x;

            transform.localPosition = topPos;

            Events.PassedBottom.Invoke();
        }
    }

    private void CheckSide()
    {
        if (transform.localPosition.x < -8.75)
        {
            sidePos.x = 8.75f;
            sidePos.y = transform.localPosition.y;

            transform.localPosition = sidePos;
        }
        else if (transform.localPosition.x > 8.75)
        {
            sidePos.x = -8.75f;
            sidePos.y = transform.localPosition.y;

            transform.localPosition = sidePos;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bouncy"))
        {
            dir.y = 10;
            source.Play();
        }
    }
}
