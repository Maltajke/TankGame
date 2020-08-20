using Leopotam.Ecs.Ui.Systems;
using MaltaTanks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    public EcsUiEmitter _ui = null;
    public GameProfile _gameProfile = null;
    public TankInfo _tank = null;
    public List<ObjectProfile> _enemies = null;
    public Camera _cam = null;
    public AudioSource _source = null;

    public List<AudioClip> _clips;
}
