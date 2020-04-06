using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float density; //路径点的间隔
    public MeshFilter meshFilter;
    private List<Vector2> points; //用来保存路径点

    //更新线的方法，传入鼠标位置作为参数
    public void UpdateLine(Vector2 mousePosition)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePosition);
            return;
        }
        if (Vector2.Distance(points.Last(), mousePosition) > density) //这里用到了Linq里的扩展方法list.Last()获取列表最后一个元素，和当前坐标点进行距离计算
        {
            SetPoint(mousePosition);
        }
    }

    //为linerenderer添加点
    private void SetPoint(Vector2 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }

    public List<Vector2> GetPoints()
    {
        return points;
    }
}
