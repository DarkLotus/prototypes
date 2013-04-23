/**
 * 
 */
package helpers;

import java.util.ArrayList;

import com.artemis.Component;
import com.artemis.utils.ImmutableBag;

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
	public ImmutableBag<String> Groups;
	public ArrayList<Component> components = new ArrayList<Component>();

}
