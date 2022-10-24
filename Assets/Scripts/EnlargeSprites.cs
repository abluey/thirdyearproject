using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnlargeSprites : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color newColor;

    [SerializeField] private Vector3 originalSize;

    public void OnMouseOver()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        image.color = newColor;
    }

    public void OnMouseExit()
    {
        transform.localScale = originalSize;
        image.color = originalColor;
    }
}
