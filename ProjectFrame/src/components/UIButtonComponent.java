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
public class UIButtonComponent extends Component {
	public String LabelString;
	public ChangeListener Listener;
	public Vector2 Location;

	/**
	 * @param name
	 */
	public UIButtonComponent(String label, Vector2 Location, ChangeListener listener) {
		this.LabelString = label;
		this.Location = Location;
		this.Listener = listener;
	}
	public UIButtonComponent(){

	}



}
