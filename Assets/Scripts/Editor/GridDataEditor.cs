using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridData))]
public class GridDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridData gridData = (GridData)target;

        // 绘制带扁长方形边框的网格
        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 5; j++)
            {
                // 跳过角落的格子
                if ((i == 0 || i == 4) && (j == 0 || j == 4))
                {
                    GUILayout.Space(28); // 与垂直扁长方形宽度一致的占位空间
                    continue;
                }

                // 中心的 3x3 网格
                if (i > 0 && i < 4 && j > 0 && j < 4)
                {
                    DrawButton(i, j, gridData, 50, 50); // 正方形按钮
                }
                else
                {
                    // 周围的 12 个扁长方形格子
                    if ((i == 0 || i == 4) && (j == 1 || j == 2 || j == 3))
                    {
                        DrawButton(i, j, gridData, 50, 25); // 水平扁长方形
                    }
                    else if ((j == 0 || j == 4) && (i == 1 || i == 2 || i == 3))
                    {
                        DrawButton(i, j, gridData, 25, 50); // 垂直扁长方形
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(gridData);
        }
    }

    private void DrawButton(int i, int j, GridData gridData, float width, float height)
    {
        bool isColored = gridData.grid[i, j] == 1;
        Color originalColor = GUI.backgroundColor;
        GUI.backgroundColor = isColored ? Color.green : Color.white;

        if (GUILayout.Button(isColored ? "1" : "0", GUILayout.Width(width), GUILayout.Height(height)))
        {
            gridData.grid[i, j] = isColored ? 0 : 1;
        }

        GUI.backgroundColor = originalColor;
    }
}
