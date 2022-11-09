using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    private Planet _planet;
    private Editor _shapeEditor;
    private Editor _colourEditor;

    private void OnEnable()
    {
        _planet = target as Planet;
    }

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if(check.changed) _planet.GeneratePlanet();
        }

        if (GUILayout.Button("Generate Planet"))
        {
            _planet.GeneratePlanet();
        }
        DrawSettingsEditor(_planet.ShapeSettings, _planet.OnShapeSettingsUpdated, ref _planet.ShapeSettingsFoldout, ref _shapeEditor);
        DrawSettingsEditor(_planet.ColourSettings, _planet.OnColorSettingsUpdated,ref _planet.ColourSettingsFoldout, ref _colourEditor);
    }

    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if(settings == null) return;
        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (foldout)
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();
                if (check.changed) onSettingsUpdated?.Invoke();
            }
        }
    }
}