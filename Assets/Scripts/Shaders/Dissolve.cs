using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = false;
    bool isAppearing = false;
    float fade = 1f;

    private enum State
    {
        Dissolved,
        Visible
    }

    State state;

    void Start()
    {
        // Get a reference to the material
        material = GetComponent<SpriteRenderer>().material;
        state = State.Visible;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (state == State.Visible)
            {
                isDissolving = true;
                isAppearing = false;
            }
            else
            {
                isAppearing = true;
                isDissolving = false;
            }
        }

        if (isDissolving)
        {
            StartDissolve();
        }

        if (isAppearing)
        {
            StartAppearing();
        }

    }

    private void StartDissolve()
    {
        fade -= Time.deltaTime;

        if (fade <= 0)
        {
            fade = 0f;
            isDissolving = false;
            state = State.Dissolved;
        }

        // Set the property
        material.SetFloat("_Fade", fade);
    }

    private void StartAppearing()
    {
        fade += Time.deltaTime;

        if (fade >= 1)
        {
            fade = 1f;
            isAppearing = false;
            state = State.Visible;
        }

        // Set the property
        material.SetFloat("_Fade", fade);
    }
}
