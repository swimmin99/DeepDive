using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 40;
    public TextMeshProUGUI textMesh;
    public Vector3 floatDirection = new Vector3(0, 1, 0);
    RectTransform rTransform;
    Color startingColor;
    float timeElapsed = 0.0f;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        startingColor = textMesh.color;
    }
        // Update is called once per frame
        void Update()
        {
            timeElapsed += Time.deltaTime;
            textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));
            if (floatSpeed > 0)
            {
                floatSpeed -= 75f * Time.deltaTime;
                rTransform.position += floatDirection * floatSpeed * Time.deltaTime;
            }
            if (timeElapsed >= timeToLive)
            {
                Destroy(gameObject);
            }
        }
}
