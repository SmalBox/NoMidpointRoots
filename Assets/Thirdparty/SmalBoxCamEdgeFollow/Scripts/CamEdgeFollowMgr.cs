using UnityEngine;

namespace SmalBox.Mgr
{
    public class CamEdgeFollowMgr : MonoBehaviour
    {
        [Header("是否开启调试信息(OpenLog?)")]
        [SerializeField] private bool debugLog;
        
        [Header("移动相机(默认为None会自动获取MainCamera)")]
        [SerializeField] private Camera moveCam;
        
        [Header("相机要跟随的目标")]
        [SerializeField] private Transform targetTransform;
        
        [Header("目标自由移动X轴范围")]
        [Header("当屏幕范围值不为0时切换使用屏幕边缘范围方式")]
        [Header("注：移动轴范围 和 屏幕边缘范围 二选一,默认使用移动轴范围.")]
        [SerializeField] private Vector2 targetRangeOffsetX;
        [Header("目标自由移动Y轴范围")]
        [SerializeField] private Vector2 targetRangeOffsetY;
        
        [Header("目标移动屏幕边缘左右范围")]
        [SerializeField] private Vector2 targetScreenEdgeOffsetLeftRight;
        [Header("目标移动屏幕边缘上下范围")]
        [SerializeField] private Vector2 targetScreenEdgeOffsetUpDown;
        
        [Header("是否开启范围参考图")]
        [SerializeField] private bool openReferenceArea;
        [Header("参考范围图")]
        [SerializeField] private RectTransform referenceArea;
        [Header("参考范围中点")]
        [SerializeField] private RectTransform referencePivotPoint;

        /// <summary> 基准点坐标 </summary>
        private Vector2 _pivot;
        /// <summary> targetTransform的目标点坐标 </summary>
        private Vector2 _targetPos;
        /// <summary> 相机移动偏移 </summary>
        private Vector2 _camOffsetPos;

        /// <summary> 目标范围X </summary>
        private Vector2 _targetRangeX;
        /// <summary> 目标范围Y </summary>
        private Vector2 _targetRangeY;

        private void Start()
        {
            #region 初始化私有变量
            _pivot = new Vector2();
            _targetPos = new Vector2();
            _camOffsetPos = new Vector2();
            _targetRangeX = new Vector2();
            _targetRangeY = new Vector2();
            #endregion
        }

        private void OnEnable()
        {
            if (moveCam == null) moveCam = Camera.main;
        }

        private void Update()
        {
            if (targetTransform == null)
            {
                Debug.Log($"<color:red>TargetTransform为Null</color>");
            }
            else
            {
                // 设置基准点坐标
                _pivot.x = (float)Screen.width / 2;
                _pivot.y = (float)Screen.height / 2;
                
                // 根据屏幕范围参数决定是否生效(生效则转换为屏幕范围控制)
                if (targetScreenEdgeOffsetLeftRight != Vector2.zero)
                {
                    // 计算目标范围X:屏幕边缘范围方式
                    _targetRangeX.x = targetScreenEdgeOffsetLeftRight.x;
                    _targetRangeX.y = _pivot.x * 2 - targetScreenEdgeOffsetLeftRight.y;
                }
                else
                {
                    // 计算目标范围X:移动轴方式
                    _targetRangeX.x = _pivot.x + targetRangeOffsetX.x;
                    _targetRangeX.y = _pivot.x + targetRangeOffsetX.y;
                }
                if (targetScreenEdgeOffsetUpDown != Vector2.zero)
                {
                     // 计算目标范围Y:屏幕边缘范围方式
                    _targetRangeY.x = targetScreenEdgeOffsetUpDown.x;
                    _targetRangeY.y = _pivot.y * 2 - targetScreenEdgeOffsetUpDown.y;
                }
                else
                {
                    // 计算目标范围Y:移动轴方式
                    _targetRangeY.x = _pivot.y + targetRangeOffsetY.x;
                    _targetRangeY.y = _pivot.y + targetRangeOffsetY.y;
                }

                // 目标在屏幕上的点坐标
                _targetPos = moveCam.WorldToScreenPoint(targetTransform.position);
                
                // 计算范围外的偏移量
                _camOffsetPos = Vector2.zero;
                var temp = _targetPos.x - _targetRangeX.x;
                if (temp < 0) _camOffsetPos.x = temp;
                temp = _targetPos.x - _targetRangeX.y;
                if (temp > 0) _camOffsetPos.x = temp;
                temp = _targetPos.y - _targetRangeY.x;
                if (temp < 0) _camOffsetPos.y = temp;
                temp = _targetPos.y - _targetRangeY.y;
                if (temp > 0) _camOffsetPos.y = temp;

                if (_camOffsetPos != Vector2.zero)
                {
                    if (debugLog) Debug.Log($"外," +
                                            $"targetPos:{targetTransform.position}," +
                                            $"targetPosScreen:{_targetPos}," +
                                            $"targetRangeX:{_targetRangeX}," +
                                            $"targetRangeY:{_targetRangeY}");
                    // 偏移量加上中心点坐标得到实际要移动的屏幕边缘位置
                    _camOffsetPos += _pivot;
                    // 转换屏幕坐标到世界坐标
                    Vector3 moveCamWorldPos = moveCam.ScreenToWorldPoint(_camOffsetPos);
                    moveCam.transform.position = moveCamWorldPos;
                }
                else
                {
                    if (debugLog) Debug.Log($"内");
                }
            }

            #region 设置参考范围显示

            if (openReferenceArea)
            {
                if (referenceArea == null || referencePivotPoint == null) return;
                referenceArea.gameObject.SetActive(true);
                referencePivotPoint.gameObject.SetActive(true);
                // 区域左边绘制
                referenceArea.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Left,
                    _targetRangeX.x,
                    _targetRangeX.y - _targetRangeX.x);
                // 区域下边绘制
                referenceArea.SetInsetAndSizeFromParentEdge(
                    RectTransform.Edge.Bottom,
                    _targetRangeY.x,
                    _targetRangeY.y - _targetRangeY.x);
                // 中心点位置
                referencePivotPoint.anchoredPosition = _pivot;
            }
            else
            {
                referenceArea.gameObject.SetActive(false);
                referencePivotPoint.gameObject.SetActive(false);
            }

            #endregion
        }
    }
}