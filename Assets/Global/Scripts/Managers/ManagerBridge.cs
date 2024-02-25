using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBridge : MonoBehaviour
{
    public void GM_NewGame(GameManager.Game game) => GameManager.instance.NewGame(game);
    public void GM_DeckCount(int decks) => GameManager.instance.deckCount = decks;
}
