using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贝塞尔曲线
/// </summary>
public class Bezeir
{
    public static Vector3[] draw_bezier_curves(Vector3[] points, int count, float step)
    {
        List<Vector3> bezier_curves_points = new List<Vector3>();
        float t = 0F;
        do
        {
            var temp_point = bezier_interpolation_func(t, points, count);    // 计算插值点
            t += step;
            bezier_curves_points.Add(temp_point);
        }
        while (t <= 1 && count > 1);    // 一个点的情况直接跳出.
        return bezier_curves_points.ToArray();  // 曲线轨迹上的所有坐标点
    }

    /// <summary>
    /// n阶贝塞尔曲线插值计算函数
    /// 根据起点，n个控制点，终点 计算贝塞尔曲线插值
    /// </summary>
    /// <param name="t">当前插值位置0~1 ，0为起点，1为终点</param>
    /// <param name="points">起点，n-1个控制点，终点</param>
    /// <param name="count">n+1个点</param>
    /// <returns></returns>
    private static Vector3 bezier_interpolation_func(float t, Vector3[] points, int count)
    {
        var PointF = new Vector3();
        var part = new float[count];
        float sum_x = 0, sum_y = 0, sum_z = 0;
        for (int i = 0; i < count; i++)
        {
            ulong tmp;
            int n_order = count - 1;    // 阶数
            tmp = calc_combination_number(n_order, i);
            sum_x += (float)(tmp * points[i].x * Math.Pow((1 - t), n_order - i) * Math.Pow(t, i));
            sum_y += (float)(tmp * points[i].y * Math.Pow((1 - t), n_order - i) * Math.Pow(t, i));
            sum_z += (float)(tmp * points[i].z * Math.Pow((1 - t), n_order - i) * Math.Pow(t, i));
        }
        PointF.x = sum_x;
        PointF.y = sum_y;
        PointF.z = sum_z;
        return PointF;
    }

    /// <summary>
    /// 计算组合数公式
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    private static ulong calc_combination_number(int n, int k)
    {
        ulong[] result = new ulong[n + 1];
        for (int i = 1; i <= n; i++)
        {
            result[i] = 1;
            for (int j = i - 1; j >= 1; j--)
                result[j] += result[j - 1];
            result[0] = 1;
        }
        return result[k];
    }
}
