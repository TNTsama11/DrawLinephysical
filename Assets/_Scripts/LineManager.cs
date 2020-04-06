using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour
{
    public GameObject linePfb; //引用刚才制作的预制体
    public GameObject colliderPfb; //Linecollider的引用
    private Line currentLine;
    public Button btn;

    private List<GameObject> gos;

    private void Awake()
    {
        gos = new List<GameObject>();
        btn.onClick.AddListener(CleanAll);
    }

    private void CleanAll()
    {
        foreach(var item in gos)
        {
            Destroy(item);
        }
        gos.Clear();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //按下左键的时候实例化预制体
        {
            GameObject go = Instantiate(linePfb);
            currentLine = go.GetComponent<Line>();
        }
        if (Input.GetMouseButtonUp(0)) //左键抬起的时候停止画线
        {
            foreach (var item in currentLine.GetPoints()) //根据列表里每一个路径点实例化LineCollider
            {
                GameObject go = Instantiate(colliderPfb, new Vector3(item.x, item.y, 0), Quaternion.identity);
                go.transform.parent = currentLine.transform; //将实例化出来的物体作为Line的子物体
            }
            currentLine.gameObject.AddComponent<Rigidbody2D>(); //给Line添加刚体组件
            currentLine.GetComponent<Rigidbody2D>().useAutoMass = true; //设置刚体的useAutoMass属性为true,自动计算物体的质量
            currentLine.GetComponent<LineRenderer>().enabled = false; //禁用掉Line上的LineRenderer组件
            Mesh mesh = new Mesh(); //实例化一个新的Mesh 
            currentLine.lineRenderer.BakeMesh(mesh); //使用LineRenderer的BackMesh()方法将画出来的线烘焙成Mesh，如果不这么做的话，LineRenderer渲染出来的的线是无法跟着物体移动的
            currentLine.meshFilter.sharedMesh = mesh; //将烘焙完的mesh给MeshFilter
            gos.Add(currentLine.gameObject);
            currentLine = null;
        }


        if (currentLine == null)
        {
            return;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //将鼠标再屏幕空间的坐标转换到世界坐标
        currentLine.UpdateLine(mousePosition); //画线

    }
}