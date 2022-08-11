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
    //UI
    BUTTON_SELECTED = 150,
    BUTTON_PRESSED,
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
    CONTINUE_DIALOG,
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
    PICK_ITEM,
    NPC,
    WARDROBE,
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