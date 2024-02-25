using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBridge : MonoBehaviour
{
    public void GM_NewGame(int game) => GameManager.instance.NewGame((GameManager.Game)game);
    public void GM_DeckCount(int decks) => GameManager.instance.deckCount = decks;
}
