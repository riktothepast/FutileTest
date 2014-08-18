using UnityEngine;
using System.Collections;

public enum PageType
{
	None,
	TitlePage,
	GamePage,
    GUIBoard,
    SinglePlayerMap,
    OptionsPage
}

public enum animSpeed 
{
    SLOW,
    NORMAL,
    FAST
}

public class Game : MonoBehaviour {

	public static Game instance;

	private FStage _stage;

	public static bool isTesting = false;
    public static int tokenNumber = 7;
    public static bool shouldAnimAvatar = true;
    public static bool showIACards = false;
    public static animSpeed animationSpeed = animSpeed.NORMAL;


	private PageType _currentPageType = PageType.None;
	private Page _currentPage = null;

	// Use this for initialization
	void Start () {
		instance = this;

        if(PlayerPrefs.HasKey("tokenNumber"))
        {
            tokenNumber = PlayerPrefs.GetInt("tokenNumber");
        }

        if(PlayerPrefs.HasKey("shouldAnimAvatar"))
        {
            if(PlayerPrefs.GetInt("shouldAnimAvatar").Equals(0))
            {
                shouldAnimAvatar = false;
            }
            if(PlayerPrefs.GetInt("shouldAnimAvatar").Equals(1))
            {
                shouldAnimAvatar = true;
            }
        }

        if(PlayerPrefs.HasKey("animationSpeed"))
        {
            if(PlayerPrefs.GetString("animationSpeed").Equals("slow"))
            {
                animationSpeed = animSpeed.SLOW;
            }
            if(PlayerPrefs.GetString("animationSpeed").Equals("normal"))
            {
                animationSpeed = animSpeed.NORMAL;
            }
            if(PlayerPrefs.GetString("animationSpeed").Equals("fast"))
            {
                animationSpeed = animSpeed.FAST;
            }
        }

        if(PlayerPrefs.HasKey("showIACards"))
        {
            if(PlayerPrefs.GetInt("showIACards").Equals(0))
            {
                showIACards = false;
            }
            if(PlayerPrefs.GetInt("showIACards").Equals(1))
            {
                showIACards = true;
            }
        }
        
		FutileParams fparams = new FutileParams(true,true,false,false);
		
		fparams.AddResolutionLevel(960.0f,	1.0f,	1.0f,	""); //iPhone retina
        fparams.shouldLerpToNearestResolutionLevel = true;

		fparams.origin = new Vector2(0f,0f);

		Futile.instance.Init (fparams);


		_stage = Futile.stage;
        setTokenNumber(1);
        GoToPage(PageType.GamePage);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToPage (PageType pageType)
	{
		
		Page pageToCreate = null;
		
		
			_currentPageType = pageType;	
			
			if(_currentPage != null)
			{
				_stage.RemoveChild(_currentPage);
			}

            if (pageType == PageType.GamePage)
                pageToCreate = new GamePage();
			
			_currentPage = pageToCreate;
			_stage.AddChild(_currentPage);
			_currentPage.Start();
		
		
	}

    public void GoToFinalPage(bool winner = false) {
             _stage.RemoveChild(_currentPage);
            _stage.AddChild(_currentPage);
            _currentPage.Start();
    }

    public static void setTokenNumber(int num)
    {
        tokenNumber = num;
        PlayerPrefs.SetInt("tokenNumber",num);
        PlayerPrefs.Save();
    }

    public static void setAnimSpeed(animSpeed speed)
    {
        if(speed.Equals(animSpeed.SLOW))
        {
            animationSpeed = speed;
            PlayerPrefs.SetString("animationSpeed","slow");
        }
        if(speed.Equals(animSpeed.NORMAL))
        {
            animationSpeed = speed;
            PlayerPrefs.SetString("animationSpeed","normal");
        }
        if(speed.Equals(animSpeed.FAST))
        {
            animationSpeed = speed;
            PlayerPrefs.SetString("animationSpeed","fast");
        }
        PlayerPrefs.Save();
    }

    public static void setShowIACards(bool show)
    {
        showIACards = show;

        if(show)
        {
            PlayerPrefs.SetInt("showIACards",1);
        }
        else        
        {
            PlayerPrefs.SetInt("showIACards",0);
        }
        PlayerPrefs.Save();
    }
    
    public static void setAvatarAnim(bool enable)
    {
        shouldAnimAvatar = enable;
        if(enable)
        {
            PlayerPrefs.SetInt("shouldAnimAvatar",1);
        }else
        {
            PlayerPrefs.SetInt("shouldAnimAvatar",0);
        }
        PlayerPrefs.Save();
    }
}
