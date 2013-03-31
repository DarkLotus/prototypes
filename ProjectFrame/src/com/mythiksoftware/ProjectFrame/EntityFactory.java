/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import com.artemis.Entity;
import com.artemis.World;
import components.MapComponent;
import components.SpriteComponent;
import components.WorldPositionComponent;

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
	
	public static Entity createObject(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent(5,5));
		entity.addComponent(new SpriteComponent(5));
		return entity;
	}
}
