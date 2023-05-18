using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MyUtils
{
    public static float GetSign(float value)
    {
        if (value >= 0)
            return 1f;
        else
            return -1f;
    }

    public static int GetSign(int value)
    {
        if (value >= 0)
            return 1;
        else
            return -1;
    }

    public static float AdditionValueWithRoundAndLimit(float A, float B, float Maximum, int roundVal = 2)
    {
        if (roundVal < 0)
            roundVal = 0;
        else if (roundVal > 8)
            roundVal = 8;

        float _A = (float)Math.Round(A, roundVal);

        _A += (float)Math.Round(B, roundVal);
        if (_A > (float)Math.Round(Maximum, roundVal))
            _A = (float)Math.Round(Maximum, roundVal);

        return _A;
    }

    public static float SubtractionValueWithRoundAndLimit(float A, float B, float Minimum, int roundVal = 2)
    {
        if (roundVal < 0)
            roundVal = 0;
        else if (roundVal > 8)
            roundVal = 8;

        float _A = (float)Math.Round(A, roundVal);

        _A -= (float)Math.Round(B, roundVal);
        if (_A < (float)Math.Round(Minimum, roundVal))
            _A = (float)Math.Round(Minimum, roundVal);

        return _A;
    }

    public static float AdditionValueWithLimit(float A, float B, float Maximum)
    {
        A += B;
        if (A > Maximum)
            A = Maximum;

        return A;
    }

    public static float SubtractionValueWithLimit(float A, float B, float Minimum)
    {
        A -= B;
        if (A < Minimum)
            A = Minimum;

        return A;
    }


}
