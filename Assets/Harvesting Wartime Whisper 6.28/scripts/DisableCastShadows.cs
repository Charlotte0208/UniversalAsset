using UnityEngine;

public class DisableCastShadows : MonoBehaviour
{
    private bool hasExecuted = false;

    void Start()
    {
        // 延迟一帧执行，在Start之后的第二帧执行
        Invoke(nameof(DisableShadows), Time.deltaTime);
    }

    void DisableShadows()
    {
        if (!hasExecuted)
        {
            // 找到场景中的所有对象
            GameObject cursorFocus = GameObject.Find("Cursor_Focus");
            GameObject cursorPress = GameObject.Find("Cursor_Press");

            // 检查是否找到，并关闭其mesh renderer的Cast Shadows属性
            if (cursorFocus != null)
            {
                MeshRenderer meshRenderer = cursorFocus.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }

            if (cursorPress != null)
            {
                MeshRenderer meshRenderer = cursorPress.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }

            hasExecuted = true;
        }
    }
}