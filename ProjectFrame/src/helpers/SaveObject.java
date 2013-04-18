/**
 * 
 */
package helpers;

import java.util.ArrayList;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class SaveObject {

	/**
	 * @param id2
	 */
	public SaveObject(int id2) {
		this.ID = id2;
	}
	public SaveObject() {

	}
	public int ID;
	public ArrayList<Component> components = new ArrayList<Component>();

}
