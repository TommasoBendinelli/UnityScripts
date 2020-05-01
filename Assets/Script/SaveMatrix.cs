using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;


public class SaveMatrix : MonoBehaviour
{
    [SerializeField]
    private string[,] grid_matrix;

    private List<string[,]> demonstration;

    public void SaveGrid (string name_file)
    {
        demonstration = new List<string[,]>();
        grid_matrix = Testing.ReturnGrid;
        if (File.Exists("Unity_save_test/" + name_file))
        {
            string json = File.ReadAllText("Unity_save_test/"+ name_file);
            demonstration = JsonConvert.DeserializeObject<List<string[,]>>(json);
            demonstration.Add(grid_matrix);
            string res = JsonConvert.SerializeObject(demonstration);
            File.WriteAllText("Unity_save_test/"+name_file,res);

        }
        else 
        {
            // To write to a file, create a StreamWriter object.  
            demonstration.Add(grid_matrix);
            string res = JsonConvert.SerializeObject(demonstration);
            File.WriteAllText("Unity_save_test/"+name_file, res);
 
        }
        Debug.Log(grid_matrix);
    }

    
}
