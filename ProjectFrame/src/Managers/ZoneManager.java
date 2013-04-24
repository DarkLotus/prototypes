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
import com.esotericsoftware.minlog.Log;
import com.mythiksoftware.ProjectFrame.Logger;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class ZoneManager  {

	private static ImmutableBag<Entity> foundEntities = new Bag<Entity>(); //Stores entities of given type.
	private static Bag<Entity> inRangeEntities = new Bag<Entity>(); //Stores the ones that are in range. < We return this from our method.
	private static WorldPositionComponent entityCallerPosition; //X,Y for the entity asking for entities within range.
	private static WorldPositionComponent foundEntityPosition; //The position of a found entity.
	
	/**
	 * @param e
	 * @param com
	 * @param Range 
	 * @return 
	 */
	public static ImmutableBag<Entity> GetZonesInRange(Entity e, ZoneTypes Ttype, int Range) 
	{			
		inRangeEntities.clear(); //Clear our inRangeEntities Bag..
		entityCallerPosition = e.getComponent(WorldPositionComponent.class);
		//Logger.Log("Entity Caller Coords:" + "X" + (entityCallerPosition.GetCoords())[0] + "Y" + (entityCallerPosition.GetCoords())[1]);
		
		//Grab the entities within range of the caller entity.
		//	TODO: James: Might need another approach in which we don't iterate through all the entities
		//	of a given type in order to filter the ones in range, not sure but this might negatively affect
		//  performance in a larger scope.
		foundEntities = e.getWorld().getManager(GroupManager.class).getEntities(Ttype.name());				
		for(int i = 0; i < foundEntities.size(); i++)
		{
			//Get the WorldPosition of the entity
			foundEntityPosition = foundEntities.get(i).getComponent(WorldPositionComponent.class);
			if(
					//Evaluate if its within the given range
					entityCallerPosition.x + Range > foundEntityPosition.x //X Axis - Right
					&& entityCallerPosition.x - Range < foundEntityPosition.x //X Axis - Left
					&& entityCallerPosition.y  + Range > foundEntityPosition.y //Y Axis - Up
					&& entityCallerPosition.y - Range < foundEntityPosition.y //Y Axis - Down
			)
				
			{	//If it is in the right range we add it to the <Entity> Bag which the method will return.
				inRangeEntities.add(foundEntities.get(i));
			}
		}

		Logger.Log("In range entities: " + Integer.toString(inRangeEntities.size())); //Activate this line to see the magic working.
		return inRangeEntities;
	}	
}

