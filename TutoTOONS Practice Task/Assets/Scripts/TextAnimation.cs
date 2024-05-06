using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public bool disappear;

    [SerializeField]
    float fadeOutSpeed;

    private void Update()
    {
        if (disappear)
            TextPuff();

        if (this.GetComponent<TextMeshPro>().color.a <= 0)
            Destroy(this);
    }

    void TextPuff()
    {
        Color ogColor = this.GetComponent<TextMeshPro>().color;
        Color newColor = new Color(ogColor.r, ogColor.g, ogColor.b, ogColor.a - fadeOutSpeed * Time.deltaTime);

        this.GetComponent<TextMeshPro>().color = newColor;
    }
}
