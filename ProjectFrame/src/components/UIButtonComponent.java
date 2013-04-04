/**
 * 
 */
package components;

import com.artemis.Component;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

/**
 * @author James
 *
 */
public class UIButtonComponent extends Component {
	private String _labelString;
	private ChangeListener _listener;
	private Vector2 _location;

	/**
	 * @param name
	 */
	public UIButtonComponent(String label, Vector2 Location, ChangeListener listener) {
		setLabel(label);
		setLocation(Location);
		setListener(listener);
	}

	/**
	 * @return the _labelString
	 */
	public String getLabel() {
		return _labelString;
	}

	/**
	 * @param _labelString the _labelString to set
	 */
	public void setLabel(String _labelString) {
		this._labelString = _labelString;
	}

	/**
	 * @return the _listener
	 */
	public ChangeListener getListener() {
		return _listener;
	}

	/**
	 * @param _listener the _listener to set
	 */
	public void setListener(ChangeListener _listener) {
		this._listener = _listener;
	}

	/**
	 * @return the _location
	 */
	public Vector2 getLocation() {
		return _location;
	}

	/**
	 * @param _location the _location to set
	 */
	public void setLocation(Vector2 _location) {
		this._location = _location;
	}

	

}
