/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 * Provides an ID for RenderSystem to render corresponding sprite
 */
public class SpriteComponent extends Component {
	public int SpriteID;
	public String SpriteName;

	public SpriteComponent() {

	}

	public SpriteComponent(int ID) {
		this.SpriteID = ID;
	}
	public SpriteComponent(String Spritename) {
		this.SpriteName = Spritename;
	}



}
