using TMPro;
using UnityEngine;

public class TranslateText : MonoBehaviour
{
    [SerializeField][TextArea] private string text;

    private void Awake()
    {
        GetComponent<TMP_Text>().text = TranslateManager.inst.GetText(text);
    }
}
