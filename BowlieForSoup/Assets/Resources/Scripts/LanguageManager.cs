using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public int currentLanguage;
    public List<string[]> allLanguages = new List<string[]>();
    public string[] english;
    public string[] test;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        english = new string[]
        {
            "Your first recipe is a tomato soup. It should be pretty easy to make. You'll need 4 tomato cubes, 1 onion cube, and 1 garlic cube. You can find all of these ingredients in the grassland area. Make sure you fill up on broth before you get going!",
            "Looks like you've been making some soup. Smells good! Do you want to submit what you have? |",
            "Well, let's get a move on! You've got more soups to make! ",
            "You did soup-er well for your first time! I'm very impressed! ",
            "Not bad for your first time. Good work!",
            "There was much to be desired from the soup you made. Try again! ",
            "The next soup to make is the creamy carrot soup. You'll need carrots, onion, and garlic for this recipe. Let's see what you can do! ",
            "How's that Creamy Carrot Soup coming along? Are you ready to turn in what you have? | ",
            "This soup is perfect! Great job! ",
            "Good job! The soup turned out nice. ",
            "Unfortunately, you didn't get this soup up to expected standards. Try making it again. ",
        };

        test = new string[]
        {
            "Your first recipe is a tomato soup. It shoud be pretty easy tae make. Ye'll neit 4 tomato cubes, 1 onion cube, an 1 garlic cube. Ye can find aw o these ingredients i the grassland area. Make sure ye fill up on broth before ye get gang!",
            "Looks like ye've been makin some soup. Smells guid! dae ye want tae submit whit ye have? |",
            "Well, let's get a move on! ye've got more soups tae make! ",
            "You did soup-er well for yer first time! A'm very impressed! ",
            "Not bad for yer first time. Guid work!",
            "There wis much tae be desirit from the soup ye made. Try again! ",
            "The neist soup tae make is the creamy carrot soup. Ye'll neit carrots, onion, an garlic for this recipe. Let's see whit ye can dae! ",
            "How's thon creamy carrot soup comin along? are ye ready tae turn i whit ye have? | ",
            "This soup is perfect! great job! ",
            "Good job! the soup turnit oot nice. ",
            "Unfortunately, ye didnae get this soup up tae expectit standards. Try makin it again. ",
        };

        allLanguages.Add(english);
        allLanguages.Add(test);
    }
}
