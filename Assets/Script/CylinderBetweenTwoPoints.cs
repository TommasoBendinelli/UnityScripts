// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class CylinderBetweenTwoPoints : MonoBehaviour {
//     [SerializeField]
//     private Transform cylinderPrefab;

//     private GameObject leftSphere;
//     private GameObject rightSphere;
//     private GameObject cylinder;
//     private Grid grid;
//     //private float mouseMoveTimer;
//     //private float mouseMoveTimerMax = .01f;

//     private GameObject[] workpieces;
//     private Dictionary<GameObject, Tuple<float, Quaternion>> dict_work_pieces;

//     private Dictionary<GameObject, Vector3> positions;

//     private int movements;


//     private void Start () {

//         workpieces = GameObject.FindGameObjectsWithTag("Workpiece");
//         dict_work_pieces = new Dictionary<GameObject, Tuple<float, Quaternion>>();
//         positions = new Dictionary<GameObject, Vector3>();
//         grid = new Grid(10, 10, 100f, transform.position);
//         foreach (GameObject workpiece in workpieces)
//         {
//             //float rel_distance = Vector3.Distance(GetComponent<Transform>().position, workpiece.transform.position);
//             Vector3 curr_pos = workpiece.GetComponent<Renderer>().bounds.center;
//             //Quaternion rel_orientation = Quaternion.Inverse(GetComponent<Transform>().rotation * workpiece.transform.rotation);
//             positions[workpiece] = curr_pos;
//             //dict_work_pieces[workpiece] = new Tuple<float,Quaternion>(rel_distance,rel_orientation);
//             //grid.SetValue(curr_pos,grid.GetValue(curr_pos)+1);
//             //Debug.Log(dict_work_pieces[workpiece]);
//             InstantiateCylinder(cylinderPrefab, new Vector3(-1, 0, 0), curr_pos);
//         }
//         //leftSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         //rightSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         //leftSphere.transform.position = new Vector3(-1, 0, 0);
//         //rightSphere.transform.position = new Vector3(1, 0, 0);

        
//     }

//     private void Update () {
//         leftSphere.transform.position = new Vector3(-1, -2f * Mathf.Sin(Time.time), 0);
//         rightSphere.transform.position = new Vector3(1, 2f * Mathf.Sin(Time.time), 0);

//         UpdateCylinderPosition(cylinder, leftSphere.transform.position, rightSphere.transform.position);
//     }

//     private void InstantiateCylinder(Transform cylinderPrefab, Vector3 beginPoint, Vector3 endPoint)
//     {
//         cylinder = Instantiate<GameObject>(cylinderPrefab.gameObject, Vector3.zero, Quaternion.identity);
//         UpdateCylinderPosition(cylinder, beginPoint, endPoint);
//     }

//     private void UpdateCylinderPosition(GameObject cylinder, Vector3 beginPoint, Vector3 endPoint)
//     {
//         Vector3 offset = endPoint - beginPoint;
//         Vector3 position = beginPoint + (offset / 2.0f);

//         cylinder.transform.position = position;
//         cylinder.transform.LookAt(beginPoint);
//         Vector3 localScale = cylinder.transform.localScale;
//         localScale.z = (endPoint - beginPoint).magnitude;
//         cylinder.transform.localScale = localScale;
//     }
// }
