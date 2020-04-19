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
using CodeMonkey.Utils;
using System;


public class Testing : MonoBehaviour {
    [SerializeField]
    private Transform cylinderPrefab;

    private static Grid grid;
    //private float mouseMoveTimer;
    //private float mouseMoveTimerMax = .01f;

    private GameObject[] workpieces;
    private Dictionary<GameObject, Tuple<float, Quaternion>> dict_work_pieces;

    private Dictionary<GameObject, Vector3> positions;
    private Dictionary<GameObject, int> cells_occupied;

    private int movements;

    private int[,] adjacency_matrix;

    private Line[,] line_matrix;

    private static int grid_dim = 10;




   private void Start() {
        int i;
        int j;
        workpieces = GameObject.FindGameObjectsWithTag("Workpiece");
        dict_work_pieces = new Dictionary<GameObject, Tuple<float, Quaternion>>();
        positions = new Dictionary<GameObject, Vector3>();
        cells_occupied = new Dictionary<GameObject, int>();
        grid = new Grid(grid_dim, grid_dim, 100f, transform.position);
        line_matrix = new Line[workpieces.Length,workpieces.Length];
        adjacency_matrix = new int[workpieces.Length, workpieces.Length];
        //adjacency_matrix = lower_fill(adjacency_matrix, workpieces.Length, workpieces.Length);
        //adjacency_matrix = upper_fill(adjacency_matrix, workpieces.Length, workpieces.Length);
        adjacency_matrix = fast_fill(adjacency_matrix, workpieces.Length, workpieces.Length);
        //print(adjacency_matrix, workpieces.Length, workpieces.Length);
        for (i = 0; i < workpieces.Length; i++) 
        {
            Vector3 curr_pos = workpieces[i].GetComponent<Renderer>().bounds.center;
            //Quaternion rel_orientation = Quaternion.Inverse(GetComponent<Transform>().rotation * workpiece.transform.rotation);
            positions[workpieces[i]] = curr_pos;
            if (workpieces[i].name != "B")
                {cells_occupied[workpieces[i]] = 1;}
            else 
                {cells_occupied[workpieces[i]] = 3;}
            //dict_work_pieces[workpiece] = new Tuple<float,Quaternion>(rel_distance,rel_orientation);
            //grid.SetValue(curr_pos,grid.GetValue(curr_pos)+1);
            grid.SetValue(curr_pos,grid.GetString(curr_pos) +workpieces[i].name + " ",cells_occupied[workpieces[i]]);
            //grid.SetValue(curr_pos,grid.GetString(curr_pos) +workpieces[i].name + " ");

            for (j = 0; j< workpieces.Length; j++)
            {
                if (i<=j)
                {
                    continue;
                }
                line_matrix[i,j] = new Line(cylinderPrefab, curr_pos, workpieces[j].GetComponent<Renderer>().bounds.center);   
            }
            //Debug.Log(dict_work_pieces[workpiece]);
            // foreach (GameObject workpice_inner in workpieces)
            // {
            //     if (workpiece == workpice_inner)
            //     {
            //         continue;
            //     }
            //     CylindePrep(cylinderPrefab, new Vector3(-1, 0, 0), curr_pos);

            // }
        }
        
        //HeatMapVisual heatMapVisual = new HeatMapVisual(grid, GetComponent<MeshFilter>());
    }



    private void Update() {
        movements = 0;
        int i; 
        int j;
        for (i = 0; i < workpieces.Length; i++) 
        {
            //float rel_distance = Vector3.Distance(GetComponent<Transform>().position, workpiece.transform.position);
            Vector3 curr_pos = workpieces[i].GetComponent<Renderer>().bounds.center;
            if (curr_pos != positions[workpieces[i]])
                {
                    movements = 1;
                    //grid.SetValue(positions[workpieces[i]],grid.GetValue(positions[workpieces[i]])-1);
                    string remove_val = remove_first_occurence(grid.GetString(positions[workpieces[i]]), workpieces[i].name + " ");
                    grid.SetValue(positions[workpieces[i]],remove_val,cells_occupied[workpieces[i]]);
                    positions[workpieces[i]] = curr_pos;
                    //grid.SetValue(positions[workpieces[i]],grid.GetValue(positions[workpieces[i]])+1);
                    grid.SetValue(curr_pos,grid.GetString(curr_pos) +workpieces[i].name + " ",cells_occupied[workpieces[i]]);
                    for (j =0; j < workpieces.Length; j++)
                    {
                        if (i==j)
                        {
                            continue;
                        }
                        else
                        {
                            if (i<j)
                                {
                                line_matrix[j,i].UpdateCylinderPosition(workpieces[j].GetComponent<Renderer>().bounds.center, workpieces[i].GetComponent<Renderer>().bounds.center);  
                                }
                            else 
                                {
                                    line_matrix[i,j].UpdateCylinderPosition(workpieces[i].GetComponent<Renderer>().bounds.center, workpieces[j].GetComponent<Renderer>().bounds.center);  
                                }
                        }
                        
                    }
                    //line_matrix[i,j].UpdateCylinderPosition(workpieces[i].GetComponent<Renderer>().bounds.center, workpieces[j].GetComponent<Renderer>().bounds.center);   
                }
        }
    }



            //Quaternion rel_orientation = Quaternion.Inverse(GetComponent<Transform>().rotation * workpiece.transform.rotation);
            //dict_work_pieces[workpiece] = new Tuple<float,Quaternion>(rel_distance,rel_orientation);
            //grid.SetValue(curr_pos,1);
            //Debug.Log(dict_work_pieces[workpiece]);
        // if (movements == 0)
        // {
        //     //Debug.Log(grid.GridArray);
        // }

    public static string[,] ReturnGrid
    {
        get {return remove_end_space(grid.StringGrid, grid_dim);}
    } 

    static string[,] remove_end_space(string [,] gridstring, int grid_dim)
    {   
        string[,] res = new string[grid_dim,grid_dim];
        for (int i = 0; i<grid_dim; i++)
        {
            for (int j = 0; j<grid_dim;j++)
            {   
                if (gridstring[i,j] != null)
                {
                    res[i,j] = gridstring[i,j].Trim();
                }

            }
        }
        return res;
    }

    private string remove_first_occurence(string sourceString, string removeString)
    {
        int index = sourceString.IndexOf(removeString);
        string cleanPath = (index < 0)
        ? sourceString
        : sourceString.Remove(index, removeString.Length);
        return cleanPath;
    }

    private int[,] fast_fill(int[,] adjacency_matrix, int row, int col){
        int i, j; 
        for (i = 0; i < row; i++) 
        { 
            for (j = 0; j < col; j++) 
            { 
                if (i <= j) 
                { 
                    continue;
                } 
                else   
                    adjacency_matrix[i, j] = 1;
                    adjacency_matrix[j,i] = 1;
            } 
        } 
        return adjacency_matrix;
    }

    private int[,] lower_fill(int[,] adjacency_matrix, int row, int col){
        int i, j; 
        for (i = 0; i < row; i++) 
        { 
            for (j = 0; j < col; j++) 
            { 
                if (i <= j) 
                { 
                    continue;
                } 
                else   
                    adjacency_matrix[i, j] = 1;
            } 
        } 
        return adjacency_matrix;
    }

    private int[,] upper_fill(int[,] adjacency_matrix, int row, int col){
        int i, j; 
        for (i = 0; i < row; i++) 
        { 
            for (j = 0; j < col; j++) 
            { 
                if (i >= j) 
                { 
                    continue;
                } 
                else   
                    adjacency_matrix[i, j] = 1;
            } 
        } 
        return adjacency_matrix;
    }

    private void print(int[,] adjacency_matrix, int row, int col){
        int i, j; 
        string row_string;
        for (i = 0; i < row; i++) 
        { 
            row_string = "";
            for (j = 0; j < col; j++) 
            { 
                row_string = row_string + " " + adjacency_matrix[i,j].ToString() ;
            } 
        Debug.Log(string.Format(row_string));
        } 
    }
}



    // private void HandleClickToModifyGrid() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
    //     }
    // }

    // private void HandleHeatMapMouseMove() {
    //     mouseMoveTimer -= Time.deltaTime;
    //     if (mouseMoveTimer < 0f) {
    //         mouseMoveTimer += mouseMoveTimerMax;
    //         int gridValue = grid.GetValue(UtilsClass.GetMouseWorldPosition());
    //         grid.SetValue(UtilsClass.GetMouseWorldPosition(), gridValue + 1);
    //     }
    // }


    // private class HeatMapVisual {

    //     private Grid grid;
    //     private Mesh mesh;

    //     public HeatMapVisual(Grid grid, MeshFilter meshFilter) {
    //         this.grid = grid;
            
    //         mesh = new Mesh();
    //         meshFilter.mesh = mesh;

    //         UpdateHeatMapVisual();

    //         grid.OnGridValueChanged += Grid_OnGridValueChanged;
    //     }

    //     private void Grid_OnGridValueChanged(object sender, System.EventArgs e) {
    //         UpdateHeatMapVisual();
    //     }

    //     public void UpdateHeatMapVisual() {
    //         Vector3[] vertices;
    //         Vector2[] uv;
    //         int[] triangles;

    //         MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out vertices, out uv, out triangles);

    //         for (int x = 0; x < grid.GetWidth(); x++) {
    //             for (int y = 0; y < grid.GetHeight(); y++) {
    //                 int index = x * grid.GetHeight() + y;
    //                 Vector3 baseSize = new Vector3(1, 1) * grid.GetCellSize();
    //                 int gridValue = grid.GetValue(x, y);
    //                 int maxGridValue = 100;
    //                 float gridValueNormalized = Mathf.Clamp01((float)gridValue / maxGridValue);
    //                 Vector2 gridCellUV = new Vector2(gridValueNormalized, 0f);
    //                 MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + baseSize * .5f, 0f, baseSize, gridCellUV, gridCellUV);
    //             }
    //         }

    //         mesh.vertices = vertices;
    //         mesh.uv = uv;
    //         mesh.triangles = triangles;
    //     }