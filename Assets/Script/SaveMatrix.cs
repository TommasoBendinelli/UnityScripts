using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;


public class SaveMatrix : MonoBehaviour
{
    [SerializeField]
    private int[,] grid_matrix;
    public void SaveGrid ()
    {
        grid_matrix = Testing.ReturnGrid;
        // To write to a file, create a StreamWriter object.  
        string res = JsonConvert.SerializeObject(grid_matrix);
        Debug.Log(res);
        if (File.Exists("Unity_save_test/matrix.json"))
        {
           using (StreamWriter w = File.AppendText("Unity_save_test/matrix.json"))
                {
                w.WriteLine(res);
                } 
        }
        else 
        {
            File.WriteAllText("Unity_save_test/matrix.json", res);
        }
        Debug.Log(grid_matrix[0,0]);
    }
}
