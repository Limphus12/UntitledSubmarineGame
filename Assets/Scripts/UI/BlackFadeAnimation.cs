using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.limphus.utilities;

public class BlackFadeAnimation : AnimationHandler
{
    public const string FADE_OUT = "FadeOut";
    public const string FADE_IN = "FadeIn";

    public void FadeOutTrigger() => SetTrigger(FADE_OUT, true);
    public void FadeInTrigger() => SetTrigger(FADE_IN, true);
}