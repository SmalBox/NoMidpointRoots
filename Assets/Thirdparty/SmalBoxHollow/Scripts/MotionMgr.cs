using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SmalBox.Mgr
{
    public class MotionMgr : MonoBehaviour
    {
        /// <summary> 动作模式 </summary>
        public enum MotionModel
        {
            Mouse,
            Target,
            Mix,
        }
        
        [Header("是否开启调试信息(OpenLog?)")]
        public bool debugLog = true;
        
        [Header("运行模式(RunModel?)")]
        public MotionModel model;
        
        [Header("跟随目标(FollowingTargetTransform)"), Space]
        public Transform targetTransform;
        [Header("开/关 目标跟随(OpenTargetFollowing?)")]
        public bool isTargetFollowing = true;
        
        [Header("开/关 保留痕迹(OpenPreserveTraces?)"), Space]
        public bool isPreserveTraces;
        [Header("保留痕迹分辨率(痕迹分辨率越高痕迹越精细)(TracesPrecision)")]
        public int preserveTracesPrecision = 1000;

        [Header("滚轮缩放可视范围速度(WheelZoomViewSpeed)")]
        public float zoomViewSpeed = 1;
        
        [Header("使用相机(默认自动自动获取MainCamera)(UseOtherCamera?)"), Space]
        public Camera cam;

        private Ray _ray;
        private RaycastHit _hit;
        
        // Shader变量
        private static readonly int Pos = Shader.PropertyToID("_Pos");
        private static readonly int Scale = Shader.PropertyToID("_Scale");
        private static readonly int MainTexture = Shader.PropertyToID("_MainTex");
        private static readonly int SubTexture2 = Shader.PropertyToID("_SubTex2");
        
        /// <summary> 渲染缓存序列 </summary>
        private readonly Queue _rtQueue = new Queue();

        private void Start()
        {
            // 启动获取
            if (cam != null) return;
            cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError($"获取不到MainCam");
            }
        }

        private void Update()
        {
            switch (model)
            {
                case MotionModel.Mouse:
                    if (Input.GetMouseButton(0))
                    {
                        _ray = cam.ScreenPointToRay(Input.mousePosition); // 鼠标点击位置射线
                        StartMotion(_ray, debugLog);
                    }
                    break;
                case MotionModel.Target:
                    if (isTargetFollowing)
                    {
                        Vector3 targetScreenPos = cam.WorldToScreenPoint(targetTransform.position);
                        _ray = cam.ScreenPointToRay(targetScreenPos); // 目标物体射线
                        StartMotion(_ray, debugLog);
                    }
                    break;
                case MotionModel.Mix:
                    if (Input.GetMouseButton(0))
                    {
                        _ray = cam.ScreenPointToRay(Input.mousePosition); // 鼠标点击位置射线
                        StartMotion(_ray, debugLog);
                    }
                    else if (isTargetFollowing)
                    {
                        Vector3 targetScreenPos = cam.WorldToScreenPoint(targetTransform.position);
                        _ray = cam.ScreenPointToRay(targetScreenPos); // 目标物体射线
                        StartMotion(_ray, debugLog);                       
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var meshRender = GetComponent<MeshRenderer>();
            if (meshRender != null)
            {
                Material material = meshRender.material;
                float result = material.GetFloat(Scale) +
                               Input.mouseScrollDelta.y * zoomViewSpeed;
                material.SetFloat(Scale, 
                    result > 0 ? result : 0);
            }

            // 回收RenderTexture，保留队列中的最后一个
            if (!isPreserveTraces) return;
            for (int i = 0; i < _rtQueue.Count - 1; i++)
            {
                RenderTexture.ReleaseTemporary((RenderTexture)_rtQueue.Dequeue());
            }
        }


        /// <summary>
        /// 开始行动！
        /// </summary>
        /// <param name="motionRay">目标射线</param>
        /// <param name="log">是否开启Log</param>
        private void StartMotion(Ray motionRay, bool log = false)
        {
            if (!Physics.Raycast(motionRay, out _hit)) return;
            if (log)
            {
                Debug.DrawRay(motionRay.origin, motionRay.direction, Color.red);
                Debug.Log($"_hit:{_hit.collider.name};point:{_hit.point};{_hit.textureCoord}\n" +
                          $"_hitObjMaterial:{_hit.transform.GetComponent<MeshRenderer>().material.name}");
            }
            
            var meshRenderer = _hit.transform.GetComponent<MeshRenderer>();
            if (meshRenderer == null ||
                meshRenderer.material == null ||
                meshRenderer.material.name
                    .Replace(" (Instance)", "") != "SmalBoxHollow") return;
            // 更改位置坐标
            Material material = _hit.transform.GetComponent<MeshRenderer>().material;
            material.SetVector(Pos, _hit.textureCoord);

            if (!isPreserveTraces) return;
            // 从池中获取指定分辨率的纹理
            RenderTexture rt = RenderTexture.GetTemporary(
                preserveTracesPrecision, preserveTracesPrecision);
            var src = material.GetTexture(MainTexture);
            Graphics.Blit(src, rt, material);
            RenderTexture.active = rt;
            material.SetTexture(SubTexture2, rt);
               
            // 进入回收队列
            _rtQueue.Enqueue(rt);
        }
    }
}