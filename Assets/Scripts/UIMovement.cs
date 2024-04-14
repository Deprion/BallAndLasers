using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class UIMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isLeft;
    [SerializeField] private PlayerMovement player;

    private void Awake()
    {
        if (YandexGame.EnvironmentData.deviceType == "desktop")
        { 
            Destroy(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLeft)
            player.SetMovement(-1);
        else
            player.SetMovement(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.SetMovement(0);
    }
}
