using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public const string KEY_PICKUP = "KEY_PICKUP";
    public const string GOLDKEY_PICKUP = "GOLDKEY_PICKUP";
    public const string OPEN_CHEST = "OPEN_CHEST";
    public const string OPEN_SCROLLCHEST = "OPEN_SCROLLCHEST";
    public const string OPEN_MONSTERCHEST = "OPEN_MONSTERCHEST";
    public const string TORCH_GRAB = "TORCH_GRAB";
    public const string OPEN_CANVAS = "OPEN_CANVAS";
    public const string CLOSE_CANVAS = "CLOSE_CANVAS";
    public const string CUTSCENE_PLAYING = "CUTSCENE_PLAYING";
    public const string CUTSCENE_FINISHED = "CUTSCENE_FINISHED";
    public const string QUARTERS_CUTSCENE_PLAYING = "QUARTERS_CUTSCENE_PLAYING";
    public const string QUARTERS_CUTSCENE_FINISHED = "QUARTERS_CUTSCENE_FINISHED";
    public const string BASEMENTDOOR_OPEN = "BASEMENTDOOR_OPEN";
    public const string PLAYER_HIT = "PLAYER_HIT";
    public const string PLAYER_INJURED = "PLAYER_INJURED";
    public const string GAME_OVER = "GAME_OVER";
    public const string TORCH_WAVE = "TORCH_WAVE";
    public const string UNDER_BED = "UNDER_BED";
    public const string EXIT_BED = "EXIT_BED";
    public const string WALKTHROUGH_CIRCLE = "WALKTHROUGH_CIRCLE";
    public const string DESTROY_BASEMENTDOOR = "DESTROY_BASEMENTDOOR";
    public const string PAUSE_GAME = "PAUSE_GAME";
    public const string FINAL_EVENT = "FINAL_EVENT";
}
