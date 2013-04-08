/**
 * 
 */
package Managers;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.StringWriter;
import java.lang.reflect.Field;

import helpers.Persistant;
import helpers.Save;
import helpers.SaveObject;


import com.artemis.Component;
import com.artemis.Entity;
import com.artemis.World;
import com.artemis.managers.GroupManager;
import com.artemis.utils.Bag;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.XmlReader;
import com.badlogic.gdx.utils.XmlReader.Element;
import com.badlogic.gdx.utils.XmlWriter;
import com.esotericsoftware.kryo.Kryo;
import com.esotericsoftware.kryo.io.Input;
import com.esotericsoftware.kryo.io.Output;
import com.mythiksoftware.ProjectFrame.Logger;

/**
 * @author James
 *
 */
public class PersistenceManager  {

	public static void Load(World world, String name){
		XmlReader reader = new XmlReader();
		Element element;
		try {
			element = reader.parse(Gdx.files.external(name));
			try {
				Load(world, element);
			} catch (ClassNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IllegalArgumentException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IllegalAccessException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (NoSuchFieldException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (SecurityException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (InstantiationException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	public static void Load(World _world,Element root) throws ClassNotFoundException, IllegalArgumentException, IllegalAccessException, NoSuchFieldException, SecurityException, InstantiationException{
		for (int i = root.getChildCount()-1;i >=0;i--) {
			Entity entity = _world.createEntity();
			Element entityElement = root.getChild(i);
			for (int x = entityElement.getChildCount()-1;x >=0;x--) {
				Element componentElement = entityElement.getChild(x);
				
				Object class1 = Class.forName(componentElement.getName()).newInstance();
				if(componentElement.getAttributes() != null)
				for(int z = componentElement.getAttributes().keys().toArray().size-1; z >= 0;z--) {
					String name = componentElement.getAttributes().keys().toArray().get(z);
					float val = Float.parseFloat(componentElement.getAttribute(componentElement.getAttributes().keys().toArray().get(z)));
					((Component) class1).getClass().getField(name).set(class1,val);
				}
				entity.addComponent((Component)class1);
				_world.addEntity(entity);
			}
		}
	}
	
	private static Kryo kryo = new Kryo();
	
	public static void LoadFromKryo(World _world,Input in) {
		Save save = kryo.readObject(in, Save.class);
		in.close();
		for (SaveObject saveObject : save.entities) {
			Entity entity = _world.createEntity();
			for (Component component : saveObject.components) {
				entity.addComponent(component);
			}
			_world.getManager(GroupManager.class).add(entity, "persist");
			_world.addEntity(entity);
		}
		
	}
	public static void PersistToKryo(World _world) {
		Save save = new Save();
		ImmutableBag<Entity> entities = _world.getManager(GroupManager.class).getEntities("persist");
		for (int i = entities.size()-1;i >=0;i--) {
			Entity entity = entities.get(i);
			SaveObject saveObject = new SaveObject(entity.getId());
			save.entities.add(saveObject);
			
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
			kryo.writeObject(output, save);
			output.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
		
	}
	
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

