using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchDraw : MonoBehaviour {

    public Material lineRenderMaterial;
    public Color lineRenderColorStart;
    public Color lineRenderColorEnd;
    public Vector2 LineSizes;
    public LayerMask DrawZoneLayer;

    [SerializeField] private List<LineRenderer> lineRenderers;
    [SerializeField] private List<List<Vector3>> pointList;

    private int currentLine = -1;

    void Start() {
        lineRenderers = new List<LineRenderer>();
        pointList = new List<List<Vector3>>();
    }


    /// <summary>
    /// Upates the array of points to be draw
    /// </summary>
    void Update() {
        /*
        if (pointList != null) {
            lineRenderer.SetVertexCount(pointList.Count);
            for (var i = 0; i < pointList.Count; i++) {
                lineRenderer.SetPosition(i, pointList[i]);
            }
        }
        */

        if (Input.touchCount > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                 CreateNewLine();
                 InvokeRepeating("AddPoint", 0.05f, 0.05f); //Calls Addpoint every 0.1seconds
            }
        } else {
            CancelInvoke();
        }
    }

    void CreateNewLine() {
        currentLine++;
        //setup new LineRenderer
        GameObject newGO = new GameObject("LineRenderer Object");
        newGO.transform.parent = transform;
        LineRenderer newLR = newGO.gameObject.AddComponent<LineRenderer>(); ;
        newLR.SetVertexCount(0);
        newLR.material = lineRenderMaterial;
        newLR.SetWidth(LineSizes.x, LineSizes.y);
        newLR.SetColors(lineRenderColorStart, lineRenderColorEnd);
        lineRenderers.Add(newLR);
        pointList.Add(new List<Vector3>());

    }

    /// <summary>
    /// Adds a point to the point List
    /// </summary>lineRenderers
    void AddPoint() {
        //Debug.Log("Added points !");
        //Get new position
        if (Input.touches.Length > 0) {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            RaycastHit hit;
            Debug.DrawRay(newPos, new Vector3(0, 0, 100), Color.red, 1f);
            if(Physics.Raycast(newPos,new Vector3(0,0,100),out hit, DrawZoneLayer)) {
                newPos += new Vector3(0, 0, 9);


                pointList[currentLine].Add(newPos);

                lineRenderers[currentLine].SetVertexCount(pointList[currentLine].Count);
                lineRenderers[currentLine].SetPosition(pointList[currentLine].Count - 1, newPos);
            }

           
        }
    }
}
