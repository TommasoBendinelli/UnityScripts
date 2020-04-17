using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


public class relative_pos: MonoBehaviour
{
    public int rows;
    public int columns;
    public int dim;
    private GameObject[] workpieces;
    private GameObject plane;
    private GameObject reference;
    private int[,] grid; 

    
    private Dictionary<GameObject, Tuple<float, Quaternion>> dict_work_pieces;
    // Start is called before the first frame update
    void Start()
    {   
        workpieces = GameObject.FindGameObjectsWithTag("Workpiece");
        dict_work_pieces = new Dictionary<GameObject, Tuple<float, Quaternion>>();
        plane = GameObject.Find("Plane");
        reference = GameObject.Find("Cube");
        var plane_norm = plane.GetComponent<Transform>().transform.up;
        var ref_onplane = Vector3.Project(reference.transform.position, plane_norm);
        grid = Grid_creator(ref_onplane,rows,columns,dim);
    }

    // Update is called once per frame
    void Update()
    {   
        
        foreach (GameObject workpiece in workpieces)
        {
            float rel_distance = Vector3.Distance(GetComponent<Transform>().position, workpiece.transform.position);
            Quaternion rel_orientation = Quaternion.Inverse(GetComponent<Transform>().rotation * workpiece.transform.rotation);
            dict_work_pieces[workpiece] = new Tuple<float,Quaternion>(rel_distance,rel_orientation);
            //Debug.Log(dict_work_pieces[workpiece]);
        }


    }

    int[,] Grid_creator(Vector3 ref_pos,int rows, int columns, int dim)
    {
        int[,] grid = new int[rows,columns];
        return grid;

    }

    void ToUnit_fromIndices(Vector3 ref_pos, int row_indices,int columns_indices,int dim)
    {  
        var row_units = (row_indices*dim) - ref_pos.x;
        var columns_unit = (columns_indices*dim) - ref_pos.y;
    }

    
    void ToIndices_fromUnit(Vector3 ref_pos, int row_indices,int columns_indices,int dim)
    {  
        var row_units = row_indices + ref_pos.x;
        var columns_unit = columns_indices + ref_pos.y;
    }




}
