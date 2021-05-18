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
            ".ﻯﺮﺧﺃ ﺓﺮﻣ ﻩﺩﺍﺪﻋﺇ ﻝﻭﺎﺣ .ﺔﻌﻗﻮﺘﻤﻟﺍ ﺮﻴﻳﺎﻌﻤﻠﻟ ﺍًﻘﻓﻭ ﺀﺎﺴﺤﻟﺍ ﺍﺬﻫ ﻰﻠﻋ ﻞﺼﺤﺗ ﻢﻟﻆﺤﻟﺍ ﺀﻮﺴﻟ",
            ".ﻒﻴﻄﻟ ﺀﺎﺴﺤﻟﺍ ﻥﺃ ﻦﻴﺒﺗ !ﺪﻴﺟ ﻞﻤﻋ",
            "!ﻊﺋﺍﺭ ﻞﻤﻋ !ﻲﻟﺎﺜﻣ ﺀﺎﺴﺤﻟﺍ ﺍﺬﻫ",
            "| ؟ﻚﻳﺪﻟ ﺎﻣ ﻢﻳﺪﻘﺘﻟ ﺪﻌﺘﺴﻣ ﺖﻧﺃ ﻞﻫ ؟ﺍﺬﻫ ﻲﻤﻳﺮﻜﻟﺍ ﺭﺰﺠﻟﺍ ﺀﺎﺴﺣ ﻝﺎﺣ ﻒﻴﻛ",
            "!ﻪﻠﻌﻓ ﻚﻨﻜﻤﻳ ﺎﻣ ﻯﺮﻨﻟ .ﺔﻔﺻﻮﻟﺍ ﻩﺬﻬﻟ ﻡﻮﺜﻟﺍﻭ ﻞﺼﺒﻟﺍﻭ ﺭﺰﺠﻟﺍ ﻰﻟﺇ ﺝﺎﺘﺤﺘﺳ .ﻲﻤﻳﺮﻜﻟﺍ ﺭﺰﺠﻟﺍ ﺀﺎﺴﺣ ﻮﻫ ﻩﺮﻴﻀﺤﺗ ﺐﺠﻳ ﻱﺬﻟﺍ ﻲﻟﺎﺘﻟﺍ ﺀﺎﺴﺤﻟﺍ",
            "!ﻯﺮﺧﺃ ﺓﺮﻣ ﻝﻭﺎﺣ .ﻪﺘﻌﻨﺻ ﻱﺬﻟﺍ ﺀﺎﺴﺤﻟﺍ ﻦﻣ ﻪﻴﻓ ﺏﻮﻏﺮﻣ ﻮﻫ ﺎﻤﻣ ﺮﻴﺜﻜﻟﺍ ﻙﺎﻨﻫ ﻥﺎﻛ",
            "!ﺪﻴﺟ ﻞﻤﻋ .ﻚﻟ ﺓﺮﻣ ﻝﻭﻷ ﺍًﺌﻴﺳ ﺲﻴﻟ",
            "!ﺍًﺪﺟ ﺐﺠﻌﻣ ﺎﻧﺃ !ﺓﺮﻣ ﻝﻭﻷ ﺍًﺪﻴﺟ ﺀﺎﺴﺤﻟﺍ ﺖﻳﺩﺃ ﺪﻘﻟ",
            "!ﻩﺮﻴﻀﺤﺘﻟ ﺀﺎﺴﺤﻟﺍ ﻦﻣ ﺪﻳﺰﻤﻟﺍ ﻚﻳﺪﻟ !ﻙﺮﺤﺘﻧ ﺎﻨﻋﺩﺍًﻨﺴﺣ",
            "| ؟ﻚﻳﺪﻟ ﺎﻣ ﻝﺎﺳﺭﺇ ﺪﻳﺮﺗ ﻞﻫ !ﺔﺒﻴﻃ ﻪﺘﺤﺋﺍﺭ .ﺀﺎﺴﺤﻟﺍ ﺾﻌﺑ ﻊﻨﺼﺗ ﺖﻨﻛ ﻚﻧﺃ ﻭﺪﺒﻳ",
            " !ﺃﺪﺒﺗ ﻥﺃ ﻞﺒﻗ ﻕﺮﻤﻟﺍ ﻕﻮﻓ ﺀﻞﻣ ﻦﻣ ﺪﻛﺄﺗ .ﺔﻴﺒﺸﻌﻟﺍ ﻲﺿﺍﺭﻷﺍ ﺔﻘﻄﻨﻣ ﻲﻓ ﺕﺎﻧﻮﻜﻤﻟﺍ ﻩﺬﻫ ﻞﻛ ﻰﻠﻋ ﺭﻮﺜﻌﻟﺍ ﻚﻨﻜﻤﻳ .ﺪﺣﺍﻭ ﻡﻮﺛ ﺐﻌﻜﻣﻭﺪﺣﺍﻭ ﻞﺼﺑ ﺐﻌﻜﻣﻭ ﻢﻃﺎﻤﻃ ﺕﺎﺒﻌﻜﻣ 4 ﻰﻟﺇ ﺝﺎﺘﺤﺘﺳ .ﺮﻴﻀﺤﺘﻟﺍ ﺔﻠﻬﺳ ﻥﻮﻜﺗ ﻥﺃ ﺐﺠﻳ .ﻢﻃﺎﻤﻄﻟﺍ ﺀﺎﺴﺣ ﻲﻫ ﻰﻟﻭﻷﺍ ﻚﺘﻔﺻﻭ",
        };

        allLanguages.Add(english);
        allLanguages.Add(test);
    }
}
