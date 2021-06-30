using System.Collections;
using UnityEngine;

public class BoostAnimation : MonoBehaviour
{
    Renderer m_Renderer;
    MaterialPropertyBlock m_PropertyBlock;

    [ColorUsage( false, true )] public Color NormalColor;
    [ColorUsage( false, true )] public Color BoostColor;

    private void Awake()
    {
        m_Renderer = GetComponentInChildren<Renderer>();
        m_PropertyBlock = new MaterialPropertyBlock();
        m_Renderer.GetPropertyBlock( m_PropertyBlock );
    }

    public void StartBoostAnimation()
    {
        StopAllCoroutines();
        StartCoroutine( BoostColorRoutine() );
    }

    IEnumerator BoostColorRoutine()
    {
        Color color = BoostColor;

        float time = 0f;
        while( time < 1f )
        {
            time += Time.deltaTime;
            color = Color.Lerp( BoostColor, NormalColor, time );
            m_PropertyBlock.SetColor( "_Color", color );
            m_Renderer.SetPropertyBlock( m_PropertyBlock );
            yield return null;
        }

        m_PropertyBlock.SetColor( "_Color", NormalColor );
        m_Renderer.SetPropertyBlock( m_PropertyBlock );
    }

}
