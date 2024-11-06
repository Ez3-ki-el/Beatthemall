using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EllipseCollider2D))]
public class EllipseCollider_Editor : Editor
{

    EllipseCollider2D ec;
    PolygonCollider2D polyCollider;
    Vector2 off;

    void OnEnable()
    {
        ec = (EllipseCollider2D)target;

        polyCollider = ec.GetComponent<PolygonCollider2D>();
        if (polyCollider == null)
        {
            polyCollider = ec.gameObject.AddComponent<PolygonCollider2D>();
        }
        polyCollider.points = ec.getPoints();
    }

    public override void OnInspectorGUI()
    {
        GUI.changed = false;
        DrawDefaultInspector();

        ec.advanced = EditorGUILayout.Toggle("Advanced", ec.advanced);
        if (ec.advanced)
        {
            ec.radiusX = EditorGUILayout.FloatField("RadiusX", ec.radiusX);
            ec.radiusY = EditorGUILayout.FloatField("RadiusY", ec.radiusY);
        }
        else
        {
            ec.radiusX = EditorGUILayout.Slider("RadiusX", ec.radiusX, 1, 25);
            ec.radiusY = EditorGUILayout.Slider("RadiusY", ec.radiusY, 1, 25);
        }

        if (GUI.changed || !off.Equals(polyCollider.offset))
        {
            polyCollider.points = ec.getPoints();
        }

        off = polyCollider.offset;
    }

}