using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BasePlatCmd
{

    float delay = 0;
    Action onComplete;
    Action onStart;

    public BasePlatCmd SetDelay(float delay)
    {
        this.delay = delay;
        return this;
    }

    public BasePlatCmd OnComplete(Action onComplete)
    {
        this.onComplete = onComplete;
        return this;
    }

    public BasePlatCmd OnStart(Action onStart)
    {
        this.onStart = onStart;
        return this;
    }

    public IEnumerator Excute(PlatHandler plat)
    {
        yield return null;
        if (onStart != null)
            onStart();
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        yield return ExcuteSelf(plat);

        if (onComplete != null)
            onComplete();
    }

    protected abstract IEnumerator ExcuteSelf(PlatHandler plat);

}
