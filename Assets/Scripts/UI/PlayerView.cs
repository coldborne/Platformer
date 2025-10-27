using System;
using Characters.Players;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusRenderer;
    [SerializeField] private Color _abilityRadiusColor = new Color(1f, 0f, 0f, 0.0f);
    [SerializeField] private Player _player;
    
    private float _abilityRadius;
 
    private void Awake()
    {
        _radiusRenderer.enabled = false;
        _radiusRenderer.color = _abilityRadiusColor;

        _radiusRenderer.transform.position = _player.transform.position;
        _abilityRadius = _player.VampirismAbilityRadius;
        
        float spriteDiameter = _radiusRenderer.sprite.bounds.size.x;
        float targetSpriteDiameter = _abilityRadius * 2f;
        
        float scale = targetSpriteDiameter / spriteDiameter;
        
        _radiusRenderer.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void OnEnable()
    {
        _player.AbilityStarted += TurnOn;
        _player.AbilityCooldownFinished += TurnOff;
    }
    
    private void OnDisable()
    {
        _player.AbilityStarted -= TurnOn;
        _player.AbilityCooldownFinished -= TurnOff;
    }
    
    private void TurnOn(float startTime, float endTime, bool isAscending)
    {
        _radiusRenderer.enabled = true;
    }
    
    private void TurnOff()
    {
        _radiusRenderer.enabled = false;
    }
}