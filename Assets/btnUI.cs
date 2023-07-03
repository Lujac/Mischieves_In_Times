using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class btnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isover)
        {
            PlayerControl.agentMovementInstance.isActive = false;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isover= true;
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isover = false;
        PlayerControl.agentMovementInstance.isActive = true;
    }
}
