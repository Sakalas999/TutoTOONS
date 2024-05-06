using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField]
    Sprite clickedSprite, ogSprite;
    [SerializeField]
    GameObject textBox, rope;

    public bool clicked, connected;


    private void Awake()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ogSprite;
    }

    public void SetTextNumber(int val)
    {
        textBox.GetComponent<TextMeshPro>().text = val.ToString();
    }

    private void OnMouseUpAsButton()
    {
        int val = int.Parse(this.textBox.GetComponent<TextMeshPro>().text);

        if (val == 1 && !clicked)
        {
            AudioManager.Instance.PlayCorrectDot();
            clicked = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = clickedSprite;
            textBox.GetComponent<TextAnimation>().disappear = true;
        }
        else if (DotSpawner.Instance.PreviousDotClicked(val - 2) && !clicked)
        {
            AudioManager.Instance.PlayCorrectDot();
            clicked = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = clickedSprite;

            if (textBox != null)
                textBox.GetComponent<TextAnimation>().disappear = true;
        }
        else if (clicked) ; //do nothing
        else
        {
            AudioManager.Instance.PlayWrongDot();
        }
    }

    public IEnumerator DrawRope(Vector3 start, Vector3 end)
    {
        float t = 0;
        float time = 0.5f;
        Vector3 newpos;
        LineRenderer lineRenderer = rope.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);

        for (; t < time; t += Time.deltaTime)
        {
            newpos = Vector3.Lerp(start, end, t / time);
            lineRenderer.SetPosition(1, newpos);
            yield return null;
        }

        lineRenderer.SetPosition(1, end);
        DotSpawner.Instance.isDrawing = false;
        connected = true;
    }
}
