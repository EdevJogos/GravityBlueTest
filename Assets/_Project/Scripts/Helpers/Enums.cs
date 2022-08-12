public enum GameState
{
    NONE,
    LOADING,
    INTRO,
    STARTING,
    PLAY,
    PAUSE,
    DIALOG,
    SHOP,
}

public enum DisplayTypes
{
    MAIN,
    MENU,
    POP_UP,
}

public enum Displays
{
    HUD,
    INTRO,
    LOADING,
    PAUSE,
    CREDITS,
    SETTINGS,
    TUTORIAL,
    DIALOG,
    SHOP,
    WARDROBE,
}

public enum SFXOccurrence
{
    //GAME
    OVEN,
    BUY,
    SELL,
    WEAR,
    NPC_HELLO,
    WARDROBE,
    PICK_UP_BREAD,
    //UI
    OPTION_SELECTED = 150,
    OPTION_CONFIRMED,
    OPTION_DENIED,
    CLOSE_PANEL,
    NONE = 254
}

public enum Particles
{

}

public enum Languages
{
    PT_BR,
    EN_US,
}

public enum SwipeDirections
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
}

public enum Characters
{

}

public enum Prefabs
{
    SHOP_ITEM_BAR,
    WARDROBE_ITEM_BAR,
}

public enum DialogActions
{
    TALK,
    OPEN_SHOP,
    CONTINUE_TALK,
    SET_KNOWN_NAME,
    EXIT,
}

public enum Inputs
{
    PLAYER,
    DIALOG,
    SHOP,
}

public enum Interactions
{
    EXECUTE_ACTION,
    PICK_ITEM,
    NONE = 254,
}

public enum ItemsTypes
{
    INGREDIENTS,
    PREPARED_FOOD,
    CLOTHINGS,
    BODY,
}

public enum Ingredients
{
    EGGS,
    SUGAR,
    FLOUR,
}