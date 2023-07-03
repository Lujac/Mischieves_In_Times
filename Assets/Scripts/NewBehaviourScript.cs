using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string word;
    public string wordExpl;
    [SerializeField] GameObject text, textExplanation, bulle;
    bool isOver;
    private void Start()
    {
        bulle.SetActive(false);
    }
    private void Update()
    {
        if (isOver)
        {
            bulle.SetActive(true);
        }
        else
        {
            bulle.SetActive(false);
        }

    }
    public void loadbtn(string wrd, string wrdExp)
    {
        word = wrd;
        wordExpl = wrdExp;
        text.GetComponent<TMP_Text>().text = word;
        textExplanation.GetComponent<TMP_Text>().text = wordExpl;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }


}
