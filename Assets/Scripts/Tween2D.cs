using System.Collections;
using UnityEngine;

/// <summary>
/// Tweens a 2D object's size, position and transparency.
/// Supports Linear, Exponential and Logarithmic easing (plus in/out variants).
/// Attach to any GameObject with a SpriteRenderer (the renderer is only needed for the alpha tween).
/// </summary>
public class Tween2D : MonoBehaviour
{
    public enum EaseType
    {
        Linear,
        ExponentialIn,      // starts slow, ends fast
        ExponentialOut,     // starts fast, ends slow (mirror of ExponentialIn)
        ExponentialInOut,   // slow - fast - slow
        LogarithmicOut,     // starts fast, ends slow (log curve)
        LogarithmicIn,      // starts slow, ends fast (mirror of LogarithmicOut)
        LogarithmicInOut    // fast - slow - fast
    }

    [Header("Targets")]
    public float sizeX = 1f;
    public float sizeY = 1f;
    public Vector2 targetPosition;
    public bool positionIsLocal = false;
    [Range(0f, 1f)] public float Clearness = 1f;   // target alpha (1 = fully visible)

    [Header("Timing")]
    public float endTime = 1f;
    public bool useUnscaledTime = false;

    [Header("Easing")]
    public EaseType ease = EaseType.ExponentialIn;
    [Tooltip("Curve strength for exponential / logarithmic easing. Higher = more extreme.")]
    [Range(0.5f, 20f)] public float steepness = 5f;

    [Header("Auto start")]
    public bool tweenSizeOnStart;
    public bool tweenPositionOnStart;
    public bool tweenClearnessOnStart;

    public Transform ThisObj { get; private set; }
    private SpriteRenderer sr;

    private Coroutine sizeRoutine, posRoutine, alphaRoutine;

    private void Awake()
    {
        ThisObj = transform;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (tweenSizeOnStart) sizeTween();
        if (tweenPositionOnStart) positionTween();
        if (tweenClearnessOnStart) clearnessTween();
    }

    // ---------- Public API (usable from UnityEvents / other scripts) ----------

    public void sizeTween() => sizeTween(new Vector2(sizeX, sizeY), endTime, ease);

    public void sizeTween(Vector2 target, float duration, EaseType easeType)
    {
        Restart(ref sizeRoutine, SizeRoutine(target, duration, easeType));
    }

    public void positionTween() => positionTween(targetPosition, endTime, ease);

    public void positionTween(Vector2 target, float duration, EaseType easeType)
    {
        Restart(ref posRoutine, PositionRoutine(target, duration, easeType));
    }

    public void clearnessTween() => clearnessTween(Clearness, endTime, ease);

    public void clearnessTween(float targetAlpha, float duration, EaseType easeType)
    {
        if (sr == null) { Debug.LogWarning("Tween2D: no SpriteRenderer found for alpha tween.", this); return; }
        Restart(ref alphaRoutine, AlphaRoutine(targetAlpha, duration, easeType));
    }

    public void StopAllTweens()
    {
        StopAllCoroutines();
        sizeRoutine = posRoutine = alphaRoutine = null;
    }

    // ---------- Easing ----------

    /// <summary>Maps linear progress t (0..1) to eased progress (0..1).</summary>
    public static float Evaluate(EaseType type, float t, float k)
    {
        t = Mathf.Clamp01(t);
        switch (type)
        {
            case EaseType.ExponentialIn:
                return ExpIn(t, k);
            case EaseType.ExponentialOut:
                return 1f - ExpIn(1f - t, k);
            case EaseType.ExponentialInOut:
                return t < 0.5f
                    ? 0.5f * ExpIn(t * 2f, k)
                    : 1f - 0.5f * ExpIn((1f - t) * 2f, k);

            case EaseType.LogarithmicOut:
                return LogOut(t, k);
            case EaseType.LogarithmicIn:
                return 1f - LogOut(1f - t, k);
            case EaseType.LogarithmicInOut:
                return t < 0.5f
                    ? 0.5f * LogOut(t * 2f, k)
                    : 1f - 0.5f * LogOut((1f - t) * 2f, k);

            default:
                return t;
        }
    }

    // (e^(k t) - 1) / (e^k - 1): 0 at t=0, 1 at t=1, grows exponentially
    private static float ExpIn(float t, float k)
    {
        return (Mathf.Exp(k * t) - 1f) / (Mathf.Exp(k) - 1f);
    }

    // ln(1 + k t) / ln(1 + k): 0 at t=0, 1 at t=1, rises fast then flattens
    private static float LogOut(float t, float k)
    {
        return Mathf.Log(1f + k * t) / Mathf.Log(1f + k);
    }

    // ---------- Coroutines ----------

    private IEnumerator SizeRoutine(Vector2 target, float duration, EaseType type)
    {
        Vector3 start = ThisObj.localScale;
        Vector3 end = new Vector3(target.x, target.y, start.z);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += DeltaTime();
            float e = Evaluate(type, elapsed / duration, steepness);
            ThisObj.localScale = Vector3.LerpUnclamped(start, end, e);
            yield return null;
        }
        ThisObj.localScale = end;
        sizeRoutine = null;
    }

    private IEnumerator PositionRoutine(Vector2 target, float duration, EaseType type)
    {
        Vector3 start = positionIsLocal ? ThisObj.localPosition : ThisObj.position;
        Vector3 end = new Vector3(target.x, target.y, start.z);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += DeltaTime();
            float e = Evaluate(type, elapsed / duration, steepness);
            Vector3 p = Vector3.LerpUnclamped(start, end, e);
            if (positionIsLocal) ThisObj.localPosition = p; else ThisObj.position = p;
            yield return null;
        }
        if (positionIsLocal) ThisObj.localPosition = end; else ThisObj.position = end;
        posRoutine = null;
    }

    private IEnumerator AlphaRoutine(float target, float duration, EaseType type)
    {
        float start = sr.color.a;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += DeltaTime();
            float e = Evaluate(type, elapsed / duration, steepness);
            SetAlpha(Mathf.LerpUnclamped(start, target, e));
            yield return null;
        }
        SetAlpha(target);
        alphaRoutine = null;
    }

    // ---------- Helpers ----------

    private float DeltaTime() => useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }

    // Stops the previous tween on the same channel so two tweens never fight.
    private void Restart(ref Coroutine routine, IEnumerator next)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(next);
    }
}

