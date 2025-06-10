using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // Dùng static để có thể truy cập mảng points từ bất kỳ script nào khác
    public static Transform[] points;

    private void Awake()
    {
        // Lấy tất cả các Transform của các GameObject con
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
