/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class MapComponent extends Component {
	private String _mapNameString;

	/**
	 * @param name
	 */
	public MapComponent(String name) {
		_mapNameString = name;
	}

	/**
	 * @return the _mapNameString
	 */
	public String getMapName() {
		return _mapNameString;
	}

	/**
	 * @param _mapNameString the _mapNameString to set
	 */
	public void setMapName(String _mapNameString) {
		this._mapNameString = _mapNameString;
	}

	

}
