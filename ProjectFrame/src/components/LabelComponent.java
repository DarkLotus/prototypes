/**
 * 
 */
package components;

import com.artemis.Component;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

/**
 * @author James
 * Provides UIRendersystem information to render UI entities.
 */
public class LabelComponent extends Component {
	public String LabelString;
	public Vector2 Location;

	/**
	 * @param name
	 */
	public LabelComponent(String label, Vector2 Location) {
		this.LabelString = label;
		this.Location = Location;
	}
	public LabelComponent(){

	}



}
