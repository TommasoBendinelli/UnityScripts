/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake() {
        //graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        //GameObject gameObject = new GameObject("circle", typeof(Image));
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 temp = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), 0, UnityEngine.Random.Range(-10.0f, 10.0f));
        gameObject.transform.position += temp;    
        //gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().sprite = circleSprite;
        //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = anchoredPosition;
        //rectTransform.sizeDelta = new Vector2(11, 11);
        //rectTransform.anchorMin = new Vector2(0, 0);
        //rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList) {
        float graphHeight = 100f; //graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.transform.position, circleGameObject.transform.position);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector3 dotPositionA, Vector3 dotPositionB) {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Renderer>().material.color = new Color(1,1,1, .5f);
        //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector3 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector3.Distance(dotPositionA, dotPositionB);
        // rectTransform.anchorMin = new Vector2(0, 0);
        // rectTransform.anchorMax = new Vector2(0, 0);
        // rectTransform.sizeDelta = new Vector2(distance, 3f);
        // rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        // rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
