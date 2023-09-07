using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.limphus.utilities;

public class EyeAnimation : AnimationHandler
{
    public const string EYE_BLINK = "EyeBlink";

    public void EyeBlinkTrigger() => SetTrigger(EYE_BLINK, true);
}
