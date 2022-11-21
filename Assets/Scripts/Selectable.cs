using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    [SerializeField] private Image outline;
    [SerializeField] private Color highlightColor = Color.green;
    
    private Color startColor;
    
    private void Start()
    {
        startColor = outline.color;
    }

    private void OnMouseEnter()
    {
        outline.color = highlightColor;
        outline.canvas.sortingOrder += 50;
    }

    private void OnMouseExit()
    {
        outline.color = startColor;
        outline.canvas.sortingOrder -= 50;
    }
}
