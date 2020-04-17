using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class GraphComponents : MonoBehaviour
{
    public float SpereTest = 10f;
    private Graph<Vector3, float> graph;
    // Start is called before the first frame update
    private void Start() 
    {
        graph = new Graph<Vector3, float>();
        var node1 = new Node<Vector3>() { Value = Vector3.zero, NodeColor = Color.black };
        var node2 = new Node<Vector3>() { Value = 100*Vector3.one, NodeColor = Color.cyan };
        var edge1 = new Edge<float, Vector3>() {
            Value = 1.0f,
             From = node1, 
             To = node2, 
             EdgeColor = Color.yellow };
        
        graph.Nodes.Add(node1);
        graph.Nodes.Add(node2);
        graph.Edges.Add(edge1);
    }

    // void OnEnable()
	// {
		
	// }

    private void Update(){
        // if (graph == null){
        //     Stari();
        // }
        foreach(var node in graph.Nodes){
            //Gizmos.color = node.NodeColor;
            //Gizmos.DrawSphere(node.Value, SpereTest);
            
        }
        foreach(var edge in graph.Edges){
            //Gizmos.color = edge.EdgeColor;
            Handles.DrawBezier(edge.From.Value,edge.To.Value,edge.From.Value,edge.To.Value, Color.red,null,10);
            //Gizmos.DrawLine(edge.From.Value, edge.To.Value);
        }

    // // Update is called once per frame
    // void Update()
    // {}
        
    }
}
