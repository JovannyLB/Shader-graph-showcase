using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(EventTrigger))]
public class OlharEAumentar : MonoBehaviour
{

    private Vector3 m_start;
    private Vector3 m_target;
    private bool m_targeted;

    public String shaderName;
    public Material shader;
    private TextMeshPro text;
    
    public Material soldierMaterial;
    public Material gunMaterial;

    // Inicialização
    void Start()
    {
        // Adicionar os triggers.
        EventTrigger trigger = GetComponent<EventTrigger>();

        // Trigger para entrada com o apontador (começou a olhar)
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        // Trigger para saída com o apontador (parou a olhar)
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        // Associa os triggers.
        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
        
        // Coloca o texto
        text = transform.root.GetChild(2).GetComponent<TextMeshPro>();
        text.text = shaderName;

        if (shaderName == "Normal"){
            soldierMaterial.shader = shader.shader;
            gunMaterial.shader = shader.shader;
        }
        
        text.rectTransform.LookAt(FindObjectOfType<Camera>().transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_targeted)
        {
            transform.DOLocalMoveY(0, 1f).OnComplete(() => { ChangeSoldierShader();});
        }
        else{
            transform.DOLocalMoveY(0.1f, 1f);
        }
        
        
        
    }

    private void OnPointerEnter(PointerEventData data)
    {
        // Olhou para o objeto
        m_targeted = true;
    }

    private void OnPointerExit(PointerEventData data)
    {
        // Parou de olhar pra o objeto
        m_targeted = false;
    }

    private void ChangeSoldierShader(){
        soldierMaterial.shader = shader.shader;
        gunMaterial.shader = shader.shader;
    }
}

