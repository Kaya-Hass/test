using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public int id;
    public PlayerInteractions playerStats;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                if(gameObject.CompareTag("puzzle01"))
                {
                    playerStats.puzzle01 += 1;
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                }
                else if(gameObject.CompareTag("puzzle02"))
                {
                    playerStats.puzzle02 += 1;
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                }
                else if(gameObject.CompareTag("puzzle03"))
                {
                    playerStats.puzzle03 += 1;
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                }
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
            }
        }
    }
    
}
