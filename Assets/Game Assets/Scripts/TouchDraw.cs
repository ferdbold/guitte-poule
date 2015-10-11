using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tendresse.Data;


namespace Tendresse.Data {
    [SerializeField]
    public struct TendresseData  {
        
        public List<List<Vector3>> pointList;
        public TendresseData(List<List<Vector3>> points)
        {
            pointList = points;
        }
    }
};

public class TouchDraw : MonoBehaviour {

    public bool canDraw = true;

    [Header("LineRenderer Properties")]
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
        if (Input.touchCount > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                 CreateNewLine();
                 InvokeRepeating("AddPoint", 0.05f, 0.05f); //Calls Addpoint every 0.1seconds
            }
        } else {
            CancelInvoke();
        }
    }

    /// <summary>
    /// Creates a new line renderer and set up points to draw in it
    /// </summary>
    void CreateNewLine() {
        currentLine++;
        //setup new LineRenderer
        lineRenderers.Add(CreateNewLineRendererObject());
        pointList.Add(new List<Vector3>());

    }

    //Create new line renderer object
    private LineRenderer CreateNewLineRendererObject() {
        GameObject newGO = new GameObject("LineRenderer Object");
        newGO.transform.parent = transform;
        LineRenderer newLR = newGO.gameObject.AddComponent<LineRenderer>(); ;
        newLR.SetVertexCount(0);
        newLR.material = lineRenderMaterial;
        newLR.SetWidth(LineSizes.x, LineSizes.y);
        newLR.SetColors(lineRenderColorStart, lineRenderColorEnd);

        return newLR;
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

    /// <summary>
    /// Saves current Draw data into a Tendresse Data
    /// </summary>
    /// <returns></returns>
    public TendresseData SaveCurrentData() {
        TendresseData TData = new TendresseData();
        TData.pointList = pointList;

        return TData;
    }

    public void LoadTendresseData(TendresseData TData, Vector3 translation, float scale) {
        WipeDrawData();
        pointList = TData.pointList;
        lineRenderers = new List<LineRenderer>();

        for (int i=0; i < pointList.Count; i++) {
            lineRenderers.Add(CreateNewLineRendererObject());
            lineRenderers[i].SetWidth(LineSizes.x*scale, LineSizes.y*scale);
            lineRenderers[i].SetVertexCount(TData.pointList[i].Count);
            for (int j = 0; j < TData.pointList[i].Count; j++) {
                lineRenderers[i].SetPosition(j, (TData.pointList[i][j] + translation) * scale);
            }
        } 
    }

    


    public void WipeDrawData() {

        foreach(LineRenderer lr in lineRenderers) {
            Destroy(lr);
        }

        currentLine = -1;
        lineRenderers = new List<LineRenderer>();
        pointList = new List<List<Vector3>>();
    }
}
