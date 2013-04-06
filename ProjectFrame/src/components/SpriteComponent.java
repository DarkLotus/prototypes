/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class SpriteComponent extends Component {
	private int _spriteID;

	/**
	 * @param i
	 */
	public SpriteComponent() {
		
	}
	
	public SpriteComponent(int ID) {
		_spriteID = ID;
	}

	/**
	 * @return the _spriteID
	 */
	public int getSpriteID() {
		return _spriteID;
	}

	/**
	 * @param _spriteID the _spriteID to set
	 */
	public void setSpriteID(int _spriteID) {
		this._spriteID = _spriteID;
	}

}
