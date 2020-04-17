/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Line {

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private GameObject cylinder;
    private int[,] gridArray;
    public Line(Transform cylinderPrefab, Vector3 beginPoint, Vector3 endPoint)
    {
        this.cylinder = GameObject.Instantiate<GameObject>(cylinderPrefab.gameObject, Vector3.zero, Quaternion.identity);
        UpdateCylinderPosition(beginPoint, endPoint);
    }

    public void UpdateCylinderPosition(Vector3 beginPoint, Vector3 endPoint)
    {
        Vector3 offset = endPoint - beginPoint;
        Vector3 position = beginPoint + (offset / 2.0f);

        this.cylinder.transform.position = position;
        this.cylinder.transform.LookAt(beginPoint);
        Vector3 localScale = cylinder.transform.localScale;
        localScale.z = (endPoint - beginPoint).magnitude;
        cylinder.transform.localScale = localScale;
    }

    // public int GetWidth() {
    //     return width;
    // }

    // public int GetHeight() {
    //     return height;
    // }

    // public float GetCellSize() {
    //     return cellSize;
    // }

    // public Vector3 GetWorldPosition(int x, int z) {
    //     return new Vector3(x,0, z) * cellSize + originPosition;
    // }

    // private void GetXZ(Vector3 worldPosition, out int x, out int z) {
    //     x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
    //     z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    // }

    // public void SetValue(int x, int z, int value) {
    //     if (x >= 0 && z >= 0 && x < width && z < height) {
    //         gridArray[x, z] = value;
    //         if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
    //     }
    // }

    // public int[,] GridArray
    // {
    //     get {return gridArray;}
    // }

    // public void SetValue(Vector3 worldPosition, int value) {
    //     int x, z;
    //     GetXZ(worldPosition, out x, out z);
    //     SetValue(x, z, value);
    // }

    // public int GetValue(int x, int z) {
    //     if (x >= 0 && z >= 0 && x < width && z < height) {
    //         return gridArray[x, z];
    //     } else {
    //         return 0;
    //     }
    // }

    // public int GetValue(Vector3 worldPosition) {
    //     int x, z;
    //     GetXZ(worldPosition, out x, out z);
    //     return GetValue(x, z);
    // }

}
