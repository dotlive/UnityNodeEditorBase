﻿
using UnityEngine;
using UnityEditor;

public class OutputTexture2D : EditorNode
{

    private Texture2D texPreview;

    EditorInputKnob inputNoise;

    public OutputTexture2D()
    {
        inputNoise = AddInput();
        inputNoise.name = "Input Noise";

        texPreview = new Texture2D(200, 200);

        FitKnobs();
        bodyRect.height += 230;
        bodyRect.width = 210f;
    }

    public override void OnBodyGUI()
    {
        GUILayout.Box(texPreview, GUILayout.Width(texPreview.width), GUILayout.Height(texPreview.height));

        if (GUILayout.Button("Update")) {
            UpdateTexture();
        }
    }

    public void UpdateTexture()
    {
        if (!inputNoise.HasOutputConnected()) {
            return;
        }

        var noise = inputNoise.OutputConnection.GetValue<LibNoise.Generator.Perlin>();

        for (int x = 0; x < texPreview.width; ++x) {
            for (int y = 0; y < texPreview.height; ++y) {

                var point = new Vector3(x, y, 0f);
                float value = (float)noise.GetValue(point);

                value = (value + 1) / 2f;
                Color color = Color.HSVToRGB(value, 1f, 1f);

                texPreview.SetPixel(x, y, color);
            }
        }

        texPreview.Apply();
    }
}
