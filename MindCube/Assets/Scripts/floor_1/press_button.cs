using UnityEngine;
using UnityEngine.EventSystems;

public class press_button : MonoBehaviour, IPointerClickHandler
{
    public int number;
    public GameObject panel;

    public void OnPointerClick(PointerEventData eventData) {
        panel.gameObject.SendMessage("Push_button",number);
    }
}
