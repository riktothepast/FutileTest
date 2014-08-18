using UnityEngine;
using System.Collections;

public class GamePage : Page {

    FSprite TextureA;
    FSprite TextureB;
    FSprite Background;

    public GamePage()
    {
    
    }

	// Use this for initialization
	public override void Start () {
        Background = new FSprite("Futile_White");
        Background.width = Futile.screen.width;
        Background.height = Futile.screen.height;

        Background.SetPosition(new Vector2(Futile.screen.halfWidth, Futile.screen.halfHeight));
        AddChild(Background);

        TextureA = new FSprite("Futile_White");
        TextureA.color = Color.yellow;
        TextureA.SetPosition(new Vector2(Futile.screen.halfWidth, Futile.screen.halfHeight));
        AddChild(TextureA);

        TextureB = new FSprite("Futile_White");
        TextureB.color = Color.red;
        TextureB.SetPosition(new Vector2(Futile.screen.halfWidth, Futile.screen.halfHeight));
        AddChild(TextureB);

        ListenForUpdate(Update);
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = TextureA.GetPosition();
        position.y = Mathf.Lerp(position.y - 10f, position.y + 10f, Mathf.PingPong(Time.time, 1.0f));
        TextureA.SetPosition(position);
        TextureA.alpha = Mathf.PingPong(Time.time, 1);

        position = TextureB.GetPosition();
        position.x = Mathf.Lerp(position.x - 10f, position.x + 10f, Mathf.PingPong(Time.time, 1.0f));
        TextureB.SetPosition(position);
        TextureB.alpha = Mathf.PingPong(Time.time, 1);

        // bad collision detection:
        if (TextureA.textureRect.CloneAndOffset(TextureA.GetPosition().x, TextureA.GetPosition().y).CheckIntersect(TextureB.textureRect.CloneAndOffset(TextureB.GetPosition().x, TextureB.GetPosition().y)))
        {
            //flash the screen
            Background.alpha = Background.alpha == 0 ? Background.alpha = 1 : Background.alpha = 0;
        }
	}
}
