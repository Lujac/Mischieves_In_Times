using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Drop : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool placed = false;
    [SerializeField][Range(1, 5)] private int defNumber;

    private void Awake()
    {
        draggingObject = transform as RectTransform;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(placed) return;
        Vector2 testPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            draggingObject,
            eventData.position,
            eventData.pressEventCamera,
            out testPosition
        )) {
            draggingObject.position = transform.TransformPoint(testPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(placed) return;
        
        if(rb.IsTouching(transform.parent.gameObject.GetComponent<BoxCollider2D>())) {
            // Si l'objet touche le parent, bloquer l'élément au bon endroit
            placed = true;
            transform.localPosition = new Vector3(350, 0, 0);
            
            puzzleDico.SetDefBool(defNumber);
            
            if(puzzleDico.IsAllDefsTrue()) {
                var ShlockDialog = GameObject.Find("PNJ_Sherlock").GetComponent<DialogueSystem>().dialogueDatas;
                ShlockDialog[0] = ShlockDialog[1];
            }
        } else {
            // Sinon, reset sa position
            transform.position = startPosition;
        }
    }

}
