using System.Collections;
using UnityEngine;

public class BouncyWall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private Color clr = new Color();

    private float leftTime = 1;

    private WaitForSeconds waitFor = new WaitForSeconds(0.03f);

    private void Awake()
    {
        clr = sprite.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Cor());

            Events.BouncyWallHit.Invoke();
        }
    }

    private IEnumerator Cor()
    {
        while (leftTime > 0)
        { 
            leftTime -= 0.03f;

            clr.a = Mathf.Lerp(1, 0, 1 - leftTime);

            sprite.color = clr;

            yield return waitFor;
        }

        Destroy(gameObject);
    }
}
