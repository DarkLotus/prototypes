/**
 * 
 */
package Managers;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.lang.reflect.Field;

import helpers.Persistant;
import helpers.Save;
import helpers.SaveObject;


import World.World;

import com.artemis.Component;
import com.artemis.Entity;

import com.artemis.managers.GroupManager;
import com.artemis.utils.Bag;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.XmlReader.Element;
import com.esotericsoftware.kryo.Kryo;
import com.esotericsoftware.kryo.io.Input;
import com.esotericsoftware.kryo.io.Output;
import com.mythiksoftware.ProjectFrame.Logger;

/**
 * @author James
 * Takes care of loading and saving the state of a world,
 * saves all entities with all components state intact.
 * Recreates in same order ID's should be the same.
 */
public class PersistenceManager  {

	private static Kryo _kryo = new Kryo();



	public static void Load(World _world,String fileNameString) {
		FileHandle f = Gdx.files.external(fileNameString);
		Input input = null;
		try {
			input = new Input(new FileInputStream(f.file()));
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		if(input == null)return;
		Save save = _kryo.readObject(input, Save.class);
		input.close();
		for (SaveObject saveObject : save.entities) {
			Entity entity = _world.createEntity();
			for (Component component : saveObject.components) {
				entity.addComponent(component);
			}
			//_world.getManager(GroupManager.class).add(entity, "persist");
			for (int _i = 0; _i < saveObject.Groups.size(); _i++) {
				_world.getManager(GroupManager.class).add(entity, saveObject.Groups.get(_i));
			}
			
			_world.addEntity(entity);
		}

	}
	public static void Save(World _world) {
		Save save = new Save();
		ImmutableBag<Entity> entities = _world.getManager(GroupManager.class).getEntities("persist");
		for (int i = entities.size()-1;i >=0;i--) {
			Entity entity = entities.get(i);
			SaveObject saveObject = new SaveObject(entity.getId());
			save.entities.add(saveObject);
			saveObject.Groups = _world.getManager(GroupManager.class).getGroups(entity);
			Bag<Component> components = new Bag<Component>();
			_world.getComponentManager().getComponentsFor(entity, components);
			for (int x = components.size()-1;x >=0;x--) {
				Component component = components.get(x);
				saveObject.components.add(component);
			}
		}
		FileHandle f = Gdx.files.external("save.bin");
		Output output;
		try {
			output = new Output(new FileOutputStream(f.file()));
			_kryo.writeObject(output, save);
			output.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}



	}

	@Deprecated
	public static void Persist(World _world){

		Element root = new Element("root", null);
		ImmutableBag<Entity> entities = _world.getManager(GroupManager.class).getEntities("persist");
		for (int i = entities.size()-1;i >=0;i--) {
			Element childElement = new Element(""+entities.get(i).getUuid(), root);
			root.addChild(childElement);
			Bag<Component> components = new Bag<Component>();
			_world.getComponentManager().getComponentsFor(entities.get(i), components);
			for (int x = components.size()-1;x >=0;x--) {
				Component component = components.get(x);
				Element comp = new Element(component.getClass().getName(), childElement);
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
		//XmlWriter rWriter = new XmlWriter(Gdx.files.external("save.xml").writer(false));
		FileHandle f = Gdx.files.external("save.xml");
		f.writeString(root.toString(""), false);


	}
}

