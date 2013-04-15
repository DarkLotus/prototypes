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
		SpriteID = ID;
	}
	public SpriteComponent(String Spritename) {
		SpriteName = Spritename;
	}
	
	

}
