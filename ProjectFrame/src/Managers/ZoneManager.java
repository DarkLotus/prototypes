/**
 * 
 */
package Managers;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

import java.lang.reflect.Field;

import helpers.Persistant;
import helpers.Save;
import helpers.SaveObject;


import Systems.city.ZoneTypes;
import World.World;

import com.artemis.Component;
import com.artemis.Entity;

import com.artemis.managers.GroupManager;
import com.artemis.utils.Bag;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.XmlReader;
import com.badlogic.gdx.utils.XmlReader.Element;
import com.esotericsoftware.kryo.Kryo;
import com.esotericsoftware.kryo.io.Input;
import com.esotericsoftware.kryo.io.Output;
import com.mythiksoftware.ProjectFrame.Logger;

/**
 * @author James
 *
 */
public class ZoneManager  {

	/**
	 * @param e
	 * @param com
	 * @param Range 
	 * @return 
	 */
	public static ImmutableBag<Entity> GetZonesInRange(Entity e, ZoneTypes Ttype, int Range) {
		// TODO Auto-generated method stub
		switch(Ttype){
			case Res:
				return e.getWorld().getManager(GroupManager.class).getEntities("Residential");
			case Com:
				return e.getWorld().getManager(GroupManager.class).getEntities("Commercial");
			case Ind:
				return e.getWorld().getManager(GroupManager.class).getEntities("Industrial");
			default:
				return null;
		}
		
		//return new Entity[0];
	}
	
	
}

