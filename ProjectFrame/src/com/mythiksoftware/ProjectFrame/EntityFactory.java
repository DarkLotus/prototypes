/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import com.artemis.Entity;
import com.artemis.World;
import components.MapComponent;

/**
 * @author James
 *
 */
public class EntityFactory {

	
	
	public static Entity createMap(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new MapComponent(name));
		return entity;
	}
}
