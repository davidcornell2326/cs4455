using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    public float percentage;
    public SpriteRenderer filler;
    public bool isJello;

    // Start is called before the first frame update
    void Start()
    {
        if (!isJello)
        {
            percentage = 1f;
            filler.transform.localScale = new Vector3(300 * percentage, 15);
        }
        else
        {
            percentage = 0f;
            filler.transform.localScale = new Vector3(200 * percentage, 15);
        }

    }

    public void SetPercentage(float p) {
        //if (percentage < 1f) {    // commented out because the MIN takes care of it, and it only makes sense for jello bars (those that count up, not down)
            percentage = p;
            percentage = Mathf.Min(percentage, 1f);
            if (!isJello) {
                filler.transform.localScale = new Vector3(300 * percentage, 15);
                filler.transform.localPosition = new Vector3(-(150 - (300 * percentage * 0.5f)), 0);
            } else {
                filler.transform.localScale = new Vector3(200 * percentage, 15);
                filler.transform.localPosition = new Vector3(-(150 - (200 * percentage * 0.5f)), 0);
            }
        //}
    }

}
