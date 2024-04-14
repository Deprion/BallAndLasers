using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;

    private bool shouldSpawn;

    private Vector2 pos;

    private GameObject wall;

    private void Awake()
    {
        Events.BouncyWallHit.AddListener(BouncyWallHit);
        Events.PassedBottom.AddListener(Passed);
        Events.Lost.AddListener(Restart);

        Restart();
    }

    private void Restart()
    {
        if (wall != null)
            Destroy(wall);

        shouldSpawn = true;
    }

    private void BouncyWallHit()
    {
        shouldSpawn = true;
    }

    private void Passed()
    {
        if (shouldSpawn)
        {
            SpawnWall();
            shouldSpawn = false;
        }
    }

    private void SpawnWall()
    {
        wall = Instantiate(wallPrefab);

        pos.x = Random.Range(-7f, 7);
        pos.y = GenerateY(pos.x);

        wall.transform.localPosition = pos;
    }

    private float GenerateY(float x)
    {
        float y = Random.Range(-12f, 8);

        if (y <= -5 && y > -11)
        {
            if (x > -2 && x < 2)
            {
                return GenerateY(x);
            }
            else return y;
        }
        else return y;
    }
}
