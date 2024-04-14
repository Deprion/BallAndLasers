using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Color clr;

    private bool canMove = false;
    [HideInInspector] public bool isMoving;

    private Vector2 dir = new Vector2(1, 0);

    private WaitForSeconds waitFor = new WaitForSeconds(0.02f);
    private float leftTime = 1;

    public void ChangeSpeed(float val)
    {
        speed = val;
    }

    public void MoveTo(Vector2 d)
    {
        dir = d * speed;
        canMove = true;
        isMoving = true;
    }

    public void ChangeDir()
    {
        dir *= -1;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = dir;

            if (transform.localPosition.x < -10 || transform.localPosition.x > 10)
            {
                isMoving = false;
                canMove = false;
            }
        }
    }

    public void Prepare()
    {
        leftTime = 1;

        StartCoroutine(Cor());
    }

    private IEnumerator Cor()
    {
        while (leftTime > 0)
        {
            leftTime -= 0.04f;
            clr.a = Mathf.Lerp(0, 1, 1 - leftTime);

            foreach (var spr in sprites)
            {
                spr.color = clr;
            }

            yield return waitFor;
        }
    }
}
