using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// 相机视野控制类，用于控制摄像机的旋转、缩放、平移、聚焦等操作
    /// </summary>
    public class MainCameraController : BaseController
    {

        private Vector2 euler;
        private Quaternion targetRot;
        private Vector3 targetLookAt;
        private float thetaSpeed = 230.5f;
        private float phiSpeed = 110.5f;
        [SerializeField]
        private float zoomSpeed = 10.0f;
        private float targetDist;
        private Vector3 distanceVec = new Vector3(0, 0, 0);
        private Transform target;
        [SerializeField]
        private Transform pivotPoint;
        private Vector3 yz;
        private float phiBoundMin = -89.5f;
        private float phiBoundMax = 89.5f;
        private float rotateSmoothing = 0.6f;
        private float moveSmoothing = 0.22f;
        private float distance = 2.0f;
        [SerializeField]
        private float minDistance = 0.3f;
        [SerializeField]
        private float maxDistance = 3f;

        [SerializeField]
        private float moveSpeed = 3f;

        public bool IsControl = true;


        /// <summary>
        /// 初始化方法，添加对聚焦及设置方位事件监听
        /// </summary>
        protected void Awake()
        {

            EventCenter.AddListener<Vector3, bool>(EventCode.CameraLookAtTarget, FocusTarget);
            EventCenter.AddListener<Vector3, Quaternion, float>(EventCode.SetCameraPosAndRot, SetTransAndRot);
        }


        /// <summary>
        /// 初始化方法， 对相机控制参数进行设置
        /// </summary>
        private void Start()
        {
            GlobalData.MainCamera = Camera.main;
            Vector3 angles = transform.eulerAngles;
            euler.x = angles.y;
            euler.y = angles.x;
            euler.y = Mathf.Repeat(euler.y + 180f, 360f) - 180f;

            GameObject go = new GameObject();
            target = go.transform;
            if (pivotPoint == null) pivotPoint = go.transform;
            target.position = pivotPoint.position;
            targetDist = (transform.position - target.position).magnitude;
            targetRot = transform.rotation;
            targetLookAt = target.position;
        }

        /// <summary>
        /// 延时更新方法，监听键盘鼠标事件，并将更新后的数据赋值给相应的控制变量
        /// </summary>
        public void LateUpdate()
        {
            if (!IsControl) return;

            if (target)
            {
                float dx = Input.GetAxis("Mouse X");
                float dy = Input.GetAxis("Mouse Y");
                bool click1 = Input.GetMouseButton(1);
                bool click2 = Input.GetMouseButton(2);
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                if (horizontal != 0 || vertical != 0)
                {
                    yz = transform.forward;
                    targetLookAt += (yz * vertical + transform.right * horizontal) * 0.05f * moveSpeed;
                }
                if (click2)
                {
                    yz = transform.up;
                    targetLookAt -= (yz * dy + transform.right * dx) * 0.3f * moveSpeed;
                }
                else if (click1)
                {
                    dx = dx * thetaSpeed * 0.02f;
                    dy = dy * phiSpeed * 0.02f;
                    euler.x += dx;
                    euler.y -= dy;
                    euler.y = ClampAngle(euler.y, phiBoundMin, phiBoundMax);
                    targetRot = Quaternion.Euler(euler.y, euler.x, 0);

                }
                targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 0.5f;
                targetDist = Mathf.Max(minDistance, targetDist);
                targetDist = Mathf.Min(maxDistance, targetDist);
            }

        }
        /// <summary>
        /// GUI更新方法，用来实现双击聚焦功能
        /// </summary>
        private void OnGUI()
        {
            if (!IsControl) return;

            if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    FocusTarget(hit.point, false);
                }
            }
        }
        /// <summary>
        /// 聚焦到某个目标位置
        /// </summary>
        private void FocusTarget(Vector3 targetVec, bool isChangeDistance)
        {
            if (gameObject.activeSelf == false) return;
            StartCoroutine(WaitLookAtTarget(targetVec, isChangeDistance));
        }
        /// <summary>
        /// 固定更新方法，根据变量数值改变相机的位置和旋转
        /// </summary>
        public void FixedUpdate()
        {
            if (!IsControl) return;

            distance = moveSmoothing * targetDist + (1 - moveSmoothing) * distance;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSmoothing);
            target.position = Vector3.Lerp(target.position, targetLookAt, moveSmoothing);
            distanceVec.z = distance;
            transform.position = target.position - transform.rotation * distanceVec;
        }
        /// <summary>
        /// 用来延时实现聚焦功能
        /// </summary>
        /// <param name="targetVec"></param>
        /// <param name="isChangeDistance"></param>
        /// <returns></returns>
        IEnumerator WaitLookAtTarget(Vector3 targetVec, bool isChangeDistance)
        {
            yield return new WaitForSeconds(0.1f);
            if (isChangeDistance)
            {
                targetDist = 5f;
            }
            else
            {
                targetDist = (transform.position - targetVec).magnitude;
            }
            targetRot = transform.rotation;
            targetLookAt = targetVec;
        }
        /// <summary>
        /// 用来对旋转角度进行限定
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
        /// <summary>
        /// 销毁方法，用于移除事件监听
        /// </summary>
        protected void OnDestroy()
        {
            EventCenter.RemoveListener<Vector3, bool>(EventCode.CameraLookAtTarget, FocusTarget);
            EventCenter.RemoveListener<Vector3, Quaternion, float>(EventCode.SetCameraPosAndRot, SetTransAndRot);
        }

        /// <summary>
        /// 用于设置摄像机位置以及旋转
        /// </summary>
        private void SetTransAndRot(Vector3 pos, Quaternion rot, float distance)
        {
            targetDist = distance;
            Vector3 angles = rot.eulerAngles;
            euler.x = angles.y;
            euler.y = angles.x;
            euler.y = Mathf.Repeat(euler.y + 180f, 360f) - 180f;
            targetRot = Quaternion.Euler(euler.y, euler.x, 0);
            targetLookAt = pos;
        }

    }
}