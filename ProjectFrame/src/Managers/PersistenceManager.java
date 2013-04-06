/**
 * 
 */
package Managers;

import java.lang.reflect.Field;

import helpers.Persistant;

import com.artemis.Component;
import com.artemis.Entity;
import com.artemis.World;
import com.artemis.managers.GroupManager;
import com.artemis.utils.Bag;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.utils.XmlReader.Element;
import com.mythiksoftware.ProjectFrame.Logger;

/**
 * @author James
 *
 */
public class PersistenceManager  {

	
	public static void Persist(World _world){
		Element root = new Element("root", null);
		ImmutableBag<Entity> entities = _world.getManager(GroupManager.class).getEntities("persist");
		for (int i = entities.size()-1;i >=0;i--) {
			Element childElement = new Element("entity"+entities.get(i).getUuid(), root);
			root.addChild(childElement);
			Bag<Component> components = new Bag<Component>();
			_world.getComponentManager().getComponentsFor(entities.get(i), components);
			for (int x = components.size()-1;x >=0;x--) {
				Component component = components.get(x);
				Element comp = new Element(component.getClass().toString(), childElement);
				childElement.addChild(comp);
				for (Field iterable_element : component.getClass().getFields()) {
					if(iterable_element.isAnnotationPresent(Persistant.class)){
						try {
							comp.setAttribute(iterable_element.getName(), iterable_element.getFloat(component)+"");
						} catch (IllegalArgumentException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						} catch (IllegalAccessException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					}
				}
				
			}
		}
		Logger.Log(root.toString());
	}
}

