using UnityEngine;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameController.Instance.CollectByTap(eventData.position, transform);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
